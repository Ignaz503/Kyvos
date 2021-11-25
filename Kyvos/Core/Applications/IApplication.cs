using System;

namespace Kyvos.Core.Applications
{
    public interface IApplication : IDisposable
    {
        ApplicationData AppData { get; init; }

        void Execute();

    }

}
