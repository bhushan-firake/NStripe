using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NStripe
{
    public interface IResponse
    {
    }

    public interface IResponse<T> : IResponse
    {
    }
}
