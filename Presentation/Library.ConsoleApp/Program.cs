using MenuUITools;
using Library.Domain;
using Library.Persistence;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Library.ConsoleApp
{
	public class Program
	{
		static void Main(string[] args)
		{
			//Book book = new Book("Zamieć", "Neal Stephenson", 1992, "83-7150-531-0", 1, 29.50m);
			BooksRepository repository = new BooksRepository();
			BooksService booksService = new BooksService(repository);
			OrdersRepository ordersRepository = new OrdersRepository();
			OrderService orderService = new OrderService(ordersRepository, repository);

			Menu menu = new Menu();
			menu.Konfiguruj(new string[] { "Dodaj", "Usun", "Wypisz",
				"Zmien", "Dodaj zamowienie", "Lista zamowien",
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
						case 0:							//dodaj książkę
							Console.WriteLine("proba dodania ksiazki");
							booksService.AddBook();
							break;
						case 1:							//usun książkę
							booksService.Remove();
							Console.ReadKey();
							break;
						case 2:							//lista książek
							if (!booksService.ListBooks())
							{
								Console.WriteLine("Brak książek w repozytorium!");
							}
							Console.ReadKey();
							break;
						case 3:							//zmiana statusu książki
								//TODO ? albo refaktor albo usunąć
							Console.WriteLine("proba zmiany stanu magazynowego ksiazek");
							booksService.ChangeStat();
							break;
						case 4:							//dodaj zamówienie 
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
						case 5:							// wyświetl wszystkie zamówienia
							if (!orderService.ListAll())
							{
								Console.WriteLine("Brak pozycji do wyświetlenia!");
							}
							Console.ReadKey();
							break;

						case 6:							//zwrot
							orderService.ReturnOrder();
							//Console.ReadKey();
							//TODO 3.5 zamiana ReturnOrders(), aby zwracała bool

							break;
						case 7:							//wyjscie
							break;
						default:
							Console.WriteLine("Niepoprawna komenda, spróbuj jeszcze raz.");
							break;
					}
				}
			} while (!(inputCommand == -1 || inputCommand == 7));
		}

}
