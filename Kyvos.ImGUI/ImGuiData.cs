using DefaultEcs;
using DefaultEcs.System;
using DefaultEcs.Threading;
using ImGuiNET;
using Kyvos.Core;
using Kyvos.Core.Logging;
using Kyvos.VeldridIntegration;
using System.Diagnostics;
using System.Numerics;
using Veldrid;
using Kyvos.ECS;

namespace Kyvos.ImGUI;

public partial struct ImGuiData : IDisposable
{
    bool isDisosed;
    int width;
    int height;
    internal ImGuiRenderer renderer; //TODO static resource?

    public ImGuiData() 
    {
        throw new InvalidOperationException($"Must supply {typeof(IApplication).Name} to ImguiData");
    }

    public ImGuiData(int width, int height, IApplication app, Framebuffer mainFrameBuffer)
    {

        isDisosed = false;
        Debug.Assert(app.HasComponent<GraphicsDeviceHandle>());
        var gfxHandle = app.GetComponent<GraphicsDeviceHandle>()!;

        this.width = width;
        this.height = height;
        renderer = new ImGuiRenderer(gfxHandle.GfxDevice, mainFrameBuffer.OutputDescription, width, height, ColorSpaceHandling.Linear);

    }

    public ImGuiData(int width, int height, IApplication app)
    {
        isDisosed = false;
        Debug.Assert(app.HasComponent<GraphicsDeviceHandle>());
        var gfxHandle = app.GetComponent<GraphicsDeviceHandle>()!;

        this.width = width;
        this.height = height;
        renderer = new ImGuiRenderer(gfxHandle.GfxDevice, gfxHandle.GfxDevice.SwapchainFramebuffer.OutputDescription, width, height, ColorSpaceHandling.Linear);

        RegisterToWindowEvent(app);
    }

    public ImGuiData(IApplication app)
    {
        isDisosed = false;
        Debug.Assert(app.HasComponent<GraphicsDeviceHandle>(), "Graphics device must be present");
        Debug.Assert(app.HasComponent<Window>(),"Window must be present");
        var gfxHandle = app.GetComponent<GraphicsDeviceHandle>()!;
        var window = app.GetComponent<Window>()!;

        this.width = window.Width;
        this.height = window.Height;
        renderer = new ImGuiRenderer(gfxHandle.GfxDevice, gfxHandle.GfxDevice.SwapchainFramebuffer.OutputDescription, width, height, ColorSpaceHandling.Linear);

        RegisterToWindowEvent(app);
    }

    private void RegisterToWindowEvent(IApplication app)
    {
        Debug.Assert(app.HasComponent<Window>());
        var window = app.GetComponent<Window>()!;

        //no double register
        window.OnWindowEvent -= OnWindowEvent;
        window.OnWindowEvent += OnWindowEvent;
    }

    private void OnWindowEvent(Window.Event ev)
    {
        if (ev.Type is Window.EventType.Resized) 
        {
            width = ev.Window.Width;
            height = ev.Window.Height;
            renderer.WindowResized(width, height);
        }
    }

    private void UnregisterWindowEvent(IApplication app) 
    {
        Debug.Assert(app.HasComponent<Window>());
        var window = app.GetComponent<Window>()!;
        window.OnWindowEvent -= OnWindowEvent;
    }    
    
    public void Dispose()
    {
        if (isDisosed)
            return;

        Log<ImGuiData>.Information("Disposing ImGui wrapper");

        renderer.Dispose();

        isDisosed = true;
    }

}

public static class UIApplicationExtensions
{
    public static IModifyableApplication WithUIComponentManagment(this IModifyableApplication app)
     => app.With(ImGuiData.ComponentManager.Instance);


}

public class UIBlueprint 
{
    public static void AddUI(IUIComponent component, World world)
    {
        Debug.Assert(world.Get<IApplication>() != null, "Application must be set");
        world.SetMaxCapacity<ImGuiData>(1);
        world.Set<ImGuiData>(new(world.Get<IApplication>()));

        world.SetMaxCapacity<UITree>(1);
        world.Set(new UITree(component));
    }

    public static ISystem<float> GetSystem(World w) 
    {
        return new SequentialSystem<float>(
            new ImGuiData.System(w),
            new UITree.System(w)
            );
    }
    public static ISystem<float> GetSystem(World w, IParallelRunner runner)
    {
        return new SequentialSystem<float>(
            new ImGuiData.System(w, runner),
            new UITree.System(w, runner)
            );
    }


