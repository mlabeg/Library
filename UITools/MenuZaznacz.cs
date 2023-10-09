using MenuUITools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Gebal.UITools
{
    internal class MenuZaznacz : IMenuZaznacz
    {
        private readonly Menu _menu;

        public MenuZaznacz(Menu menu)
        {
            _menu = menu;
        }

        public bool[] Zaznacz()
        {
            bool[] wybrane = new bool[_menu.elementy.Length];

            int wybrany = 0;
            bool przerwijPetle = false;
            if (_menu.elementy != null)
            {
                ConsoleKeyInfo keyInfo;
                Console.BackgroundColor = ConsoleColor.DarkBlue;

                do
                {
                    Console.SetCursorPosition(0, 0);
                    for (int i = 0; i < _menu.elementy.Length; i++)
                    {
                        if (wybrane[i] && wybrany == i)
                        {
                            Console.BackgroundColor = ConsoleColor.Green;
                        }
                        else if (wybrane[i])
                        {
                            Console.BackgroundColor = ConsoleColor.DarkGreen;
                        }
                        else if (i == wybrany)
                        {
                            Console.BackgroundColor = ConsoleColor.Blue;
                        }
                        else
                        {
                            Console.BackgroundColor = ConsoleColor.DarkBlue;
                        }
                        Console.WriteLine(_menu.elementy[i].PadRight(_menu.najdluzszyElement + 2));
                    }
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.WriteLine("\nNaciśnij \"K\" aby zatwierdzić wybór.");

                    keyInfo = Console.ReadKey();

                    if ((keyInfo.Key == ConsoleKey.UpArrow && wybrany == 0) || keyInfo.Key == ConsoleKey.End)
                    {
                        wybrany = _menu.elementy.Length - 1;
                    }
                    else if (keyInfo.Key == ConsoleKey.UpArrow && wybrany > 0)
                    {
                        wybrany--;
                    }
                    else if ((keyInfo.Key == ConsoleKey.DownArrow && wybrany == _menu.elementy.Length - 1) || keyInfo.Key == ConsoleKey.Home)
                    {
                        wybrany = 0;
                    }
                    else if (keyInfo.Key == ConsoleKey.DownArrow)
                    {
                        wybrany++;
                    }
                    else if (keyInfo.Key == ConsoleKey.Enter)
                    {
                        wybrane[wybrany] = !wybrane[wybrany];
                    }
                    else if (keyInfo.Key == ConsoleKey.K)
                    {
                        Console.WriteLine("\nCzy zwrócić wybrane pozycje z zamówienia? [TAK/NIE]");
                        string choice = Console.ReadLine();
                        if (String.Compare(choice, "TAK", true) == 0)
                        {
                            przerwijPetle = true;
                        }
                    }
                    else if (keyInfo.Key == ConsoleKey.Escape)
                    {
                        wybrany = -1;
                    }
                } while (!przerwijPetle && keyInfo.Key != ConsoleKey.Escape);
            }
            Console.ResetColor();

            return wybrane;
        }
    }
}