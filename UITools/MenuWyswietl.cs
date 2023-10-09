using MenuUITools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Gebal.UITools
{
    internal class MenuWyswietl : IMenuWyswietl
    {
        private readonly Menu _menu;

        public MenuWyswietl(Menu menu)
        {
            _menu = menu;
        }

        public int Wyswietl()
        {
            // Console.Clear();	//używając metody Wyswietl() pamiętać, żeby przed wywołaniem użyć Console.Clear();
            int wybrany = 0;
            if (_menu.elementy != null)
            {
                ConsoleKeyInfo keyInfo;
                Console.BackgroundColor = ConsoleColor.DarkBlue;

                do
                {
                    Console.SetCursorPosition(0, 0);
                    for (int i = 0; i < _menu.elementy.Length; i++)
                    {
                        if (i == wybrany)
                        {
                            Console.BackgroundColor = ConsoleColor.Blue;
                        }
                        else
                        {
                            Console.BackgroundColor = ConsoleColor.DarkBlue;
                        }
                        Console.WriteLine(_menu.elementy[i].PadRight(_menu.najdluzszyElement + 2));
                    }

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
                    else if (keyInfo.Key == ConsoleKey.Escape)
                    {
                        wybrany = -1;
                    }
                } while (keyInfo.Key != ConsoleKey.Enter && keyInfo.Key != ConsoleKey.Escape);
            }
            Console.ResetColor();
            return wybrany;
        }

        public int Wyswietl(int wiersz)
        {
            //-1 - kod wyjścia
            if (wiersz == -1) return -1;
            int wybrany = 0;
            if (_menu.elementy != null)
            {
                ConsoleKeyInfo keyInfo;
                Console.BackgroundColor = ConsoleColor.DarkBlue;

                do
                {
                    for (int i = 0; i < _menu.elementy.Length; i++)
                    {
                        if (i == wybrany)
                        {
                            Console.BackgroundColor = ConsoleColor.Blue;
                        }
                        else
                        {
                            Console.BackgroundColor = ConsoleColor.DarkBlue;
                        }
                        Console.SetCursorPosition(0, wiersz + i);
                        Console.WriteLine(_menu.elementy[i].PadRight(_menu.najdluzszyElement + 2));
                    }

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
                    else if (keyInfo.Key == ConsoleKey.Escape)
                    {
                        wybrany = -1;
                    }
                } while (keyInfo.Key != ConsoleKey.Enter && keyInfo.Key != ConsoleKey.Escape);
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Brak pozycji do wyświetleia!");
                Console.ReadKey();
            }
            Console.ResetColor();
            return wybrany;
        }
    }
}