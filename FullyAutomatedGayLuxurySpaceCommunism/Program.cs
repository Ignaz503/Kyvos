using Kyvos.AppConfigExamples;
using Kyvos.Core.Configuration;
using Kyvos.VeldridIntegration;
using Veldrid.StartupUtilities;
using System.Diagnostics;
using System;

namespace FullyAutomatedGayLuxurySpaceCommunism;
class Program
{
    static void Main(string[] args)
    {
        var config = Config.Load();

        using var app = EngineTestRegistry.BuildTest(config);

        app.Run();
        //var winfo = config.ReadValue<WindowCreateInfo>(Window.CONFIG_KEY);

        //Debug.WriteLine($"X: {winfo.X} Y: {winfo.Y} Width: {winfo.WindowWidth} Height: {winfo.WindowHeight} Name: {winfo.WindowTitle} State: {winfo.WindowInitialState}");

        //using var app = new CameraMovementMeshTest(config).BuildApp();

        //app.Run();
    }

}
