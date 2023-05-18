namespace Shopping.Web.Models
{
    public class CartHeaderViewModel
    {
        public string UserId { get; set; }
        public string CouponCode { get; set; }
        public decimal PurchaseAmount { get; internal set; }
    }
}
