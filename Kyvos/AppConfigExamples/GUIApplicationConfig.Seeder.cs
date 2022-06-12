using Kyvos.Core.Configuration;
using Kyvos.Core;
using Kyvos.VeldridIntegration;
using Veldrid;
using Veldrid.StartupUtilities;

namespace Kyvos.AppConfigExamples;

public partial class Config
{
    public class GUIAppSeeder 
    {
        public void Seed( string filePath ) 
        {
            var builder =  Build(Example.Builder);

            builder.WriteTo( filePath );
        }

        virtual protected internal Example Build( Example builder )
        {
            builder.With( Window.CONFIG_KEY, new WindowCreateInfo() )
                .With(GraphicsDeviceHandle.CONFIG_KEY, new GraphicsDeviceConfig { Options = new GraphicsDeviceOptions() { SwapchainDepthFormat = PixelFormat.R16_UNorm }, Backend = GraphicsBackend.Vulkan } )
                .With(Timer.CONFIG_KEY, new Timer.Config() );
            return builder;
        }

        public static IConfig Seed(WindowCreateInfo windowInfo, GraphicsDeviceConfig gfxConfig, Timer.Config timerConfig) 
        {
            var builder = Example.Builder
                    .With(Window.CONFIG_KEY, windowInfo)
                    .With(GraphicsDeviceHandle.CONFIG_KEY, gfxConfig)
                    .With(Timer.CONFIG_KEY, timerConfig);
            return LoadFromString(builder.ToString());
        }

    }

}




