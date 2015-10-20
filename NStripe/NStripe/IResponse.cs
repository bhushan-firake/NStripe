using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NStripe
{
    internal interface IResponse
    {
    }

    internal interface IResponse<T> : IResponse
    {
    }
}
