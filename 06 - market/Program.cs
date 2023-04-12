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
            const string Shopping = "1";
            const string ProductsList = "2";
            const string ShoppingList = "3";
            const string ExitProgram = "4";

            bool programIsWork = true;
            Buyer buyer = new Buyer();
            Salesman salesman = new Salesman();

            while (programIsWork)
            {
                Console.Clear();
                Console.SetCursorPosition(0, 0);
                Console.WriteLine("Выбирите действие: ");
                Console.WriteLine($"{Shopping})Покупка\n{ProductsList})Список продоваемых вещей\n{ShoppingList})Список купленных вещей\n{ExitProgram})Выход");
                string userInput = Console.ReadLine();

                
                switch (userInput)
                {
                    case Shopping:
                        Pay(salesman, buyer);
                        break;

                    case ProductsList:
                        salesman.ShowProducts();
                        break;

                    case ShoppingList:
                        buyer.ShowProducts();
                        break;

                    case ExitProgram:
                        programIsWork = false;
                        break;
                }
            }
        }

        public void Pay(Salesman salesman, Buyer buyer)
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

    class Buyer
    {
        private List<Product> _products = new List<Product>();
        private int _wallet = 1000;

        public void TakeProduct(Product product)
        {
            if (_wallet >= product.Price())
            {
                _products.Add(product);
                Console.Write("Вы Купили: ");
                _wallet -= product.Price();
                _products.Last().ShowInfo();
                Console.Write("\nДля продолжения нажмите любую кнопку:");                
            }
            else
            {
                Console.WriteLine("Недостаточно денег");
            }

            Console.ReadKey();           
        }             

        public void ShowProducts()
        {
            Console.WriteLine("Остаток денег:" + _wallet);

            foreach (Product products in _products)
            {                
                products.ShowInfo();
            }

            Console.ReadKey();
        }   
    }

    class Salesman
    {
        private List<Product> _products = new List<Product>();

        public Salesman()
        {
            ListProducts();
        }

        public Product GiveProduct(int userInput)
        {
            Product product = _products[userInput];
            return product;
        }

        public void ShowProducts()
        {
            foreach (Product products in _products)
            {
                products.ShowInfo();
            }

            Console.ReadKey();
        }

        public int LengthList()
        {
            return _products.Count;
        }

        private void ListProducts()
        {
            _products.Add(new Product("Картошка", 49));
            _products.Add(new Product("Капуста", 60));
            _products.Add(new Product("Морковь", 48));
            _products.Add(new Product("Лук", 31));
            _products.Add(new Product("Свекла", 45));
            _products.Add(new Product("Колбаса", 120));
            _products.Add(new Product("Хлеб", 17));
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

        public int Price()
        {
            return _price;
        }        
    }
}