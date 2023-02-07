using System.ComponentModel.DataAnnotations;

namespace GrpcClient.Models
{
    public class CustomerInput
    {
        [Required]
        public string CustomerName { get; set; }

        [Required]
        public string CustomerEmail { get; set; }

        [Required]
        public int  CustomerAge { get; set; }
        

    }
}
