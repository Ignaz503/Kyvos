using Kyvos.Maths;
using Kyvos.ImGUI;
using Kyvos.Core.Logging;
using DefaultEcs;
using Kyvos.Graphics;
using System.Diagnostics;

namespace FullyAutomatedGayLuxurySpaceCommunism;

public class MyUI : ISetupableUI
{
    const int min = 3;
    const int max = 5;
    
    MainMenuBar bar;
    MessageBoard? messageBoard;

    Group imageGroup;

    public MyUI(World w)
    {
        bar = new MainMenuBar();

        var val = GetRandomNumberBetweenMinAnMax();
        for (int i = 0; i < val; i++)
        {
            var subChildren = GetRandomNumberBetweenMinAnMax();
            bar.AppendChild(SomeMenu(i, subChildren));
        }

        int GetRandomNumberBetweenMinAnMax()
            => Mathf.RoundToInt(Kyvos.Maths.Mathf.Map(Kyvos.Maths.Random.Value, 0, 1, min, max));

        if(w.Has<MessageBoard>())
            messageBoard = w.Get<MessageBoard>();

        imageGroup = new() { Name = "Image", Flags=ImGuiNET.ImGuiWindowFlags.AlwaysAutoResize};

    }


    public void Setup(ref ImGuiHandle handle)
    {
        var image = new Image(new(TemporaryConsts.TextureToUse), handle);
        //image.Size *= 10;
        imageGroup.AppendChild(image) ;
    }

    public void Show()
    {
        bar.Show();
        messageBoard?.Show();
        imageGroup?.Show();
    }

    Menu SomeMenu(int idx, int items) 
    {
        var menu = new Menu($"Menu {idx}");

        for (int i = 0; i < items; i++)
        {
            var text = $"Menu {idx} Item {i}";
            menu.AppendChild(new MenuItem(text, () => LogText(text)));
        }

        return menu;
    }

    void LogText(string text) 
    {
        Log.Information(text);
    }

    public void Dispose()
    {
        bar.Dispose();
        messageBoard?.Dispose();
    }

}
