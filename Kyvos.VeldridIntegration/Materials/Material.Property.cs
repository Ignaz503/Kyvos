using System;
using Veldrid;

namespace Kyvos.VeldridIntegration.Materials;

public partial class Material
{
    public abstract class Property : IDisposable
    {
        protected bool isDisposed = false;

        internal abstract BindableResource Bindable { get; }

        internal int Order { get; private set; }

        public abstract void Dispose();

        public Property(int order)
            => this.Order = order;
    }

}

