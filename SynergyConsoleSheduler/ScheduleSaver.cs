using System.Text;

namespace SynergyConsoleSheduler;

public sealed class ScheduleSaver
{
    private const string FileName = "Schedules.txt";

    public void Save(List<Schedule> schedules)
    {
        var rows = new List<string>(schedules.Count);
        foreach (var schedule in schedules)
        {
            var compl = schedule.Completed ? 1 : 0;
            var str = $"{schedule.Name};{schedule.Description};{compl}";

            rows.Add(str);
        }

        File.WriteAllLines(FileName, rows);
    }

    public List<Schedule> Load()
    {
        var schedules = new List<Schedule>();

        if (!File.Exists(FileName))
        {
            return [];
        }

        var fileRows = File.ReadAllLines(FileName);
        var id = 1;

        foreach (var row in fileRows)
        {
            var elems = row.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

            if (elems.Length == 3)
            {
                schedules.Add(new Schedule
                {
                    Id = id++,
                    Name = elems[0],
                    Description = elems[1],
                    Completed = elems[2] == "1"
                });
            }
        }

        return schedules;
    }
}
