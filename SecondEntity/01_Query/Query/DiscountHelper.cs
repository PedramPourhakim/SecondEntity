namespace _01_Query.Query
{
    public class DiscountHelper
    {
        public long ProductId { get; set; }
        public int DiscountRate { get; set; }
        public DiscountHelper Init(long ProductId,int DiscountRate)
        {
            this.ProductId = ProductId;
            this.DiscountRate = DiscountRate;
            return this;
        }
    }
}    

    