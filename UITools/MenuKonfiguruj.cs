using MenuUITools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Gebal.UITools
{
    internal class MenuKonfiguruj : IMenuKonfiguruj
    {
        private readonly Menu _menu;

        public MenuKonfiguruj(Menu menu)
        {
            _menu = menu;
        }

        public void Konfiguruj(string[] elementyMenu)
        {
            if (elementyMenu.Length <= 100)
            {
                _menu.elementy = elementyMenu;
                for (int i = 0; i < _menu.elementy.Length; i++)
                {
                    if (elementyMenu[i].Length > _menu.najdluzszyElement)
                    {
                        _menu.najdluzszyElement = elementyMenu[i].Length;
                    }
                }
            }
            else
            {
                _menu.elementy = new string[0];
            }
        }

        public void Konfiguruj(List<string> lista)
        {
            _menu.elementy = lista.ToArray();
        }
    }
}