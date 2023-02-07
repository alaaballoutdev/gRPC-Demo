using Grpc.Core;
using GrpcServer.Protos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GrpcClient.Pages
{
    public class ViewSalesModel : PageModel
    {
        [BindProperty(SupportsGet =true)]
        public int Id { get; set; }

        public CustomerSalesResponse CustomerSalesResponse { get; set; }
        
        public async Task<IActionResult> OnGet()
        {
            try
            {
                var client = CustomerClientSingleton.GetCustomerClient();
                CustomerSalesResponse = await client.GetCustomerSalesAsync(new CustomerLookUpModel { CustomerId = Id });
                return Page();
            }
            catch(RpcException ex) {
                return NotFound(ex.Status.ToString());
            
            }
        }
    }
}
