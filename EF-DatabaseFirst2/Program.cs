using EF_DatabaseFirst2.Models;
using Microsoft.EntityFrameworkCore;


var ctx = new EFDatabaseFirst.DatabaseFirstContext();

//CREAR UN CUSTOMER
var newCustomer = new Customer()
{
    CustomerId = "Sebastian",
    CompanyName = "Seba Amaral",
    Orders = new List<Order>()
};

newCustomer.Orders.Add(new Order()
{
    CustomerId = "Sebastian",
    OrderDate = DateTime.Now,
});

// AGREGAMOS EL CUSTOMER
ctx.Add(newCustomer);
ctx.SaveChanges();


////EDITAR
//var reg = ctx.Customers.FirstOrDefault(r => r.CustomerId == "Sebastian");
//if (reg != null)
//{
//    reg.CompanyName = "Seba Amaral Editado";
//    ctx.SaveChanges();
//}

// ELIMINAR
//var regOrders = ctx.Orders.Where(r => r.CustomerId == "Sebastian");
//ctx.RemoveRange




var sqlRaw = ctx.Customers.FromSqlRaw("SELECT * FROM Customers");


Console.WriteLine("Lista de Customers");
Console.WriteLine();
Console.WriteLine("==========================================");

// SELECT
var customers = ctx.Customers;

foreach (var customer in customers)
{
    Console.WriteLine($"{customer.CustomerId} - {customer.CompanyName}");    
}

Console.WriteLine("Ingrese el IDCustomer a buscar");

string idCustomer = Console.ReadLine();


//Consulta si existe en la base de datos (Any)
bool anyCustomer = ctx.Customers.Any(c => c.CustomerId == idCustomer.ToUpper());

if (anyCustomer)
{
    Console.WriteLine("El Customer existe. Todo OK!");
    //SELECT TOP (1)
    var customer = ctx.Customers.Include(i => i.Orders)
                                .FirstOrDefault(c => c.CustomerId == idCustomer);
    Console.WriteLine();
    Console.WriteLine($"IdCustomer: {customer.CustomerId} - CompanyName: {customer.CompanyName}");
    Console.WriteLine();
    Console.WriteLine("Orders");
    foreach(var item in customer.Orders)
    {
        Console.WriteLine($"OrderId> {item.OrderId} - OrderDate: {item.OrderDate}");
    }
    Console.WriteLine("==========================================");
    Console.WriteLine();
}
else
{
    Console.WriteLine("El IdCustomer no existe en la BD.");

}
Console.ReadKey();