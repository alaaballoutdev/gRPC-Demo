using GrpcServer.Protos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GrpcClient.Pages
{
    public class SalesModel : PageModel
    {
        
        public List<SaleModel> salesList { set; get; }= new List<SaleModel>();

        
        public async Task OnGet()
        {
            var client = CustomerClientSingleton.GetCustomerClient();
            SaleList saleList = await client.GetSalesAsync(new Empty { });
            salesList.AddRange(saleList.Sales);
        }
    }
}
