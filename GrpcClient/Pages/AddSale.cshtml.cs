using Grpc.Core;
using GrpcClient.Models;
using GrpcServer.Protos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GrpcClient.Pages
{
    public class AddSaleModel : PageModel
    {
        [BindProperty(SupportsGet =true)]            
        public int Id { get; set; }

        [BindProperty]
        public SaleInput Input { get; set; }   
        
        public async Task<ActionResult> OnGet()
        {
            try
            {
                var client = CustomerClientSingleton.GetCustomerClient();
                var customer = client.GetCustomerEmailAsync(new GrpcServer.Protos.CustomerLookUpModel
                {
                    CustomerId = Id
                });

                return Page();
                
            }    
            catch(RpcException ex)
            {
                return NotFound(ex.Status);

            }
        }


        public async Task<ActionResult> OnPost() {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var client = CustomerClientSingleton.GetCustomerClient();
            try
            {
                SaleModel sale = new SaleModel { 
                    CustomerId=Id,
                    Item= Input.Item,
                    Price=Input.Price,
                    SaleDate=Input.SaleDate.ToString("yyyy-MM-dd HH:mm:ss"),

                
                };
                await client.AddSaleAsync(sale);
                return Redirect("/ViewSales/"+Id);
            } catch (RpcException ex) {
                return NotFound(ex.Status);
            
            }

        
        
        
        }




    }
}
