using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace EBookLib
{
    // Класс Magazine, созданный в соответствии с условием
    public class Magazine : PrintEdition
    {
        int _period;
        // Конструктор класса Magazine
        public Magazine(string name, int pages, int period) : base(name, pages)
        {
            if (period < 1)
            {
                throw new ArgumentException("Поданое значение для period неположительно, журнал будет пересоздан");
            }
            _period = period;
        }
        // Переопределенный метод ToString() для Magazine
        public override string ToString()
        {
            return $"name={_name}; pages={_pages}; period={_period}.";
        }
    }
}
