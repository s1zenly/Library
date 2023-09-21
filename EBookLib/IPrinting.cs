using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBookLib
{
    // Интерфейс для обработки событий и вызова метода Print()
    public interface IPrinting
    {
        public static Action<PrintEdition> onPrint;
        public void Print();
    }
}