    public static ISystem<float> GetSystem(World w, IParallelRunner runner, int minComponentCountByRunnerIndex)
    {
        return new SequentialSystem<float>(
            new ImGuiData.System(w, runner, minComponentCountByRunnerIndex),
            new UITree.System(w, runner, minComponentCountByRunnerIndex)
            );
    }
}

public interface IUIComponent 
{
    void Show();
}

public interface IUINode : IUIComponent, IEquatable<IUINode>
{
    void AppendChild(IUINode node);

    void AppendChildAt(IUINode node, int idx);

    int IdxOfChild(IUINode child);

    void RemoveChild(IUINode node);

}

public abstract class UILeafNode : IUINode
{
    public void AppendChild(IUINode node)
    {}

    public void AppendChildAt(IUINode node, int idx)
    {}

    public abstract bool Equals(IUINode? other);

    public int IdxOfChild(IUINode node)
        => -1;

    public void RemoveChild(IUINode node)
    { }

    public abstract void Show();
}

public abstract class OneChildNode : IUINode
{
    protected IUINode? Child { get; set; }

    public void AppendChild(IUINode node)
    {
        Child = node;
    }

    public void AppendChildAt(IUINode node, int idx)
    {
        if (idx > 0)
            throw new IndexOutOfRangeException($"Node '{GetType().Name}' can only have one child. Index needs to be 0");
        Child = node;
    }


    public void RemoveChild(IUINode uINode)
    {
        if (EqualityComparer<IUINode>.Default.Equals(uINode, Child))
            Child = null;
    }

    public int IdxOfChild(IUINode uiNode)
    {
        if (EqualityComparer<IUINode>.Default.Equals(uiNode, Child))
            return 0;
        return -1;
    
    }

    public abstract void Show();

    public abstract bool Equals(IUINode? other);
}

public abstract class FixedChildSizeUINode : IUINode
{
    protected IUINode?[] Children { get; set; }
    int childIndexer = 0;

    public FixedChildSizeUINode(int size)
    {
        Debug.Assert(size > 0, "Size must be greater than 0");
        Children = new IUINode[size];
    }

    public void AppendChild(IUINode node)
    {
        Children[childIndexer] = node;
        childIndexer = (childIndexer + 1) % Children.Length;
    }

    public void AppendChildAt(IUINode node, int idx)
    {
        if (idx > Children.Length)
            throw new IndexOutOfRangeException($"Idx for node '{GetType().Name}' needs to be in range [0..{Children.Length}[");
        Children[idx] = node;
        childIndexer = (idx + 1) % Children.Length;
    }

    public int IdxOfChild(IUINode child)
    {
        for (int i = 0; i < Children.Length; i++)
        {
            if (EqualityComparer<IUINode>.Default.Equals(child, Children[i]))
                return i;
        }
        return -1;
    }

    public void RemoveChild(IUINode node)
    {
        for (int i = 0; i < Children.Length; i++)
        {
            var child = Children[i];
            if (child is not null && EqualityComparer<IUINode>.Default.Equals(child, node))
            {
                Children[i] = null;
                childIndexer = i;
                return;
            }
        }
    }

    public abstract void Show();

    public abstract bool Equals(IUINode? other);
}

public abstract class TwoChildUINode : FixedChildSizeUINode
{
    protected TwoChildUINode() : base(2)
    {}

    protected IUINode? FirstChild => Children[0];
    protected IUINode? SecondChild => Children[1];
}

public abstract class ThreeChildUINode : FixedChildSizeUINode
{
    protected ThreeChildUINode() : base(3)
    { }

    protected IUINode? FirstChild => Children[0];
    protected IUINode? SecondChild => Children[1];
    protected IUINode? ThirdChild => Children[2];
}

public abstract class NChildUINode : IUINode
{
    protected List<IUINode> children;

    public NChildUINode()
    {
        children = new();
    }

    public void AppendChild(IUINode node)
        => children.Add(node);
    

    public void AppendChildAt(IUINode node, int idx)
        => children.Insert(idx, node);
    

    public abstract bool Equals(IUINode? other);

    public int IdxOfChild(IUINode child)
        => children.IndexOf(child);


    public void RemoveChild(IUINode node)
        => children.Remove(node);

    public abstract void Show();
}

