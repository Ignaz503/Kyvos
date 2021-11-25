using Kyvos.Util.Sdl2;
using Veldrid.Sdl2;

namespace Kyvos.Core.Applications
{
    public struct WindowData 
    {
        public Sdl2Window Window { get; init; }

        public int Width => Window.Width;
        public int Height => Window.Height;

        public int DisplayRefreshRate => DisplayInformation.GetDisplayRefreshRate( @default:() => 60 );

        public float DisplayRefreshPerSecond => 1f / DisplayRefreshRate;

        public static implicit operator Sdl2Window(WindowData d)
            => d.Window;
    }

}
