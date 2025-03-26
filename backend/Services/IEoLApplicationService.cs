public interface IEoLApplicationService
{
    void Start();
    void Stop();
    void Initialize();

    bool CheckIfActive();
    Task<BmsData?> GetLatestBmsData();
    
    Task<bool> Evaluate();
}