using Library.Persistence;
using MenuUITools;
using System;

namespace Library.ConsoleApp
{
    public class Program
    {
        private static void Main(string[] args)
        {
            BooksRepository bookRepository = new BooksRepository();
            BooksService booksService = new BooksService(bookRepository);
            OrdersRepository ordersRepository = new OrdersRepository();
            OrderService orderService = new OrderService(ordersRepository, bookRepository);
            LibrarySeeder librarySeeder = new LibrarySeeder(bookRepository, ordersRepository);

            Menu menu = new Menu();
            menu.Konfiguruj(new string[] { "Dodaj", "Usuń", "Lista książek",
                "Dodaj zamowienie", "Lista zamowien",
                "Zwrot zamowienia", "Wyjdz" });

            int inputCommand;
            do
            {
                Console.Clear();
                inputCommand = menu.Wyswietl();

                if (inputCommand >= 0)
                {
                    switch (inputCommand)
                    {
                        case 0:                         //dodaj książkę
                            Console.WriteLine("proba dodania ksiazki");
                            booksService.AddBook();
                            break;

                        case 1:                         //usun książkę
                            booksService.Remove();
                            Console.ReadKey();
                            break;
                        //TODO sprawdzić poprawność działania funkcji, po wybraniu "nie" książka i tak jest usuwana

                        case 2:                         //lista książek
                            if (!booksService.ListBooks())
                            {
                                Console.WriteLine("Brak książek w repozytorium!");
                            }
                            //Console.ReadKey();
                            break;

                        case 3:                         //dodaj zamówienie
                            if (orderService.PlaceOrder())
                            {
                                Console.WriteLine("pomyślnie dodano zamówienie!");
                            }
                            else
                            {
                                Console.WriteLine("Błąd przy dodawaniu zamówienia!");
                            }
                            Console.ReadKey();
                            break;

                        case 4:                         // wyświetl wszystkie zamówienia
                            if (!orderService.ListAll())
                            {
                                Console.WriteLine("Brak pozycji do wyświetlenia!");
                            }
                            Console.ReadKey();
                            break;

                        case 5:                         //zwrot
                            if (ordersRepository.GetCount() == 0)
                            {
                                Console.WriteLine("Brak zamówień w repozytorium!");
                                Console.ReadKey();
                            }
                            else
                            {
                                orderService.ReturnOrder();
                            }
                            break;

                        case 6:                         //wyjscie
                            break;

                        default:
                            Console.WriteLine("Niepoprawna komenda, spróbuj jeszcze raz.");
                            break;
                    }
                }
            } while (!(inputCommand == -1 || inputCommand == 6));
        }
    }
}