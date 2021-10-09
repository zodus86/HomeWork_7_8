using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;


namespace HomeWork_7_8
{
    /// <summary>
    /// репозитторий
    /// </summary>
    class Repository
    {
        /// <summary>
        /// База данных ежедневника
        /// </summary>
        private Note[] note;

        /// <summary>
        /// полный путь хранения базы 
        /// </summary>
        private string Path;
        
        /// <summary>
        /// индекс крайнего не пустого элемента в массиве
        /// </summary>
        uint index; 


        public string this[uint index]
        {
            get { return note[index].Print(); }
        }


        /// <summary>
        /// скрытый метод для создания базы при ее отсутсвии
        /// </summary>
        private void InstalNewBase()
        {

            if (IhaveBase())
            {
                Console.WriteLine($"Обнуружен файл базы данных в {Path}");
            }
            else
            {
            using (StreamWriter streamWriter = new StreamWriter(Path))
            {
                Console.WriteLine("Файл базы данных не обноружен, начинаю создание базы");
                string newBase =   @"1#20.12.2021 00:12#Иванов Иван Иванович#25#176#05.05.1992#город Москва" +
                                  "\n2#15.12.2021 03:12#Алексеев Алексей Иванович#24#176#05.11.1980#город Томск" +
                                  "\n2#12.11.2021 03:12#Петров Петр Петрович#33#125,4#05.11.1990#Krasnodar";
                streamWriter.Write(newBase);
                Console.WriteLine("Файл базы данных был создан и наполнен ");
            }
            }

            Console.WriteLine("Начинаю загружать базу в репозиторий");
            Load();
            Console.WriteLine("База успешно загружена!");
            

        }
       
     
        /// <summary>
        /// если нету файла с базой по адресу, то будет создана новая база
        /// </summary>
        /// <param name="path">полный путь к файлу базы данных</param>
        public Repository(string path)
        {
            Path = path;
            note = new Note[1];
            index = 0; 
            InstalNewBase();

        }

        /// <summary>
        /// проверка есть ли файл базы сохраненный
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
         bool IhaveBase()
        {
            return File.Exists(Path);
        }

        /// <summary>
        /// Метод увеличения текущего хранилища
        /// </summary>
        /// <param name="Flag">Условие увеличения</param>
        private void Resize(bool Flag)
        {
            if (Flag)
            {
                Array.Resize(ref this.note, this.note.Length * 2);
            }
        }

        /// <summary>
        /// Метод добавления записи в хранилище
        /// </summary>
        /// <param name="ConcreteWorker">Сотрудник</param>
        public void Add(Note ConcreteNote)
        {
            this.Resize(index >= this.note.Length);
            this.note[index] = ConcreteNote;
            this.index++;
        }

        /// <summary>
        /// Метод загрузки данных
        /// </summary>
        private void Load()
        {
            using (StreamReader streamReader = new StreamReader(this.Path))
            {
                while (!streamReader.EndOfStream)
                {
                    try
                    {
                        string[] args = streamReader.ReadLine().Split('#');
                        Add(new Note(   index, 
                                        DateTime.Parse(args[1]), 
                                        args[2],
                                        Convert.ToInt32(args[3]),
                                        Convert.ToDouble(args[4]), 
                                        Convert.ToDateTime(args[5]),
                                        args[6]));

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Нарушен формат базы данных, загрузка не возможна" +
                                $"\n Рекомендуется проверить содержимое файла {Path} " +
                                $"\n ошибка - {ex}");
                    }
                }
            }
        }

        /// <summary>
        /// загрузка записей из файла по диапазону дат
        /// </summary>
        /// <param name="path"></param>
        /// <param name="dataStart"></param>
        /// <param name="dataEnd"></param>
        public void Load(string path, DateTime dataStart, DateTime dataEnd)
        {
            using (StreamReader streamReader = new StreamReader(path))
            {
                while (!streamReader.EndOfStream)
                {
                    try
                    {
                        string[] args = streamReader.ReadLine().Split('#');
                        DateTime date = DateTime.Parse(args[1]);
                        if (date >= dataStart && date <= dataEnd)
                        {
                            Add(new Note(index,
                                        date,
                                        args[2],
                                        Convert.ToInt32(args[3]),
                                        Convert.ToDouble(args[4]),
                                        Convert.ToDateTime(args[5]),
                                        args[6]));
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Нарушен формат базы данных, загрузка не возможна" +
                                $"\n Рекомендуется проверить содержимое файла {Path} " +
                                $"\n ошибка - {ex}");
                    }
                }
            }
        }

        /// <summary>
        /// Метод сохранения данных
        /// </summary>
        /// <param name="Path">Путь к файлу сохранения</param>
        public void Save(string Path)
        {
            File.Delete(Path);

            for (int i = 0; i < this.index; i++)
            {
              string  temp = String.Format("{0}#{1}#{2}#{3}#{4}#{5}#{6} ",
                                        this.note[i].ID,
                                        this.note[i].Date,
                                        this.note[i].FullName,
                                        this.note[i].Age,
                                        this.note[i].Height,
                                        this.note[i].Birthday,
                                        this.note[i].PlaceOfBirth);


                File.AppendAllText(Path, $"{temp}\n");
            }
        }

        /// <summary>
        /// Вывод данных в консоль
        /// </summary>
        public void PrintDbToConsole()
        {

            for (int i = 0; i < index; i++)
            {
                Console.WriteLine(this.note[i].Print());
            }
        }

        /// <summary>
        /// Количество заметок
        /// </summary>
        public uint Count { get { return this.index; } }

        /// <summary>
        /// Удалить запись по ID
        /// </summary>
        /// <param name="id"></param>
        public void Delete (uint id)
        {
          List<Note> noteList = new List<Note>(this.note);
          noteList.Remove(note[id]);
          this.note = noteList.ToArray();
          index--;   
        }

        /// <summary>
        /// редактируем запись
        /// </summary>
        /// <param name="id"></param>
        /// <param name="newNote"></param>
        public void SetNote (uint id, Note newNote) 
        {
            newNote.ID = this.note[id].ID;
            this.note[id] = newNote;
            Console.WriteLine($"Запись изменина, но ID остается прежним");
        }

        public void SortNote (bool MinToMax)
        {
            List<Note> noteList = new List<Note>(this.note);

            if (MinToMax) noteList.Sort();
            else noteList.Reverse();

            this.note = noteList.ToArray();
        }

    }
}
