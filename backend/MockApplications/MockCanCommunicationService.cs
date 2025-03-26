

using CommunityToolkit.Mvvm.Messaging;

public class MockCanCommunicationService (IMessenger _messenger): ICanCommunicationService
{
    Status _status = Status.UNKNOWN;

    public Status GetStatus()
    {
        return _status;
    }

    public bool Init()
    {
        if(_status!=Status.UNKNOWN||_status!=Status.STOP)
        {
            return false;
        }
        _status = Status.INIT;
        return true;
    }


    public void SimulateMessageReception()
    {
        if(_status!=Status.RUNNING)
        {
            return;
        }

        while(_status==Status.RUNNING)
        {
            //simulate reception
        }
    }

    public bool Start()
    {
        if( _status != Status.INIT) 
        {
            return false;
        }
        _status = Status.RUNNING;
        return true;
    }

    public bool Stop()
    {
        if(_status != Status.RUNNING)
        {
            return false;
        }
        _status=Status.STOP;
        return true;
    }
}
