using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBookLib
{
    // Класс Book, созданый в соответствии с условием
    public class Book : PrintEdition
    {
        string _author;
        // Конструктор класса Book
        public Book(string name, int pages, string author) : base(name, pages)
        {
            _author = author;
        }
        // Переопределенный метод ToString() для Book
        public override string ToString()
        {
            return $"name={_name}; pages={_pages}; author={_author}.";
        }
    }
}
