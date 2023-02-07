using Grpc.Core;
using GrpcServer.Models;
using GrpcServer.Protos;
using System.Linq.Expressions;
using System.Net.Sockets;
using System.Runtime.InteropServices;

namespace GrpcServer.Services
{
    public class CustomersService : GrpcServer.Protos.Customer.CustomerBase
    {
        private readonly ILogger<CustomersService> _logger;
        private readonly CustomerDb _context;
        public CustomersService(ILogger<CustomersService> logger, CustomerDb context)
        {
            _logger = logger;
            _context = context;
        }

        public override Task<CustomerModel> GetCustomerInfo(CustomerLookUpModel request, ServerCallContext context)
        {

            var customer = _context.Customers
                 .FirstOrDefault(c => c.CustomerId == request.CustomerId);
            if (customer != null)
            {
                return Task.FromResult(new CustomerModel
                {
                    CustomerId = customer.CustomerId,
                    CustomerName = customer.CustomerName,
                    Age = customer.Age,
                    EmailAddress = customer.EmailAddress,
                });
            }
            throw new RpcException(
                new Status(StatusCode.NotFound, "Customer Not Found"),
                new Metadata {
                    { "customerId", request.CustomerId.ToString()}
                });


        }

        public override Task<CustomerList> GetCustomers(Empty request, ServerCallContext context)
        {
            var customers = _context.Customers.Select(c =>
             new CustomerModel
             {
                 CustomerId = c.CustomerId,
                 CustomerName = c.CustomerName,
                 Age = c.Age,
                 EmailAddress = c.EmailAddress

             }

         ).ToList();
           CustomerList customerList = new CustomerList();
            customerList.CustomerModels.AddRange(customers);
            return Task.FromResult(customerList);
        }

        public override Task<CustomerSalesResponse> GetCustomerSales(CustomerLookUpModel request, ServerCallContext context)
        {
            var customerName = _context.Customers
                .Where(c => c.CustomerId == request.CustomerId)
                .Select(c => c.CustomerName)
                .SingleOrDefault();

            if (customerName == null) {

                throw new RpcException(
                new Status(StatusCode.NotFound, "Customer Not Found"),
                new Metadata {
                    { "customerId", request.CustomerId.ToString()}
                });

            }

            var sales = _context.Sales
                .Where(sale=>sale.CustomerId==request.CustomerId)
                .Select(sale=>
                    new SaleModel { 
                        SaleId= sale.SaleId,
                        Item=sale.Item,
                        SaleDate=sale.SaleDate,
                        Price=sale.Price

                    
                    }
                )
                .ToList();
            
            CustomerSalesResponse customerSalesResponse = new CustomerSalesResponse();

            customerSalesResponse.CustomerSales.AddRange(sales);
            customerSalesResponse.CustomerName=customerName;

            return Task.FromResult(customerSalesResponse);  

        }

        public override Task<SaleList> GetSales(Empty request, ServerCallContext context)
        {
            var sales = _context.Sales
                    .Select(sale=> new SaleModel { 
                        SaleId= sale.SaleId,
                        CustomerId=sale.CustomerId,
                        Item= sale.Item,
                        Price=sale.Price,
                        SaleDate=sale.SaleDate

                       
                    
                    })
                    .ToList();


            SaleList response = new SaleList();

            response.Sales.AddRange(sales);
            return Task.FromResult(response);
            
        }

        public override Task<Empty> AddCustomer(CustomerModel request, ServerCallContext context)
        {
            Models.Customer customer = new Models.Customer
            {
                CustomerName = request.CustomerName,
                EmailAddress = request.EmailAddress,
                Age = request.Age
            };

            _context.Customers.Add(customer);
            _context.SaveChanges();
            return Task.FromResult(new Empty());
        }

        public override  Task<Empty> EditCustomer(CustomerModel request, ServerCallContext context)
        {
            _context.Customers
                .Update(new Models.Customer
                {
                    CustomerId = request.CustomerId,
                    CustomerName = request.CustomerName,
                    Age = request.Age,
                    EmailAddress = request.EmailAddress

                });
            _context.SaveChanges();

            return Task.FromResult(new Empty());


        }

        public override Task<Empty> DeleteCustomer(CustomerLookUpModel request, ServerCallContext context)
        {
            var sales = _context.Sales
                .Where(sale=>sale.CustomerId==request.CustomerId)
                .ToList();
            _context.Sales.RemoveRange(sales);
            
           _context.SaveChanges();

            var customer = _context.Customers
                .Where(customer => customer.CustomerId==request.CustomerId).FirstOrDefault();
            if (customer != null)
            {
                _context.Customers.Remove(customer);
                _context.SaveChanges();

            }

            return Task.FromResult(new Empty { });



        }

        public override Task<CustomerEmail> GetCustomerEmail(CustomerLookUpModel request, ServerCallContext context)
        {
            var email = _context.Customers
                .Where(customer=>customer.CustomerId==request.CustomerId)
                .Select(customer=>customer.EmailAddress)
                .FirstOrDefault();
            if (email ==null) {
                throw new RpcException(
                new Status(StatusCode.NotFound, "Customer Not Found"),
                new Metadata {
                    { "customerId", request.CustomerId.ToString()}
                });}
            return Task.FromResult(new CustomerEmail { Email=email });
            
            
        }

        public override Task<Empty> AddSale(SaleModel request, ServerCallContext context)
        {
            Sale sale = new Sale {
                Item= request.Item,
                CustomerId=request.CustomerId,
                Price= request.Price,
                SaleDate=request.SaleDate
            
            };

            _context.Sales.Add(sale);

            _context.SaveChanges();

            return Task.FromResult(new Empty { });
             
        }

        public override Task<SaleModel> GetSaleInfo(SaleLookUp request, ServerCallContext context)
        {
            var sale = _context.Sales.Where(sale => sale.SaleId == request.SaleId).FirstOrDefault();

            if (sale == null)
            {
                throw new RpcException(
               new Status(StatusCode.NotFound, "Sale Not Found"),
               new Metadata {
                    { "SaleId", request.SaleId.ToString()}
               });

            }
            SaleModel saleModel= new SaleModel { 
               SaleId=request.SaleId,
               CustomerId=sale.CustomerId, 
                Price=sale.Price,
                Item = sale.Item,
                SaleDate = sale.SaleDate
            };

            return Task.FromResult(saleModel);
        }
        public override Task<Empty> UpdateSale(SaleModel request, ServerCallContext context)
        {
            Sale sale = new Sale
            {
                SaleId = request.SaleId,
                SaleDate = request.SaleDate,
                CustomerId = request.CustomerId,
                Item = request.Item,
                Price = request.Price

            };
            
            _context.Sales.Update(sale);
            _context.SaveChanges();

            return Task.FromResult(new Empty { });
        }





        public override Task<Empty> DeleteSale(SaleLookUp request, ServerCallContext context)
        {
            var sale = _context.Sales.Where(sale => request.SaleId == sale.SaleId).FirstOrDefault();

            if (sale != null) {
                _context.Sales.Remove(sale);
                _context.SaveChanges();

            
            }

            return Task.FromResult(new Empty { });
           
        }







    }
}
