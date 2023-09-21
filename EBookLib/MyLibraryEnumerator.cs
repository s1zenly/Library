using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBookLib
{
    // Реализация MyLibraryEnumerator<U>
    internal class MyLibraryEnumerator<U> : IEnumerator<U>
    {
        int index = -1;
        List<U> list = new List<U>();

        // Получение объекта, на который указывает индекс
        public U Current
        {
            get
            {
                return list[index];
            }

        }

        object IEnumerator.Current => Current;

        // Коструктор для MyLibraryEnumerator<U>
        public MyLibraryEnumerator(List<U> list)
        {
            this.list = new(list);
        }

        // Возвращения индекса в изначальное положение
        public void Reset()
        {
            index = -1;
        }

        // Продвижение по списку вперед
        public bool MoveNext()
        {
            if (index == list.Count - 1)
            {
                Reset();
                return false;
            }

            index++;
            return true;
        }

        public void Dispose()
        {

        }

        // Получение энумератора
        public IEnumerator<U> GetEnumerator()
        {
            return this;
        }
    }
}
