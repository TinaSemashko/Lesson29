using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.SqlServer;

public class Program
{
	public static void Main()
	{
		using (UserContext db = new UserContext())
        {

            Customers customer1 = new Customers { Name = "Tom" };
            Customers customer2 = new Customers { Name = "Alice" };
            Customers customer3 = new Customers { Name = "Bob" };

            Employees employe1 = new Employees { Name = "Operator1" };

            Products product1 = new Products { Name = "Candy" };
            Products product2 = new Products { Name = "Cookis" };
            Products product3 = new Products { Name = "Chokolad" };

            Products productOrder1 = product1;
            Orders order1 = new Orders { Number = "1", Customer = customer1, Employe = employe1, Product = productOrder1 };

            Products productOrder2 = product3;
            Orders order2 = new Orders { Number = "2", Customer = customer2, Employe = employe1, Product = productOrder2 };

            db.Customers.Add(customer1);
            db.Customers.Add(customer2);
            db.Customers.Add(customer3);

            db.Employees.Add(employe1);

            db.Products.Add(product1);
            db.Products.Add(product2);
            db.Products.Add(product3);

            db.Orders.Add(order1);
            db.Orders.Add(order2);

            db.SaveChanges();
            Console.WriteLine("Объекты успешно сохранены");

            // получаем объекты из бд и выводим на консоль
            var customers = db.Customers.ToList();
            var employees = db.Employees.ToList();
            var orders = db.Orders.ToList();
            var products = db.Products.ToList();


            var users = db.Users.ToList();
            Console.WriteLine("Список объектов:");
            foreach (User u in users)
            {
                Console.WriteLine($"{u.Id}.{u.Name} - {u.Age}");
            }


            Console.WriteLine("Список объектов:");
            foreach (Customers c in customers)
            {
                Console.WriteLine($"{ c.Id}.{c.Name}");
            }

            foreach (Employees e in employees)
            {
                Console.WriteLine($"{e.Id}.{e.Name}");
            }

            foreach (Products p in products)
            {
                Console.WriteLine($"{p.Id}.{p.Name}");
            }

            foreach (Orders o in orders)
            {
                Console.WriteLine($"{o.Id}.{o.Number}.{o.Customer}.{o.Employe}.{o.Product}");
            }

        }
        Console.Read();
    }
}

public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
}

public class UserContext : DbContext
{
     public DbSet<Customers> Customers { get; set; }
    public DbSet<Employees> Employees { get; set; }
    public DbSet<Products> Products { get; set; }
    public DbSet<Orders> Orders { get; set; }

    public UserContext()
    {
        Database.EnsureCreated();
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=myshop;Trusted_Connection=True;");
    }
}

public class Customers
{
	public int Id { get; set; }
	public string Name { get; set; }
}

public class Employees
{
	public int Id { get; set; }
	public string Name { get; set; }
}

public class Products
{
	public int Id { get; set; }
	public string Name { get; set; }
}

public class Orders
{
	public int Id { get; set; }
	public string Number { get; set; }
	public Customers Customer { get; set; }
	public Employees Employe { get; set; }
	public Products Product { get; set; }
}
//checked
//it would be better to use Entity (such as Employee) in single, to avoid misunderstanding between single entity and multiple entities
