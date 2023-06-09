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

        private void Trade(Salesman salesman, Buyer buyer)
        {
            Console.WriteLine("Введите номер товара:");
            int.TryParse(Console.ReadLine(), out int userInput);
            userInput -= 1;

            if (userInput >= 0 && userInput < salesman.GetProductsCount())
            {                
                buyer.TakeProduct(salesman.GiveProduct(userInput , salesman));
            } 
            else 
            {
                Console.WriteLine("Не верный номер товара"); 
            }

            Console.ReadKey();
        }
    }

    class Human
    {
        private List<Product> _products = new List<Product>();
        protected int Wallet;

        public Human(int wallet)
        {
            Wallet = wallet;
        }        

        public int GetProductsCount()
        {
           return _products.Count;
        }               

        public void ShowProducts()
        {
            Console.WriteLine("В кошельке: " + Wallet + "руб.\n");

            foreach (Product products in _products)
            {
                products.ShowInfo();
            }

            Console.ReadKey();
        }

        protected void AddProduct(Product product)
        {
            _products.Add(product);
        }

        protected Product GetProductByIndex(int index)
        {
            return _products.ElementAt(index);
        }
    }

    class Buyer : Human
    {
        public Buyer() : base(1000) { }

        public void TakeProduct(Product product)
        {
            if (Wallet >= product.Price)
            {
                AddProduct(product);                
                Wallet -= product.Price;
                Console.Write($"Вы Купили: {product.Name} за {product.Price} руб. Остаток: {Wallet}");
                Console.Write("\nДля продолжения нажмите любую кнопку:");
            }
            else
            {
                Console.WriteLine("Недостаточно денег");
            }        
        }           
    }

    class Salesman : Human
    {
        public Salesman() : base(0) 
        {
            ListProducts();
        }

        public Product GiveProduct(int userInput, Salesman salesman)
        { 
            Product product = salesman.GetProductByIndex(userInput);
            Wallet += product.Price;
            return product;
        }

        private void ListProducts()
        {
            AddProduct(new Product("Картошка", 49));
            AddProduct(new Product("Капуста", 60));
            AddProduct(new Product("Морковь", 48));
            AddProduct(new Product("Лук", 31));
            AddProduct(new Product("Свекла", 45));
            AddProduct(new Product("Колбаса", 120));
            AddProduct(new Product("Хлеб", 17));  
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

        public int Price
        {
            get
            {
                return _price;
            }
            private set
            {
                _price = value;
            }
        }
        public string Name
        {
            get
            {
                return _name;
            }
            private set
            {
                _name = value;
            }
        }

        public void ShowInfo()
        {  
            Console.WriteLine($"{_name}. Цена = {_price}");
        }        
    }
}
