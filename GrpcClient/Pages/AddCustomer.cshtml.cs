using GrpcClient.Models;
using GrpcServer.Protos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.Common;

namespace GrpcClient.Pages
{
    public class AddCustomerModel : PageModel
    {

        [BindProperty]
        public CustomerInput Input { set; get; }
        
        public async Task<ActionResult> OnPost()
        {
            if (!ModelState.IsValid) {
                return Page();
            }

            var client = CustomerClientSingleton.GetCustomerClient();
             await client.AddCustomerAsync(new CustomerModel { 
                CustomerName= Input.CustomerName,
                EmailAddress = Input.CustomerEmail,
                Age= Input.CustomerAge
            });

            return Redirect("/");


        }
    }
}
