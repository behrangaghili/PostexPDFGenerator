namespace Postex.Sample.Domain
{
    public class ReceiptRequest
    {
        public string BarcodeImage { get; set; }
        public string BarcodeNo { get; set; }
        public string StoreName { get; set; }
        public string StorePhoneNo { get; set; }
        public string Source { get; set; }
        public string StoreAddress { get; set; }
        public string StorePostCode { get; set; }
        public string StoreAnswerTime { get; set; }
        public string E_NemadNo { get; set; }
        public string StoreUrl { get; set; }
        public string ShipmentId { get; set; }
        public string ProductName { get; set; }
        public bool IsSafeBuy { get; set; }
        public string PayeType { get; set; }
        public decimal Eng_Kala_Price { get; set; }
        public string ReciverFullName { get; set; }
        public string ReciverPhoneNo { get; set; }
        public string Destination { get; set; }
        public string ReciverAddress { get; set; }
        public string ReciverPostCode { get; set; }
        public decimal ApproximateValue { get; set; }
        public string PostTypeName { get; set; }
        public decimal Weight { get; set; }
        public DateTime SendToPostDate { get; set; }
        public decimal PostPrice { get; set; }
        public decimal CodCost { get; set; }
        public decimal CodBmValue { get; set; }
        public decimal CodCostPlusCodBmValue { get; set; }
    }
}
