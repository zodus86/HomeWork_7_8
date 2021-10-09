
using System;
using System.IO;

namespace HomeWork_7_8
{
    struct Note : IComparable<Note>
    {

        //private static long id = 2;
        //public Guid ID { get; set; }

        /// <summary>
        /// id
        /// </summary>
        public uint ID { get; set; }

        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// ФИО
        /// </summary>
        public String FullName { get; set; }

        /// <summary>
        /// Возраст
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// Рост
        /// </summary>
        public double Height { get; set; }

        /// <summary>
        /// День рождения
        /// </summary>
        public DateTime Birthday { get; set; }

        /// <summary>
        /// Место рождения
        /// </summary>
        public String PlaceOfBirth { get; set; }

        /// <summary>
        /// Запись конструктор
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <param name="date">ДатаСоздания записи</param>
        /// <param name="fullName">ФИО</param>
        /// <param name="age">Возраст</param>
        /// <param name="height">Рост</param>
        /// <param name="birthday">Дата рождения</param>
        /// <param name="placeOfBirth">Место рождения</param>
        public Note (uint id, DateTime date, string fullName, int age, double height, DateTime birthday, string placeOfBirth)
        {
            ID = id;
            Date = date;
            FullName = fullName;
            Age = age;
            Height = height;
            Birthday = birthday;
            PlaceOfBirth = placeOfBirth;
        }

        /// <summary>
        /// Вывод данных в строке
        /// </summary>
        /// <returns></returns>
        public string Print()
        {
            return $"{ID}#{Date}#{FullName}#{Age}#{Height}#{Birthday}#{PlaceOfBirth}";
        }

        /// <summary>
        /// для сравнения стркуктур по дате
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(Note other)
        {
            if (other.Date == DateTime.Parse("01.01.0001 00:00"))
                return 1;

            else
                return this.Date.CompareTo(other.Date);
        }
    }
}
