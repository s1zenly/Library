using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace EBookLib
{
    // Абстрактный класс PrintEdition, написанный в соостветсвии с условием
    public abstract class PrintEdition : IPrinting
    {
        protected string _name;
        protected int _pages;

        // Коструктор абстрактного класса PrintEdition
        public PrintEdition(string name, int page)
        {
            _name = name;
            if (page < 0)
            {
                throw new ArgumentException("Поданое значение страниц отрицательно, издание будет пересоздано");
            }
            _pages = page;
        }

        // Метод для получения имени (_name) экземпляра класса
        public string GetName()
        {
            return _name;
        }
        // Метод для получения количества страниц (_pages) экземпляра класса
        public int GetPages()
        {
            return _pages;
        }
        // Вызов PrintHandler() из Programm
        public void Print()
        {
            IPrinting.onPrint(this);
        }

        public override string ToString()
        {
            return $"name={_name}; pages={_pages}.";
        }
    }
}
