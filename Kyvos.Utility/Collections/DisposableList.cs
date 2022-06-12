using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kyvos.Utility.Collections;
public class DisposableList<T> : List<T>, IDisposable where T : IDisposable
{
    bool isDisposed = false;

    public DisposableList() : base()
    { }

    public DisposableList(int capacity) : base(capacity)
    {

    }

    public DisposableList(IEnumerable<T> collection) : base(collection)
    { }

    public DisposableList(params T[] args) : base(args)
    { }

    public void Dispose()
    {
        if (isDisposed)
            return;
        foreach (var item in this)
        {
            item.Dispose();
        }
        isDisposed = true;
    }
}
