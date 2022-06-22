using Veldrid.StartupUtilities;
using Veldrid;
using Kyvos.Core;
using Kyvos.Graphics;
using Kyvos;
using Kyvos.VeldridIntegration;
using Kyvos.Core.Configuration;
using Kyvos.Assets;
using System;

namespace FullyAutomatedGayLuxurySpaceCommunism;

public interface IEngineTest 
{
    IModifyableApplication BuildApp();
    
    public static IModifyableApplication DefaultAppSetup(string name ="Test") 
    {
        /*
        X = 100,
        Y = 100,
        WindowWidth = 960,
        WindowHeight = 540,
        */

        return new Application().DefaultGUI(
                new WindowCreateInfo()
                {
                    WindowTitle = name,
                    X = 100,
                    Y = 100,
                    WindowHeight = 540,
                    WindowWidth = 960
                },
                new GraphicsDeviceConfig()
                {
                    Backend = GraphicsBackend.Vulkan,
                    Options = new GraphicsDeviceOptions()
                    {
                        PreferStandardClipSpaceYDirection = true,
                        PreferDepthRangeZeroToOne = true,
#if DEBUG
                        Debug = true
#endif
                    }
                },
                new Timer.Config() { FixedUpdateTimingMS = 20f }
            ).WithAssetDatabase();
    }

    public static IModifyableApplication DefaultAppSetup(IConfig config, string? name = "") 
    {
        var app = new Application().DefaultGUI(config);
        if (/*NOT*/!string.IsNullOrEmpty(name)) 
        {
            app.GetComponent<Window>()!.Title = name;
        }
        return app.WithAssetDatabase();

    }
}