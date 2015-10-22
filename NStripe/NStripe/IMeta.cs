using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NStripe
{
    public interface IMeta
    {
        Dictionary<string, string> Metadata { get; set; }
    }

    public class Meta : IMeta
    {
        public Dictionary<string, string> Metadata { get; set; }
    }
}
