using System.Xml.Linq;

namespace task;

internal class Date
{
    private int m_Day { get; set; }     // День.
    int m_Month { get; set; }           // Месяц.
    int m_Year { get; set; }            // Год.
    string? m_DayOfWeek { get; set; }   // День недели.

    // Месяца.
    enum Months
    {
        January = 1,    // Январь, 31 день
        February = 2,   // Февраль, 28 дней (В високосные годы вводится дополнительный день — 29 февраля.)
        March = 3,      // Март, 31 день 
        April = 4,      // Апрель, 30 дней 
        May = 5,        // Май, 31 день 
        June = 6,       // Июнь, 30 дней 
        July = 7,       // Июль, 31 день 
        August = 8,     // Август, 31 день 
        September = 9,  // Сентябрь, 30 дней 
        October = 10,   // Октябрь, 31 день 
        November = 11,  // Ноябрь, 30 дней 
        December = 12   // Декабрь, 31 день 
    };

    // Конструктор по умолчанию.
    public Date()
    {
        m_Day = 1;
        m_Month = 1;
        m_Year = 1;
        m_DayOfWeek = "default";
    }

    // Конструктор с параметрами.
    public Date(in int day, in int month, in int year)
    {
        m_Day = day;
        m_Month = month;
        m_Year = year;
        m_DayOfWeek = DayOfWeek(day, month, year);
    }

    private string? DayOfWeek(in int day, in int month, in int year)
    {
        string? str = null;
        int a = (14 - month) / 12;
        int y = year - a;
        int m = month + 12 * a - 2;
        int dow = (7000 + (day + y + y / 4 - y / 100 + y / 400 + (31 * m) / 12)) % 7;

        switch (dow)
        {
            case 1:
                str = "Понедельник";
                break;
            case 2:
                str = "Вторник";
                break;
            case 3:
                str = "Среда";
                break;
            case 4:
                str = "Четверг";
                break;
            case 5:
                str = "Пятница";
                break;
            case 6:
                str = "Суббота";
                break;
            case 0:
                str = "Воскресенье";
                break;
        }

        return str;
    }

    // Деконструкторы позволяют выполнить декомпозицию объекта на отдельные части.
    private void Deconstruct(out int day, out int month, out int year, out string? dayOfWeek)
    {
        day = m_Day;
        month = m_Month;
        year = m_Year;
        dayOfWeek = m_DayOfWeek;
    }

    // Возвращает разность в днях между датами.
    public static int DiffInDaysBetwDates(Date date2, Date date1)
    {
        (int day1, int month1, int year1, _) = date1;
        (int day2, int month2, int year2, _) = date2;

        int differenceIs = 0; // Счетчик разности в днях между этими датами.

        if (year1 == year2 && month1 == month2 && day1 == day2) // Если даты равны.
        {
            return differenceIs = 0;
        }
        else
        {
            // Пока даты не равны, вычисляем следующую дату.
            while (!(year1 == year2 && month1 == month2 && day1 == day2))
            {
                switch (month1)
                {
                    case (int)Months.April: // Расчет следующей даты за введенной, для месяцев c 30 днями.
                    case (int)Months.June:
                    case (int)Months.September:
                    case (int)Months.November:
                        day1++;
                        if (day1 > 30)
                        {
                            day1 = 1;
                            month1++;
                            if (month1 > 12)
                            {
                                month1 = 1;
                                year1++;
                            }
                        }
                        break;

                    case (int)Months.January: // Расчет следующей даты за введенной, для месяцев c 31 днем.
                    case (int)Months.March:
                    case (int)Months.May:
                    case (int)Months.July:
                    case (int)Months.August:
                    case (int)Months.October:
                    case (int)Months.December:
                        day1++;
                        if (day1 > 31)
                        {
                            day1 = 1;
                            month1++;
                            if (month1 > 12)
                            {
                                month1 = 1;
                                year1++;
                            }
                        }
                        break;

                    case (int)Months.February: // Расчет следующей даты за введенной, для февраля (как высокосного так и не високосного).
                        day1++;
                        if (!(year1 % 400 == 0 || (year1 % 100 != 0 && year1 % 4 == 0)) && (day1 > 28)) // Не високосный.
                        {
                            day1 = 1;
                            month1++;
                            if (month1 > 12)
                            {
                                month1 = 1;
                                year1++;
                            }
                        }
                        else if ((year1 % 400 == 0 || (year1 % 100 != 0 && year1 % 4 == 0)) && (day1 > 29)) // Високосный.
                        {
                            day1 = 1;
                            month1++;
                            if (month1 > 12)
                            {
                                month1 = 1;
                                year1++;
                            }
                        }
                        break;
                }
                differenceIs++;
            }
        }
        return differenceIs;
    }

