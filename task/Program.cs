namespace task
{
    internal class Program
    {

        /*
         *Создать класс Date, включающий следующие члены: 
          поля day, month, year; 
          свойства Day, Month, Year, Day_Of_Week;
          конструктор по умолчанию; 
          конструктор с параметрами;
          метод, возвращающий разницу между двумя датами в днях;
          метод изменения даты на заданное количество дней;
          метод вывода даты на экран.
          клиентской части приложения продемонстрировать 
          работу всех методов класса
         *
         */
        static void Main(string[] args)
        {
            // Конструктор по умолчанию.
            Date date0 = new Date();
            date0.Print();
            Console.WriteLine();

            // Конструктор с параметрами.
            Date date1 = new Date(DateTime.Today.Day, DateTime.Today.Month, DateTime.Today.Year);
            date1.Print();
            Console.WriteLine();

            Date date2 = new Date(24, 02, 2022);
            date2.Print();
            Console.WriteLine();

            // Разница между двумя датами в днях.
            Console.WriteLine($"Разница {Date.DiffInDaysBetwDates(date1, date2)} дней.");

            // Изменение даты на заданное количество дней.
            date1.DatePlusDays(10);
            date1.Print();
            Console.WriteLine();

            // Ввод даты.
            Date date3 = new Date();
            do
            {
                Console.WriteLine("Введите дату: ");
                date3.Input();
                if (Date.DateValidationCheck(date3) == false)
                    Console.WriteLine("Даты не существует! Попробуйте еще раз!");
            } while (Date.DateValidationCheck(date3) == false);
            date3.Print();
            Console.WriteLine();
        }
    }
}