using System;


namespace HomeWork_7_8
{
    class Program
    {
        static void Main(string[] args)
        {
            string baseDT = @"D:\temp\NoteBD.txt";
            Repository base1 = new Repository(baseDT);

            Console.WriteLine("\nПечать базы из репозитория");
            base1.PrintDbToConsole();

            Console.WriteLine("\nПросмотр записи. Функция должна содержать параметр ID записи, которую необходимо вывести на экран");
            Console.WriteLine(base1[0]);



            Console.WriteLine("Удалим из базы элементм по ИД");
            base1.Delete(0);
            base1.PrintDbToConsole();

            Console.WriteLine("Редактируем запись, получая ее по ID");
            Note newNote = new Note(990,
                                    DateTime.Parse("12.11.2021 03:12"),
                                    "Петров Петр Петрович",
                                    33,
                                    125.4,
                                    DateTime.Parse("05.11.1990"),
                                    "Краснодар город");
             
            base1.SetNote(1, newNote);
            base1.PrintDbToConsole();
            
            Console.WriteLine("Загрузка записей в базу по диапазону дат");
            baseDT = @"D:\temp\NoteBD2.txt";
            DateTime dateStart = DateTime.Parse("01.01.2020");
            DateTime dateEnd = DateTime.Parse("31.12.2020");
            base1.Load(baseDT, dateStart, dateEnd);
            base1.PrintDbToConsole();

            Console.WriteLine("Сортировка базы по дате Возрастание");
            base1.SortNote(true);
            base1.PrintDbToConsole();
            Console.WriteLine("Сортировка базы по дате Убыввание");
            base1.SortNote(false);
            base1.PrintDbToConsole();


            Console.WriteLine("Сохраним базу");
            base1.Save(@"D:\temp\NoteBD1.txt");



        }
    }
}


/*Просмотр записи. Функция должна содержать параметр ID записи, которую необходимо вывести на экран. 
+Создание записи. 
Удаление записи.
Редактирование записи.
Загрузка записей в выбранном диапазоне дат.
Сортировка по возрастанию и убыванию даты.*/