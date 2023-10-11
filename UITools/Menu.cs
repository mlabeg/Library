using Gebal.UITools;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace MenuUITools
{
    public class Menu
    {
        internal String[] elementy;
        public int najdluzszyElement;

        private readonly IMenuZaznacz _menuZaznacz;
        private readonly IMenuWyswietl _menuWyswietl;
        private readonly IMenuUaktualnij _menuUaktualnij;
        private readonly IMenuKonfiguruj _menuKonfiguruj;

        public Menu()
        {
            najdluzszyElement = 0;

            _menuZaznacz = new MenuZaznacz(this);
            _menuWyswietl = new MenuWyswietl(this);
            _menuUaktualnij = new MenuUaktualnij(this);
            _menuKonfiguruj = new MenuKonfiguruj(this);
        }

        public void Konfiguruj(IEnumerable<string> lista)
        {
            _menuKonfiguruj.Konfiguruj(lista);
            _menuUaktualnij.Uaktualnij(elementy);
        }

        public void Uaktualnij(string[] elementyMenu)
        {
            _menuUaktualnij.Uaktualnij(elementyMenu);
        }

        public int Wyswietl(int? wiersz = 0)
        {
            return _menuWyswietl.Wyswietl(wiersz.Value);
        }

        public bool[] Zaznacz()
        {
            return _menuZaznacz.Zaznacz();
        }
    }
}