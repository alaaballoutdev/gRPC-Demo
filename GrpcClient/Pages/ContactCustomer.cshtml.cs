using Grpc.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GrpcClient.Pages
{
    public class ContactCustomerModel : PageModel
    {
        [BindProperty(SupportsGet =true)]
        public int Id { get; set; }
        
        public async Task<ActionResult> OnGet()
        {
            try
            {
                var client = CustomerClientSingleton.GetCustomerClient();

                var email = await client.GetCustomerEmailAsync(new GrpcServer.Protos.CustomerLookUpModel { CustomerId = Id });

                return Redirect("mailto:" + email.Email);

                

            }
            catch (RpcException ex)
            {

                return NotFound(ex.Status);

            }


        }
    }
}
