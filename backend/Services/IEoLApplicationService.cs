public interface IEoLApplicationService
{
    void Start();
    void Stop();
    void Pause();
    void Initialize();

    bool CheckIfActive();
    Task<BmsData?> GetLatestBmsData();
}