using Grpc.Core;
using GrpcClient.Models;
using GrpcServer.Protos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace GrpcClient.Pages
{
    public class DeleteCustomerModel : PageModel
    {
        [BindProperty(SupportsGet =true)]
        public int Id { get; set; }

        [BindProperty]
        public CustomerInput Input { get; set; }


        public async Task<IActionResult> OnGet()
        {
            var client= CustomerClientSingleton.GetCustomerClient();
            try
            {
                var customer = await client.GetCustomerInfoAsync(new GrpcServer.Protos.CustomerLookUpModel
                {
                    CustomerId = Id

                });

                Input = new CustomerInput
                {
                    CustomerName = customer.CustomerName,
                    CustomerAge = customer.Age,
                    CustomerEmail = customer.EmailAddress

                }; 

                return Page();
            
            
            }catch(RpcException ex)
            {
                return NotFound(ex.Status);
            }

        }

        public async Task<IActionResult> OnPost() {

            var client = CustomerClientSingleton.GetCustomerClient();

            await client.DeleteCustomerAsync(new CustomerLookUpModel { CustomerId=Id});

            return Redirect("/");
        
        
        
        }
    }
}
