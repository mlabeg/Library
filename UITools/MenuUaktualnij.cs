using MenuUITools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Gebal.UITools
{
    internal class MenuUaktualnij : IMenuUaktualnij
    {
        private readonly Menu _menu;

        public MenuUaktualnij(Menu menu)
        {
            _menu = menu;
        }

        public void Uaktualnij(string[] elementyMenu)
        {
            _menu.elementy = elementyMenu;
        }
    }
}