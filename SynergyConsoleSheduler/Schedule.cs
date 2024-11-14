namespace SynergyConsoleSheduler;

public sealed class Schedule
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public bool Completed { get; set; }
}
