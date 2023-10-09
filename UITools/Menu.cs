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

        //Na ten moment nie wiem jak zbić to w jedną metodę, refaktor jak na coś wpadniesz
        public void Konfiguruj(string[] elementyMenu)
        {
            _menuKonfiguruj.Konfiguruj(elementyMenu);//System.NullReferenceException
        }

        public void Konfiguruj(List<string> lista)
        {
            _menuKonfiguruj.Konfiguruj(lista);
            _menuUaktualnij.Uaktualnij(elementy);
        }

        //UP typy generyczne?

        public void Uaktualnij(string[] elementyMenu)
        {
            _menuUaktualnij.Uaktualnij(elementyMenu);
        }

        public int Wyswietl(int? wiersz = null)
        {
            if (wiersz.HasValue)
            {
                return _menuWyswietl.Wyswietl(wiersz.Value);
            }
            else
            {
                return _menuWyswietl.Wyswietl();
            }
        }

        public bool[] Zaznacz()
        {
            return _menuZaznacz.Zaznacz();
        }
    }
}