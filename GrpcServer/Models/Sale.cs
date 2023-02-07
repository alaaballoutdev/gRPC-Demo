namespace GrpcServer.Models
{
    public class Sale
    {
        public int SaleId { get; set; }

        public int CustomerId { get; set; }

        public string Item { get; set; }

        public float Price { get; set; }
        public string SaleDate { get; set; }


    }
}
