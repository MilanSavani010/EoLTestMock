
public enum TestState
{
    RUNNING,
    IDLE
}

public enum EoLAppState
{
    ERROR,
    OK
}
public interface IEoLApplicationService
{
    void Start();
    void Stop();
    void Initialize();

    bool CheckIfActive();
    Task<BmsData?> GetLatestBmsData();
}