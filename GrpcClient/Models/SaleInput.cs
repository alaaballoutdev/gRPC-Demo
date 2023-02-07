using System.ComponentModel.DataAnnotations;

namespace GrpcClient.Models
{
    public class SaleInput
    {

        [Required]
        public string Item { set; get; }
        [Required]
        public float Price { set; get; }
        [Required]
        public DateTime SaleDate { set; get; }
        [Required]
        public int CustomerId { set; get; }


    }
}
