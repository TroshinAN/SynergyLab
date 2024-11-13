namespace SynergyConsoleLab;

internal class Program
{
    private static readonly string[,] _n0 = { { "*", "*", "*" }, { "*", " ", "*" }, { "*", " ", "*" }, { "*", " ", "*" }, { "*", "*", "*" } };
    private static readonly string[,] _n1 = { { " ", " ", "*" }, { " ", "*", "*" }, { "*", " ", "*" }, { " ", " ", "*" }, { " ", " ", "*" } };
    private static readonly string[,] _n2 = { { "*", "*", "*" }, { " ", " ", "*" }, { " ", " ", "*" }, { " ", "*", " " }, { "*", "*", "*" } };
    private static readonly string[,] _n3 = { { "*", "*", "*" }, { " ", "*", " " }, { "*", "*", "*" }, { " ", "*", " " }, { "*", " ", " " } };
    private static readonly string[,] _n4 = { { "*", " ", "*" }, { "*", " ", "*" }, { "*", "*", "*" }, { " ", " ", "*" }, { " ", " ", "*" } };
    private static readonly string[,] _n5 = { { "*", "*", "*" }, { "*", " ", " " }, { "*", "*", "*" }, { " ", " ", "*" }, { "*", "*", "*" } };
    private static readonly string[,] _n6 = { { " ", " ", "*" }, { " ", "*", " " }, { "*", "*", "*" }, { "*", " ", "*" }, { "*", "*", "*" } };
    private static readonly string[,] _n7 = { { "*", "*", "*" }, { " ", "*", " " }, { "*", " ", " " }, { "*", " ", " " }, { "*", " ", " " } };
    private static readonly string[,] _n8 = { { "*", "*", "*" }, { "*", " ", "*" }, { "*", "*", "*" }, { "*", " ", "*" }, { "*", "*", "*" } };
    private static readonly string[,] _n9 = { { "*", "*", "*" }, { "*", " ", "*" }, { "*", "*", "*" }, { " ", "*", " " }, { "*", " ", " " } };
    private static readonly string[,] _pt = { { " " }, { " " }, { " " }, { " " }, { "*" } };
    private static readonly string[][,] _arr = [_n0, _n1, _n2, _n3, _n4, _n5, _n6, _n7, _n8, _n9];

    static void Main(string[] args)
    {
        try
        {
            Console.Write("Введите число вашего рождения: ");
            var day = int.Parse(Console.ReadLine());

            Console.Write("Введите порядковый номер месяца вашего рождения: ");
            var month = int.Parse(Console.ReadLine());

            Console.Write("Введите год вашего рождения: ");
            var year = int.Parse(Console.ReadLine());

            var date = new DateTime(year, month, day);

            ViewDayOfWeekName(date.DayOfWeek);
            ViewAgeNow(date);
            ViewIsLeapYear(date.Year);
            ViewDateByStarts(date);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private static void ViewDayOfWeekName(DayOfWeek dayOfWeek)
    {
        var name = dayOfWeek switch
        {
            DayOfWeek.Monday => "понедельник",
            DayOfWeek.Tuesday => "вторник",
            DayOfWeek.Wednesday => "среда",
            DayOfWeek.Thursday => "четверг",
            DayOfWeek.Friday => "пятница",
            DayOfWeek.Saturday => "суббота",
            DayOfWeek.Sunday => "воскресенье",
            _ => throw new NotImplementedException(),
        };

        Console.WriteLine($"Это был день недели: {name}");
    }

    private static void ViewAgeNow(DateTime date)
    {
        var nowDate = DateTime.Today;
        var age = 0;

        if (nowDate > date)
        {
            age = nowDate.AddTicks(-date.Ticks).Year - 1;
        }

        Console.WriteLine($"Сейчас лет: {age}");
    }

    private static void ViewIsLeapYear(int year)
    {
        // Вариант 1:
        //var not = DateTime.IsLeapYear(year) ? "" : " не";

        // Вариант 2:
        var not = (year % 4 == 0 && ((year % 100 == 0 && year % 400 == 0) || year % 100 != 0))
            ? ""
            : " не";

        Console.WriteLine($"{year}{not} високосный");
    }

    private static void ViewDateByStarts(DateTime date)
    {
        var starNumbers = new List<string[,]>(10);

        AddNumber(starNumbers, date.Day);
        starNumbers.Add(_pt);
        AddNumber(starNumbers, date.Month);
        starNumbers.Add(_pt);
        AddNumber(starNumbers, date.Year);

        for (var row = 0; row < starNumbers[0].GetLength(0); row++)
        {
            for (var elem = 0; elem < starNumbers.Count; elem++)
            {
                // Разделитель между элементами для красоты
                Console.Write(" ");
                for (var column = 0; column < starNumbers[elem].GetLength(1); column++)
                {
                    Console.Write(starNumbers[elem][row, column]);
                }
            }
            Console.WriteLine();
        }
    }

    private static void AddNumber(List<string[,]> list, int number)
    {
        var numberText = number.ToString("00");

        for (int i = 0; i < numberText.Length; i++)
        {
            var ch = numberText[i].ToString();
            var num = int.Parse(ch);

            list.Add(_arr[num]);
        }
    }
}
