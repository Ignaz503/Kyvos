using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kyvos.Util
{
    public interface IIdentifiable<T>
    {
        T ID { get; }
    }
}
