using Kyvos.Core.Applications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Veldrid.Sdl2;

using Kyvos.Core.Inputs;

namespace Kyvos.Core.Applications
{
    public abstract class Application : IApplication
    {
        public static IApplication Instance { get; private set; }

        public ApplicationData AppData { get; init; }

        bool isDisposed = false;
        bool resized = false;

        protected Application()
        {
            Instance = this;
        }

        public void Execute()
        {
            Initialize();
            Time.Start();

            while (AppData.WindowData.Window.Exists)
            {
                Time.MeassureElapsedTime();
                if (resized)
                    HandleResize();

                Sdl2Events.ProcessEvents();
                Input.Update( AppData.WindowData.Window.PumpEvents() );
                Run();
            }
            HandleClosing();
        }

        internal virtual void Initialize()
        {
            AppData.WindowData.Window.Resized += () => resized = true;
        }

        internal abstract void Cleanup();

        internal abstract void Run();

        public void Dispose()
        {
            if (!isDisposed)
            {
                isDisposed = true;
                HandleDispose();
            }

        }

        void HandleClosing()
        {
            Time.Stop();

            Cleanup();

            AppData.GfxDevice.Dispose();
        }

        private void HandleDispose()
        {
            AppData.WindowData.Window.Close();
        }

        void HandleResize()
        {
            resized = false;
            AppData.GfxDevice.ResizeMainWindow( (uint)AppData.WindowData.Width, (uint)AppData.WindowData.Height );
        }
    }
}
