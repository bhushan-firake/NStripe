using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NStripe
{
    #region Request

    [Route("/balance", "GET")]
    public class RetrieveStripeBalance : StripeRequestBase, IResponse<StripeBalance>
    {
    }

    #endregion Request

    public class StripeBalance : StripeId
    {
        public Dictionary<string, object> Available { get; set; }
        public bool Livemode { get; set; }
        public Dictionary<string, object> Pending { get; set; }
    }
}
