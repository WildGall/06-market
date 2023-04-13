using System;
using System.Collections.Generic;
using System.Linq;

namespace _06___market
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Market market = new Market();
            market.Work();
        }
    } 
        
    class Market
    {
        public void Work()
        {
            const string CommandShopping = "1";
            const string CommandProductsList = "2";
            const string CommandShoppingList = "3";
            const string CommandExitProgram = "4";

            bool isProgramWork = true;
            Buyer buyer = new Buyer();
            Salesman salesman = new Salesman();

            while (isProgramWork)
            {
                Console.Clear();
                Console.SetCursorPosition(0, 0);
                Console.WriteLine("Выбирите действие: ");
                Console.WriteLine($"{CommandShopping})Покупка\n{CommandProductsList})Список продaваемых вещей\n{CommandShoppingList})Список купленных вещей\n{CommandExitProgram})Выход");
                string userInput = Console.ReadLine();

                
                switch (userInput)
                {
                    case CommandShopping:
                        Trade(salesman, buyer);
                        break;

                    case CommandProductsList:
                        salesman.ShowProducts();
                        break;

                    case CommandShoppingList:
                        buyer.ShowProducts();
                        break;

                    case CommandExitProgram:
                        isProgramWork = false;
                        break;
                }
            }
        }

        public void Trade(Salesman salesman, Buyer buyer)
        {
            Console.WriteLine("Введите номер товара:");
            int.TryParse(Console.ReadLine(), out int userInput);
            userInput -= 1;

            if (userInput > 0 && userInput < salesman.LengthList())
            {
                buyer.TakeProduct(salesman.GiveProduct(userInput));
            } 
            else { Console.WriteLine("Не верный номер товара"); }

            Console.ReadKey();
        }
    }

    class Human
    {
        public List<Product> Products = new List<Product>();
        public int Wallet;

        public Human(int wallet)
        {
            Wallet = wallet;
        }            

        public void ShowProducts()
        {
            Console.WriteLine("В кошельке: " + Wallet + "руб.\n");

            foreach (Product products in Products)
            {
                products.ShowInfo();
            }

            Console.ReadKey();
        }
    }

    class Buyer : Human
    {
        public Buyer() : base(1000) { }

        public void TakeProduct(Product product)
        {
            if (Wallet >= product.AppointPrice())
            {
                Products.Add(product);
                Console.Write("Вы Купили: ");
                Wallet -= product.AppointPrice();
                Products.Last().ShowInfo();
                Console.Write("\nДля продолжения нажмите любую кнопку:");                
            }
            else
            {
                Console.WriteLine("Недостаточно денег");
            }

            Console.ReadKey();           
        }           
    }

    class Salesman : Human
    {
        public Salesman() : base(0) 
        {
            ListProducts();
        }

        public Product GiveProduct(int userInput)
        {
            Product product = Products[userInput];
            Wallet += product.AppointPrice();
            return product;
        }

        public int LengthList()
        {
            return Products.Count;
        }

        private void ListProducts()
        {
            Products.Add(new Product("Картошка", 49));
            Products.Add(new Product("Капуста", 60));
            Products.Add(new Product("Морковь", 48));
            Products.Add(new Product("Лук", 31));
            Products.Add(new Product("Свекла", 45));
            Products.Add(new Product("Колбаса", 120));
            Products.Add(new Product("Хлеб", 17));
        }
    }

    class Product
    {
        private string _name;
        private int _price;

        public Product(string name, int price)
        {
            _name = name;
            _price = price;
        }

        public void ShowInfo()
        {  
            Console.WriteLine($"{_name}. Цена = {_price}");
        }

        public int AppointPrice()
        {
            return _price;
        }        
    }
}
