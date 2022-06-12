using DefaultEcs;
using Kyvos.Core;
using Kyvos.ECS;
using Kyvos.ECS.Components.Management;
using Kyvos.ECS.GameStatesExtension;
using Kyvos.ECS.Resources;
using Kyvos.GameStates;
using Kyvos.VeldridIntegration;
using Kyvos.AppConfigExamples;
using Veldrid.StartupUtilities;
using Kyvos.Core.Configuration;
using Kyvos.Core.Logging;

namespace Kyvos;

public static class DefaultGUIApplication
{
    public static IModifyableApplication DefaultGUI(this IModifyableApplication app, string configfileName)
    {
        return app.WithConfig(configfileName)
               .WithDefaultLogging()
               .WithWindow()
               .WithGfxDevice()
               .WithTimer()
               .WithGameStateStack<ECSGameState.Builder, ECSGameState, World>()
               .With(ResourceManagers.Default)
               .With(ComponentManager.Default)
               .DefaultGUICreateMainLoop();

    }


    public static IModifyableApplication DefaultGUI(this IModifyableApplication app, WindowCreateInfo windowInfo, GraphicsDeviceConfig gfxDeviceConfig, Timer.Config timerConfig)
    {
        return app.With(Config.GUIAppSeeder.Seed(windowInfo, gfxDeviceConfig, timerConfig))
               .WithDefaultLogging()
               .WithWindow()
               .WithGfxDevice()
               .WithTimer()
               .WithGameStateStack<ECSGameState.Builder, ECSGameState, World>()
               .With(ResourceManagers.Default)
               .With(ComponentManager.Default)
               .DefaultGUICreateMainLoop();
    }

    public static IModifyableApplication DefaultGUI(this IModifyableApplication app, IConfig config) 
    {
        return app.With(config)
               .WithDefaultLogging()
               .WithWindow()
               .WithGfxDevice()
               .WithTimer()
               .WithGameStateStack<ECSGameState.Builder, ECSGameState, World>()
               .With(ResourceManagers.Default)
               .With(ComponentManager.Default)
               .DefaultGUICreateMainLoop();
    }

    static IModifyableApplication DefaultGUICreateMainLoop(this IModifyableApplication app)
    {
        app.With(new SequentialAppComponentSystem(
            new Timer.System(),
            new Window.System(),
            new GameStateStack<ECSGameState.Builder, ECSGameState, World>.System(app)
            ));
        return app;
    }

    static IModifyableApplication WithDefaultLogging(this IModifyableApplication app) 
    {
        app.WithLogging(logSetup =>
        {
            logSetup.WithConsoleLogging(outputTemplate: Templates.ThreadNameAndSourceContextMessage)
                .WithDebugLogging(outputTemplate: Templates.ThreadNameAndSourceContextMessage)
#if DEBUG
                .WithMinimumLevel(LogLevel.Verbose)
#endif
                .ShowThreadName(); 
        });

        return app;
    }

}
