using Grpc.Core;
using GrpcClient.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GrpcClient.Pages
{
    public class EditSaleModel : PageModel
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
                var sale = await client
                    .GetSaleInfoAsync(new GrpcServer.Protos.SaleLookUp { SaleId=Id});
                Input = new SaleInput
                {
                    Item = sale.Item,
                    Price = sale.Price,
                    SaleDate = DateTime.Parse(sale.SaleDate),
                    CustomerId= sale.CustomerId
                }; 

                return Page();
                

            }
            catch (RpcException ex) {
                return NotFound(ex.Status);
            
            }
            
        }

        public async Task<IActionResult> OnPost() {
            if (!ModelState.IsValid) {
                return Page();
            
            }

            var client = CustomerClientSingleton.GetCustomerClient();
            await client.UpdateSaleAsync(new GrpcServer.Protos.SaleModel
            {
                SaleId = Id,
                SaleDate = Input.SaleDate.ToString("yyyy-MM-dd HH:mm:ss"),
                CustomerId= Input.CustomerId,
                Item= Input.Item,
                Price = Input.Price
            }) ;
            return Redirect("/Sales");
        
        
        
        }
    }
}
