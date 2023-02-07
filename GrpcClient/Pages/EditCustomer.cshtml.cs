using GrpcClient.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using GrpcServer.Protos;
using Grpc.Core;

namespace GrpcClient.Pages
{
    public class EditCustomerModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        [BindProperty]
        public CustomerInput Input {set;get;}


        public async Task<IActionResult> OnGet()
        {
            var client = CustomerClientSingleton.GetCustomerClient();

            try
            {
                var customer = await client.GetCustomerInfoAsync(new CustomerLookUpModel { CustomerId = Id });
                Input = new CustomerInput
                {
                    CustomerName = customer.CustomerName,
                    CustomerAge = customer.Age,
                    CustomerEmail = customer.EmailAddress
                };
                return Page();
            }
            catch (RpcException ex)
            {
                return NotFound(ex.Status);

            }
        }

        public async Task<IActionResult> OnPost() {
            if (!ModelState.IsValid) {
                return Page();
            }
            var client = CustomerClientSingleton.GetCustomerClient();
           await client.EditCustomerAsync(new CustomerModel {
                CustomerId=Id,
                CustomerName=Input.CustomerName,
                Age=Input.CustomerAge,
                EmailAddress=Input.CustomerEmail

            
            });
            return Redirect("/");


        }
            
            


    }
}
