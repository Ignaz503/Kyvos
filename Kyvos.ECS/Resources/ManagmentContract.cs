using System;
using Kyvos.Utility;

namespace Kyvos.ECS.Resources;
public struct ManagmentContract
{
    IDisposable contract;
    public ManagmentContract(IDisposable contract)
    {
        this.contract = contract;
    }

    public void End()
    {
        contract?.Dispose();
    }

    public static readonly ManagmentContract Empty = new(EmptyDispose.Instance);

}

