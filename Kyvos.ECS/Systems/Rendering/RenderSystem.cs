using DefaultEcs;
using DefaultEcs.System;
using Kyvos.Core;
using Kyvos.Core.Logging;
using Kyvos.VeldridIntegration;
using Veldrid;

namespace Kyvos.ECS.Systems.Rendering;

public class RenderSystem : ISystem<float>, IDisposable
{
    //TODO gfxDevice nullability
    World world;
    bool isDisposed = false;
    bool renderMultiThreaded;
    IApplication application;
    GraphicsDevice gfxDevice;
    Window window;

    //todo mabye parallel execution
    //but resource update cmdlists need to be split as well
    ISystem<RenderContext>[] perFrameResourceUpdates;

    ISystem<RenderContext>[] renderSystems;
    Task[] renderingTasks;
    CommandList resourceCmdList;
    CommandList[] commandLists;
    public RenderSystem(bool renderMultiThreaded, World w, IEnumerable<ISystem<RenderContext>> renderStages, IEnumerable<ISystem<RenderContext>> resourceUpdates)
    {
        this.renderMultiThreaded = renderMultiThreaded;
        this.world = w;
        application = w.Get<IApplication>();
        this.renderSystems = renderStages.Where(s => s != null).ToArray();
        this.perFrameResourceUpdates = resourceUpdates.Where(s => s != null).ToArray();
        renderingTasks = new Task[this.renderSystems.Length];
        IsEnabled = true;

        gfxDevice = application.GetComponent<GraphicsDeviceHandle>()!.GfxDevice;
        window= application.GetComponent<Window>()!;

        resourceCmdList = gfxDevice.ResourceFactory.CreateCommandList();

        if (renderMultiThreaded)
        {
            commandLists = new CommandList[this.renderSystems.Length];
            for (int i = 0; i < commandLists.Length; i++)
            {
                commandLists[i] = gfxDevice.ResourceFactory.CreateCommandList();
            }
        }
        else 
        {
            commandLists = new CommandList[1]{
                gfxDevice.ResourceFactory.CreateCommandList()
            };
        }
    }
    public RenderSystem(bool renderMultiThreaded, World w, RenderSystemProvider renderStages, RenderSystemProvider perFrameResourceUpdates)
        : this(renderMultiThreaded, w, renderStages.Systems, perFrameResourceUpdates.Systems)
    {
    }

    public bool IsEnabled { get; set ; }

    public void Dispose()
    {
        if (isDisposed)
            return;
        foreach(var system in renderSystems)
            system.Dispose();

        resourceCmdList.Dispose();

        foreach(var cmdL in commandLists)
            cmdL.Dispose();

        world = ECSExtensions.EmptyWorld;
        isDisposed = true;
    }
    
    public void Update(float deltaTime)
    {
        try 
        {
            if (!IsEnabled || !window.Exists)
                return;

            UpdateResources(deltaTime);

            if (renderMultiThreaded)
                RenderMultiThreaded(deltaTime);
            else
                RenderSingleThreaded(deltaTime);

            //done
            gfxDevice.SwapBuffers();
        }
        catch (Exception e)
        {
            Log<RenderSystem>.Error(e,"An error occured rendering");
        }
    }

    private void UpdateResources(float deltaTime)
    {
        resourceCmdList.Begin();

        for (int i = 0; i < perFrameResourceUpdates.Length; i++)
        {
            perFrameResourceUpdates[i].Update(CreateContext(resourceCmdList, deltaTime));
        }

        resourceCmdList.End();
        gfxDevice.SubmitCommands(resourceCmdList);
    }

    private void RenderSingleThreaded(float deltaTime)
    {
        var commandList = commandLists[0];
        commandList.Begin();
        for (int i = 0; i < renderSystems.Length; i++)
        {
            renderSystems[i].Update(CreateContext(commandList, deltaTime));
        }

        commandList.End();
        gfxDevice.SubmitCommands(commandList);
    }

    void RenderMultiThreaded(float deltaTime) 
    {
        for (int i = 0; i < commandLists.Length; i++)
        {
            commandLists[i].Begin();
            var cmdList = commandLists[i];
            renderingTasks[i] = Task.Run(() => renderSystems[i].Update(CreateContext(cmdList,deltaTime)));
        }

        Task.WaitAll(renderingTasks);

        for (int i = 0; i < commandLists.Length; i++)
        {
            commandLists[i].End();
            gfxDevice.SubmitCommands(commandLists[i]);
        }
    }

    RenderContext CreateContext(CommandList cmdList, float deltaTime)
        => new() { World = world, GfxDevice = gfxDevice, DeltaTime = deltaTime, CmdList = cmdList };

}


