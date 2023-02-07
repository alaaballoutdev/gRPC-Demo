
using Grpc.Net.Client;
using GrpcServer.Protos;


namespace GrpcClient
{
    public class CustomerClientSingleton
    {
        private static Customer.CustomerClient client;

        public static Customer.CustomerClient GetCustomerClient() {
            if (client == null) {
                var channel = GrpcChannel.ForAddress("https://localhost:7137");
                client = new Customer.CustomerClient(channel);
            }
            return client;
        
        
        }




    }
}
