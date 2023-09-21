using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EBookLib
{
    // Класс MyLibrary<T>, написанный в соостветствии с условием
    public class MyLibrary<T> : IEnumerable<T> where T : PrintEdition
    {
        public static Action<char> onTake;
        List<T> _library = new List<T>();
        // Свойство для получения средненего количества страниц в книгах
        public double averageNumberOfPagesInBooks
        {
            get
            {
                double pagesNumber = 0;
                double booksCount = 0;
                for (int i = 0; i < _library.Count; i++)
                {
                    if (_library[i] is Book)
                    {
                        pagesNumber += _library[i].GetPages();
                        booksCount++;
                    }
                }
                return pagesNumber / booksCount;
            }
        }
        // Свойство для получения среднего колечества страниц в журналах
        public double averageNumberOfPagesInMagazines
        {
            get
            {
                double pagesNumber = 0;
                double magazineCount = 0;
                for (int i = 0; i < _library.Count; i++)
                {
                    if (_library[i] is Magazine)
                    {
                        pagesNumber += _library[i].GetPages();
                        magazineCount++;
                    }
                }
                return pagesNumber / magazineCount;
            }
        }
        
        public MyLibrary()
        {

        }
        // Добавить новый элемент в экземпляр класса MyLibrary
        public void Add(T printed)
        {
            _library.Add(printed);
        }
        // Удаление книг, начинающихся с переданной буквы
        public void TakeBooks(char start)
        {
            for (int i = 0; i < _library.Count; i++)
            {
                if (_library[i] is Book && _library[i].GetName()[0] == start)
                {
                    _library.RemoveAt(i);
                    if (i > 0)
                    {
                        i--;
                    }
                }
            }

            onTake(start);
        }
        // Вывод только хранимых книг
        public void CallPrintForBooks()
        {
            for(int i = 0; i < _library.Count; i++)
            {
                if (_library[i] is Book)
                {
                    _library[i].Print();
                }
            }
        }
        // Перевод содержания экземляра класса MyLibrary в текст
        public string ConvertMyLibraryToText()
        {
            string s = "";
            foreach (var item in _library)
            {
                s += item.ToString() + "\n";
            }
            return s;
        }
        // Вывод общего колчества страниц для всех печатных изданий и вывод всех печатных изданий
        public override string ToString()
        {
            string s = "";
            int pageNumber = 0;
            foreach (var item in _library)
            {
                pageNumber += item.GetPages();
            }
            s = $"Общее количество страниц во всех печатных изданиях: {pageNumber}\n";
            foreach (var item in _library)
            {
                s += item.ToString() + "\n";
            }
            return s;
        }
        // Индексатор для доступа к хранимому печатному изданию по индексу
        public PrintEdition this[int index]
        {
            get
            {
                return _library[index];
            }
        }
        // Код для получения IEnumerator
        public IEnumerator<T> GetEnumerator()
        {
            MyLibraryEnumerator<T> enumerator = new(_library);
            return enumerator.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            MyLibraryEnumerator<T> enumerator = new(_library);
            return enumerator.GetEnumerator();
        }

    }
}
