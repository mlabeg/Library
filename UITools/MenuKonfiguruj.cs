using MenuUITools;
using System;
using System.Collections;
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

        public void Konfiguruj(IEnumerable<string> elementyMenu)
        {
            var type = elementyMenu.GetType();

            if (type.IsArray)
            {
                _menu.elementy = (string[])elementyMenu;
            }
            else
            {
                _menu.elementy = elementyMenu.ToArray();
            }

            for (int i = 0; i < _menu.elementy.Length; i++)
            {
                if (_menu.elementy[i].Length > _menu.najdluzszyElement)
                {
                    _menu.najdluzszyElement = _menu.elementy[i].Length;
                }
            }
        }
    }
}