using Library.ConsoleApp;
using Library.Domain;
using Library.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Books.Commands
{
    internal class AddBook
    {
        private readonly BooksRepository _booksRepository;
        private readonly BooksService _booksService;

        public AddBook(BooksRepository booksRepository, BooksService booksService)
        {
            _booksRepository = booksRepository;
            _booksService = booksService;
        }

        public void Add()
        {
            Console.WriteLine("Podaj tytuł: ");
            string title = Console.ReadLine();
            Console.WriteLine("Podaj Autora: ");
            string author = Console.ReadLine();

            Console.WriteLine("Podaj rok wydania: ");
            int.TryParse(Console.ReadLine(), out int year);

            Console.WriteLine("Podaj numer ISBN");
            string isbn = Console.ReadLine();

            Console.WriteLine("Podaj ile jest dostepnych pozycji: ");
            int.TryParse(Console.ReadLine(), out int productsAvailable);

            decimal price = 0;
            int check;
            do
            {
                check = 0;
                try
                {
                    Console.WriteLine("Podaj cenę: ");
                    price = decimal.Parse(Console.ReadLine());
                }
                catch (FormatException)
                {
                    Console.WriteLine("Bład ceny! Użyj przecinka!");
                    check = 1;
                }
                catch (ArgumentNullException)
                {
                    Console.WriteLine("Bląd ceny!");
                    check = 1;
                }
            } while (check == 1);

            Book newBook = new Book(title, author, year, isbn, productsAvailable, price);
            _booksRepository.Insert(newBook);

            Console.WriteLine("Pomyślnie dodano książkę!");
        }
    }
}