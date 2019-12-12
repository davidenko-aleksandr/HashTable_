using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashTable
{   
    /// <summary>
    /// Элемент хранимых данных хэш таблицы
    /// </summary>
    public class Item
    {
        //Ключ
        public string Key { get; private set; }
        //Хранимые данные
        public string Value { get; private set; }
        //Создание нового экземпляра хранимых данных
        public Item(string key, string value)
        {
            //проверка введенных данных на корректность
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException(nameof(key));
            }
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException(nameof(value));
            }
            //Устанавливаем значения
            Key = key;
            Value = value;
        }
        //привидение объекта к строке
        public override string ToString()
        {
            return Key;
        }
        

    }
}
