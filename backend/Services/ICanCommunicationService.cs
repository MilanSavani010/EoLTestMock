using Peak.Can.Basic;
public enum Status
{
    INIT,
    RUNNING,
    STOP,
    UNKNOWN
}
public interface ICanCommunicationService
{
    bool Init();
    bool Start();
    bool Stop();
    void SimulateMessageReception();
    Status GetStatus();

}