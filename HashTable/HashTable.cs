using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashTable
{
    /// <summary>
    /// Хэш таблица
    /// </summary>
    /// Используем метод цепочек (открытое хеширование)
    class HashTable
    {
        //максимальная длина ключа
        private readonly byte _maxSize = 255;
        //Коллекция хранимых данных
        private Dictionary<int, List<Item>> _items = null;
        /// <summary>
        /// Коллекция хранимых данных в хэш-таблице в виде пар хэш-значение 
        /// </summary>
        public IReadOnlyCollection<KeyValuePair<int, List<Item>>> Items => _items?.ToList()?.AsReadOnly();

        /// <summary>
        /// СОздание нового экземпляра класса Hash Table
        /// </summary>
        public HashTable()
        {
            //Инициализируем коллекции максимальным колличеством элементов
            _items = new Dictionary<int, List<Item>>(_maxSize);
        }
        /// <summary>
        /// Добавление данных в хэш-таблицу
        /// </summary>
        /// <param name="key"> Ключ </param>
        /// <param name="value"> Хранимые данные </param>
        public void Insert(string key, string value)
        {
            //Проверка входных данных на корректность
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException(nameof(key));
            }
            if (key.Length>_maxSize)
            {
                throw new ArgumentException($"Максимальная длина ключа составляет {_maxSize} символов", nameof(key));
            }
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException(nameof(value));
            }
            //Создание нового экземпляра данных
            var item = new Item(key, value);
            //Получение хэш ключа
            var hash = GetHash(item.Key);

            // Получаем коллекцию элементов с таким же хешем ключа.
            // Если коллекция не пустая, значит заначения с таким хешем уже существуют,
            // следовательно добавляем элемент в существующую коллекцию.
            // Иначе коллекция пустая, значит значений с таким хешем ключа ранее не было,
            // следовательно создаем новую пустую коллекцию и добавляем данные.
            List<Item> hashTableItem = null;

            if (_items.ContainsKey(hash))
            {
                hashTableItem = _items[hash];
                var OldElementWithKey = hashTableItem.SingleOrDefault(i => i.Key == item.Key);
                if (OldElementWithKey != null)
                {
                    throw new ArgumentException($"Хэш таблица уже содержит элемент с ключем {key}. Ключ должен быть уникален", nameof(key));

                }
                _items[hash].Add(item);
            }
            else
            {
                hashTableItem = new List<Item> { item };
                _items.Add(hash, hashTableItem);
            }
        }
        /// <summary>
        /// Удаление данных из хэш таблицы по ключу
        /// </summary>
        /// <param name="key"> Ключ </param>
        public void Delete(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException(nameof(key));
            }
            if (key.Length > _maxSize)
            {
                throw new ArgumentException($"Максимальная длина ключа составляет {_maxSize} символов", nameof(key));
            }
            // Получаем хеш ключа.
            var hash = GetHash(key);
            // Если значения с таким хешем нет в таблице, 
            // то завершаем выполнение метода.
            if (!_items.ContainsKey(hash))
            {
                return;
            }
            // Получаем коллекцию элементов по хешу ключа.
            var hashTableItem = _items[hash];
            // Получаем элемент коллекции по ключу.
            var item = hashTableItem.SingleOrDefault(i => i.Key == key);
            // Если элемент коллекции найден, 
            // то удаляем его из коллекции.
            if (item != null)
            {
                hashTableItem.Remove(item);
            }
        }
        public string Search(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException(nameof(key));
            }
            if (key.Length > _maxSize)
            {
                throw new ArgumentException($"Максимальная длина ключа составляет {_maxSize} символов", nameof(key));
            }
            // Получаем хеш ключа.
            var hash = GetHash(key);
            if (!_items.ContainsKey(hash))
            {
                return null;
            }
            // Если хеш найден, то ищем значение в коллекции по ключу.
            var hashTableItem = _items[hash];
            if (hashTableItem != null)
            {
                // Получаем элемент коллекции по ключу.
                var item = hashTableItem.SingleOrDefault(i => i.Key == key);
                // Если элемент коллекции найден, 
                // то возвращаем хранимые данные.
                if (item != null)
                {
                    return item.Value;
                }
            }
            return null;
        }
                              
        /// <summary>
        /// Хэш функция
        /// </summary>
        /// <param name="value"> Хешируемая строка </param>
        /// <returns> хэш строки </returns>
        private int GetHash(string value)
        {
            
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (value.Length > _maxSize)
            {
                throw new ArgumentException($"Максимальная длина ключа составляет {_maxSize} символов", nameof(value));
            }
            var hash = value.Length;
            return hash;
        }
    }
}
