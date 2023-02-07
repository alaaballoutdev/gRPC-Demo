using Grpc.Net.Client;
using GrpcServer.Protos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GrpcClient.Pages
{
    public class IndexModel : PageModel
    {

        public List<CustomerModel> customerlist { get; set; } = new List<CustomerModel>(); 

        public IndexModel()
        {
            
        }



        public async Task OnGet()
        {
            var client = CustomerClientSingleton.GetCustomerClient();


            var customers = await client.GetCustomersAsync(new Empty { });
            customerlist.AddRange(customers.CustomerModels);
            
            

        }
    }
}