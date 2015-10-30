using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NStripe
{
    #region Request

    [Route("/bitcoin/receivers", "POST")]
    public class CreateStripeBitcoinReceiver : StripeRequestBase, IResponse<StripeBitcoinReceiver>
    {
        public int Amount { get; set; }
        public string Currency { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public Dictionary<string, string> Metadata { get; set; }
        public bool? RefundMispayments { get; set; }
    }

    [Route("/bitcoin/receivers/{receiverId}")]
    public class RetrieveStripeBitcoinReceiver : StripeRequestBase, IResponse<StripeBitcoinReceiver>
    {
        public string ReceiverId { get; set; }
    }

    [Route("/bitcoin/receivers", "GET")]
    public class GetStripeBitcoinReceivers : StripeRequestBase, IResponse<StripeCollection<StripeBitcoinReceiver>>
    {
        public int? Limit { get; set; }
        public int? Offset { get; set; }
        public bool? Active { get; set; }
        public bool? Filled { get; set; }
        public bool? UncapturedFunds { get; set; }
    }

    #endregion Request

    public class StripeBitcoinReceiver : StripeId
    {
        public bool Active { get; set; }
        public int Amount { get; set; }
        public int AmountReceived { get; set; }
        public int BitcoinAmount { get; set; }
        public int BitcoinAmountReceived { get; set; }
        public string BitcoinUrl { get; set; }
        public DateTime Created { get; set; }
        public string Currency { get; set; }
        private string Customer { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public bool Filled { get; set; }
        public string InboundAddress { get; set; }
        public bool Livemode { get; set; }
        public Dictionary<string, string> Metadata { get; set; }
        public string Payment { get; set; }
        public string RefundAddress { get; set; }

        //TODO:Add transactions

        public bool UncapturedFunds { get; set; }
        public bool UsedForPayment { get; set; }
    }
}
