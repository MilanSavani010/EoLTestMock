namespace Models;

public class Profile
{
    public string Name { get; set; } = string.Empty;
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public List<Matrix> Matrices { get; set; } = new();
    public List<Sequence> Sequences { get; set; } = new();
}

public class Matrix
{
    public string Name { get; set; } = string.Empty;
    public string Id { get; set; } = Guid.NewGuid().ToString();
}

public class Sequence
{
    public string Name { get; set; } = string.Empty;
    public List<Step> Steps { get; set; } = new();
}

public class Step
{
    public string Name { get; set; } = string.Empty;
    public List<Command> StartCommands { get; set; } = new();
    public List<Command> EndCommands { get; set; } = new();
}

public class Command
{
    public string Name { get; set; } = string.Empty;
    public float Value { get; set; }
}
