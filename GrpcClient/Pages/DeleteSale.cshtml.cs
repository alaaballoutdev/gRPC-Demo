using Grpc.Core;
using GrpcClient.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GrpcClient.Pages
{
    public class DeleteSaleModel : PageModel
    {

        [BindProperty(SupportsGet =true)]
        public int Id { get; set; }

        [BindProperty]
        public SaleInput Input { get; set; }

        public async Task<IActionResult> OnGet()
        {
            var client = CustomerClientSingleton.GetCustomerClient();

            try
            {
                var sale = await client.GetSaleInfoAsync(new GrpcServer.Protos.SaleLookUp { SaleId=Id});

                Input = new SaleInput
                {
                    CustomerId = sale.CustomerId,
                    SaleDate = DateTime.Parse(sale.SaleDate),
                    Item = sale.Item,
                    Price = sale.Price,


                };


                return Page();


            }
            catch(RpcException ex) {
                return NotFound(ex.Status);
            }

           
        }

        public async Task<IActionResult> OnPost() {
            var client = CustomerClientSingleton.GetCustomerClient();
            await client.DeleteSaleAsync(new GrpcServer.Protos.SaleLookUp { SaleId=Id});

            return Redirect("/Sales");

        
        
        
        
        }


    }
}
