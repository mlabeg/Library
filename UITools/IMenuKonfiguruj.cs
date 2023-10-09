using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gebal.UITools
{
    internal interface IMenuKonfiguruj
    {
        void Konfiguruj(string[] elementyMenu);

        void Konfiguruj(List<string> lista);
    }
}