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
    public List<Message> Messages { get; set; } = new();
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
    public List<Observe> Observes { get; set; } = new();
}

public class Command
{
    public string Name { get; set; } = string.Empty;
    public Signal CommandSignal { get; set; } = new();
    public float Value { get; set; }
}


public class Observe
{
    public string Name { get; set; } = string.Empty;
    public Signal ObserveSignal { get; set; } = new();

    public List<Acceptance> Acceptances { get; set; } = new();
}


public class Acceptance
{
    public string Name { get; set; } = string.Empty;
    public float Minimum { get; set; } = 0.0f;
    public float Maximum { get; set; } = 0.0f;
    public Result Result { get; set; } = Result.UNKNOWN;
    
}


public enum Result
{
    UNKNOWN,
    ERROR,
    OK,
    WARNING
}


public class Message
{
    public string Name { get; set;} = string.Empty;
    public List<Signal> Signals { get; set; } = new();
}


public class Signal
{
    public string Name { get; set;} = string.Empty;
    public Message Parent { get; set; } = new();
}