    // Изменение даты на заданное количество дней.
    public void DatePlusDays(int days)
    {
        while (days > 0) // Пока даты не равны, вычисляем следующую дату.
        {
            switch (m_Month)
            {
                case (int)Months.April: // Расчет следующей даты за введенной, для месяцев c 30 днями.
                case (int)Months.June:
                case (int)Months.September:
                case (int)Months.November:
                    m_Day++;
                    if (m_Day > 30)
                    {
                        m_Day = 1;
                        m_Month++;
                        if (m_Month > 12)
                        {
                            m_Month = 1;
                            m_Year++;
                        }
                    }
                    break;

                case (int)Months.January: // Расчет следующей даты за введенной, для месяцев c 31 днем.
                case (int)Months.March:
                case (int)Months.May:
                case (int)Months.July:
                case (int)Months.August:
                case (int)Months.October:
                case (int)Months.December:
                    m_Day++;
                    if (m_Day > 31)
                    {
                        m_Day = 1;
                        m_Month++;
                        if (m_Month > 12)
                        {
                            m_Month = 1;
                            m_Year++;
                        }
                    }
                    break;

                case (int)Months.February: // Расчет следующей даты за введенной, для февраля (как высокосного так и не високосного).
                    m_Day++;
                    if (!(m_Year % 400 == 0 || (m_Year % 100 != 0 && m_Year % 4 == 0)) && (m_Day > 28)) // Не високосный.
                    {
                        m_Day = 1;
                        m_Month++;
                        if (m_Month > 12)
                        {
                            m_Month = 1;
                            m_Year++;
                        }
                    }
                    else if ((m_Year % 400 == 0 || (m_Year % 100 != 0 && m_Year % 4 == 0)) && (m_Day > 29)) // Високосный.
                    {
                        m_Day = 1;
                        m_Month++;
                        if (m_Month > 12)
                        {
                            m_Month = 1;
                            m_Year++;
                        }
                    }
                    break;
            }
            days--;
        }
    }

    // Ввод даты.
    public void Input()
    {
        Console.WriteLine("День:");
        m_Day = Convert.ToInt32(Console.ReadLine());

        Console.WriteLine("Месяц:");
        m_Month = Convert.ToInt32(Console.ReadLine());

        Console.WriteLine("Год:");
        m_Year = Convert.ToInt32(Console.ReadLine());

        Console.WriteLine();

        m_DayOfWeek = DayOfWeek(m_Day, m_Month, m_Year);
    }

    // Проверяет является ли год високосным.
    static bool IsItALeapYear(int year)
    {
        if (year % 400 == 0 || (year % 100 != 0 && year % 4 == 0))
            return true;
        else
            return false;
    }

    // Проверяет корректность введенных значений даты.
    public static bool DateValidationCheck(Date date)
    {
        (int day, int month, int year, _) = date;

        if (year > 0)
        {
            if (month > 0 && month < 13)
            {
                if (
                    ((IsItALeapYear(year))
                     && (month == (int)Months.February) && (day == 29))                 // Если 29 февраля в високосном году.
                    || ((IsItALeapYear(year))
                        && (month == (int)Months.February) && (day > 0 && day < 29))    // Если 1-28 февраля, високосный год.
                    || (!(IsItALeapYear(year))
                        && (month == (int)Months.February) && (day > 0 && day < 29))    // Если 1-28 февраля, обычный год.
                    || ((month == (int)Months.January
                         || month == (int)Months.March
                         || month == (int)Months.May
                         || month == (int)Months.July
                         || month == (int)Months.August
                         || month == (int)Months.October
                         || month == (int)Months.December) && (day > 0 && day < 32))    // Все месяца, в которых 31 день.
                    || ((month == (int)Months.April
                         || month == (int)Months.June
                         || month == (int)Months.September
                         || month == (int)Months.November) && (day > 0 && day < 31))    // Все месяца, в которых 30 дней.
                )
                {
                    return true; // Все введённые значения корректны.
                }
                else
                    return false; // Введен не корректный день.
            }
            else
                return false; // Введен не корректный месяц. 
        }
        else
            return false; // Введен не корректный год. 
    }

    // Вывод даты.
    public void Print()
    {
        Console.Write($"{m_Day}.{m_Month}.{m_Year} ({m_DayOfWeek})");
    }
}