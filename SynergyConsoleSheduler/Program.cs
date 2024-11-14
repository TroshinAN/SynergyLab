namespace SynergyConsoleSheduler;

internal class Program
{
    private enum ChangeMainMenuEnum : int { Exit = 0, Add, Change, Remove, ChangeStatus }

    private static ScheduleSaver _scheduleSaver = new();
    private static List<Schedule> _schedules = new();

    static void Main(string[] args)
    {
        _schedules = _scheduleSaver.Load();

        ToMainMenu();

        _scheduleSaver.Save(_schedules);
    }

    private static void ShowSchedules()
    {
        foreach (var schedule in GetOrderedSchedules())
        {
            Console.WriteLine($"{schedule.Id:#0}: {schedule.Name}");
            Console.WriteLine($"\tОписание: {schedule.Description}");
            Console.WriteLine("\tСтатус: " + (schedule.Completed ? "выполнена" : "не выполнена"));
        }
    }


    private static readonly int[] _mainMenuVariant = [0, 1, 2, 3, 4];
    private static void ToMainMenu()
    {
        Console.Clear();

        while (true)
        {
            Console.Clear();
            Console.WriteLine("\t\tСписок задач:");
            Console.WriteLine();

            ShowSchedules();

            Console.WriteLine();
            Console.WriteLine("Выберите действие:");

            if (_schedules.Count > 0)
            {
                Console.WriteLine("1 - Добавить задачу; 2 - редактировать задачу; 3 - удалить задачу; 4 - сменить статус; 0 - выход");
            }
            else
            {
                Console.WriteLine("1 - Добавить задачу; 0 - выход");
            }

            Console.Write("Ввод: ");
            var key = Console.ReadKey().KeyChar.ToString();

            if (int.TryParse(key, out var val))
            {
                if (_mainMenuVariant.Contains(val))
                {
                    var variant = (ChangeMainMenuEnum)val;

                    if (variant == ChangeMainMenuEnum.Exit)
                    {
                        return;
                    }

                    ToChangeMenu(variant);
                }
            }
        }
    }

    private static void ToChangeMenu(ChangeMainMenuEnum changeMainType)
    {
        switch (changeMainType)
        {
            case ChangeMainMenuEnum.Add:
                AddNewScheduleMenu();
                break;
            case ChangeMainMenuEnum.Change:
                ChangeScheduleMenu();
                break;
            case ChangeMainMenuEnum.Remove:
                RemoveScheduleMenu();
                break;
            case ChangeMainMenuEnum.ChangeStatus:
                ChangeScheduleStatusMenu();
                break;

            default: break;
        }
    }

    private static void AddNewScheduleMenu()
    {
        Console.Clear();
        Console.WriteLine("\t\tСоздание задачи");
        Console.WriteLine();

        Console.Write("Введите наименование: ");
        var name = Console.ReadLine() ?? string.Empty;

        Console.Write("Введите описание: ");
        var descr = Console.ReadLine() ?? string.Empty;

        var id = _schedules.Max(x => x.Id) + 1;

        _schedules.Add(new Schedule { Id = id, Name = name, Description = descr, Completed = false });
    }

    private static void RemoveScheduleMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("\t\tВыберите номер задачи для удаления или 0 для выхода:");
            Console.WriteLine("№\tНаименование");

            foreach (var schedule in GetOrderedSchedules())
            {
                Console.WriteLine($"{schedule.Id}\t{schedule.Name}");
            }
            Console.Write("Ввод: ");
            var key = Console.ReadLine();

            if (int.TryParse(key, out var val))
            {
                if (val == 0)
                {
                    return;
                }

                var schedule = _schedules.FirstOrDefault(x => x.Id == val);

                if (schedule is not null)
                {
                    _schedules.Remove(schedule);
                    return;
                }
            }
        }
    }

    private static void ChangeScheduleStatusMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("\t\tВыберите номер задачи для изменения статуса или 0 для выхода");
            Console.WriteLine("№\tНаименование");

            foreach (var schedule in GetOrderedSchedules())
            {
                Console.WriteLine($"{schedule.Id}\t{schedule.Name}");
            }
            Console.Write("Ввод: ");
            var key = Console.ReadLine();

            if (int.TryParse(key, out var val))
            {
                if (val == 0)
                {
                    return;
                }

                var scheduleToChange = _schedules.FirstOrDefault(x => x.Id == val);

                if (scheduleToChange is not null)
                {
                    ChangeScheduleStatus(scheduleToChange);
                }
            }
        }
    }

    private static void ChangeScheduleStatus(Schedule schedule)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("\t\tВыберите новый статус для задачи");
            Console.WriteLine("1 - выполнена; 2 - не выполнена; 0 - назад");

            Console.Write("Ввод: ");
            var key = Console.ReadKey().KeyChar.ToString();

            if (int.TryParse(key, out var val))
            {
                switch (val)
                {
                    case 0: return;
                    case 1: schedule.Completed = true; return;
                    case 2: schedule.Completed = false; return;

                    default: break;
                }
            }
        }
    }

    private static void ChangeScheduleMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("\t\tВыберите номер задачи для редактирования или 0 для выхода");
            Console.WriteLine("№\tНаименование");

            foreach (var schedule in GetOrderedSchedules())
            {
                Console.WriteLine($"{schedule.Id}\t{schedule.Name}");
            }
            Console.Write("Ввод: ");
            var key = Console.ReadLine();

            if (int.TryParse(key, out var val))
            {
                if (val == 0)
                {
                    return;
                }

                var scheduleToChange = _schedules.FirstOrDefault(x => x.Id == val);

                if (scheduleToChange is not null)
                {
                    ChangeSchedule(scheduleToChange);
                }
            }
        }
    }

    private static void ChangeSchedule(Schedule schedule)
    {
        Console.Clear();
        Console.Write("Введите новое наименование: ");
        schedule.Name = Console.ReadLine() ?? string.Empty;

        Console.Write("Введите новое описание: ");
        schedule.Description = Console.ReadLine() ?? string.Empty;
    }

    private static List<Schedule> GetOrderedSchedules()
    {
        return _schedules
            .OrderBy(ord => ord.Completed)
            .ThenBy(ord => ord.Name)
            .ToList();
    }
}
