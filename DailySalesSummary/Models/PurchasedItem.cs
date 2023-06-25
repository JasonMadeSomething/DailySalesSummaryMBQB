namespace DailySalesSummary.Models
{
    public class PurchasedItem
    {
    public long? SaleDetailId { get; set; }
        public int? Id { get; set; }
        public bool? IsService { get; set; }
        public string BarcodeId { get; set; }
        public string Description { get; set; }
        public int? ContractId { get; set; }
        public int? CategoryId { get; set; }
        public int? SubCategoryId { get; set; }
        public decimal? UnitPrice { get; set; }
        public int? Quantity { get; set; }
        public decimal? DiscountPercent { get; set; }
        public decimal? DiscountAmount { get; set; }
        public decimal? Tax1 { get; set; }
        public decimal? Tax2 { get; set; }
        public decimal? Tax3 { get; set; }
        public decimal? Tax4 { get; set; }
        public decimal? Tax5 { get; set; }
        public decimal? TaxAmount { get; set; }
        public decimal? TotalAmount { get; set; }
        public string Notes { get; set; }
        public bool? Returned { get; set; }
        public long? PaymentRefId { get; set; }
        public DateTime? ExpDate { get; set; }
        public DateTime? ActiveDate { get; set; }
        public string GiftCardBarcodeId { get; set; }
    }
}
