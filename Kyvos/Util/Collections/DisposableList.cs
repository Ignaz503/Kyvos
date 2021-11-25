using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kyvos.Util.Collections
{
    public class DisposableList<T> : List<T>, IDisposable where T : IDisposable
    {
        bool isDisposed = false;


        public void Dispose()
        {
            if (!isDisposed)
            {
                isDisposed = true;
                foreach (var item in this)
                {
                    item.Dispose();
                }
            }
        }
    }
}