public sealed class MainMenuBar : NChildUINode
{
    public override bool Equals(IUINode? other)
    {
        return this == other; // just do a reference equality
        //if (other is not MainMenuBar menu)
        //    return false;

        //if (this.children.Count != menu.children.Count)
        //    return false;

        ////check all children are the same
        //for (int i = 0; i < children.Count; i++)
        //{
        //    if (!EqualityComparer<IUINode>.Default.Equals(children[i], menu.children[i]))
        //        return false;
        //}


        //return true;
    }

    public override void Show()
    {
        if (ImGui.BeginMainMenuBar()) 
        {
            foreach (var child in children)
                child.Show();
            ImGui.EndMainMenuBar();
        }
    }
}

public sealed class Menu : NChildUINode
{
    public string Label { get; set; }
    public Menu(string label):base()
    {
        this.Label = label;
    }

    public override bool Equals(IUINode? other)
    {
        return this == other; // just do a reference equality
        //if (other is not Menu menu)
        //    return false;

        //if (Label != menu.Label)
        //    return false;

        //if (this.children.Count != menu.children.Count)
        //    return false;

        ////check all children are the same
        //for (int i = 0; i < children.Count; i++)
        //{
        //    if (!EqualityComparer<IUINode>.Default.Equals(children[i], menu.children[i]))
        //        return false;
        //}

        //return true;
    }

    public override void Show()
    {
        if (ImGui.BeginMenu(Label)) 
        {
            foreach (var child in children)
                child.Show();
            ImGui.EndMenu();
        }
    }
}

public sealed class MenuItem : UILeafNode
{
    public string Label { get; set; }
    public string ShortCut { get; set; }

    Func<bool> Selected { get; set; }
    Func<bool> Enabled { get; set; }
    Action callback;

    public MenuItem(string label, Action callback, string shortCut = ""):base()
    {
        this.Label = label;
        this.callback = callback;
        ShortCut = shortCut;
        Selected = () => false;
        Enabled = () => true;
    }

    public MenuItem(string label, Action callback, Func<bool> enabled, Func<bool> selected, string shortCut = "") : base()
    {
        this.Label = label;
        this.callback = callback;
        ShortCut = shortCut;
        Selected = selected;
        Enabled = enabled;
    }


    public override bool Equals(IUINode? other)
    {
        return this == other; //ref equality is enough
        //if (other is not MenuItem menuItem)
        //    return false;

        //if (Label != menuItem.Label)
        //    return false;

        //if (this.children.Count != menuItem.children.Count)
        //    return false;

        ////check all children are the same
        //for (int i = 0; i < children.Count; i++)
        //{
        //    if (!EqualityComparer<IUINode>.Default.Equals(children[i], menuItem.children[i]))
        //        return false;
        //}


        //return true;
    }

    public override void Show()
    {
        if (ImGui.MenuItem(Label, ShortCut, Selected(), Enabled()))
        {
            callback();
        }
    }
}

public class Button : UILeafNode
{
    public string Label { get; set; }

    protected Action callback;

    public Button(string label, Action callback)
    {
        Label = label;
        this.callback = callback;
    }


    public override bool Equals(IUINode? other)
    => this == other;
    

    public override void Show()
    {
        if (ImGui.Button(Label))
            callback();
    }
}

public class SizedButton : Button
{
    public Vector2 Size { get; set; }
    public SizedButton(string label,Vector2 size, Action callback) : base(label, callback)
    {
        this.Size = size;
    }

    public override void Show()
    {
        if (ImGui.Button(Label, Size))
            callback();
    }

}

public class Text : UILeafNode 
{
    public string Content { get; set; }

    public Text(string content )
    {
        Content = content;
    }

    public override bool Equals(IUINode? other)
        => this == other;

    public override void Show()
    {
        ImGui.Text(Content);
    }
}


public struct UITree 
{
    IUIComponent root;

    public UITree(IUIComponent root)
    {
        this.root = root;
    }

    void Show() 
    {
        root.Show();
    }

    public class System : AComponentSystem<float, UITree>
    {
        public System(World world) : base(world)
        {
        }

        public System(World world, IParallelRunner runner) : base(world, runner)
        {
        }

        public System(World world, IParallelRunner runner, int minComponentCountByRunnerIndex) : base(world, runner, minComponentCountByRunnerIndex)
        {
        }

        protected override void Update(float state, ref UITree component)
        {
            component.Show();
        }
    }

}
