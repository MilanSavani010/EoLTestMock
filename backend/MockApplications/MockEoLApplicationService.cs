using System.Threading;

public class MockEoLApplicationService(ICanCommunicationService _canService) : IEoLApplicationService
{


    private BatteryModel _batteryModel=new();
    private PSBModel _psbModel=new();
    private TestState _testState = TestState.IDLE;
    private EoLAppState _eoLState = EoLAppState.OK;
    


    private double chargeLevel = 0.2; // Initial charge level (20%)
    private readonly double maxCellVoltage = 4.2; // Fully charged voltage per cell
    private readonly double minCellVoltage = 3.0; // Minimum voltage per cell
    private readonly double chargeRate = 0.00005; // Charge rate per update
    private readonly int cellCount = 14;
    private readonly Random random = new();
    public bool CheckIfActive()
    {
        return _testState == TestState.RUNNING;
    }

 
    public async Task<BmsData?> GetLatestBmsData()
    {
        var brickData = new BrickData(
            LoadActive: _batteryModel.LoadActive, 
            ChargeActive: _batteryModel.ChargeActive,
            BalancingActive: _batteryModel.BalancingActive,
            Voltage: _batteryModel.Voltage,
            MaxCellV: _batteryModel.MaxCellV,
            MinCellV: _batteryModel.MinCellV,
            Current: _batteryModel.Current,
            Dcli: _batteryModel.Dcli, 
            Dclo: _batteryModel.Dclo, 
            Temperature: _batteryModel.Temperature,
            DeltaCellV: _batteryModel.DeltaCellV,
            Cells: _batteryModel.CellVoltages
        );

        var powerData = new PowerData(
            Active: _psbModel.Active, 
            Voltage: _psbModel.Voltage,
            CurrentDischarge: _psbModel.CurrentDischarge,
            CurrentCharge: _psbModel.CurrentCharge,
            PowerDischarge: _psbModel.PowerDischarge,
            PowerCharge: _psbModel.PowerCharge
        );

        return await Task.FromResult(new BmsData(brickData, powerData));
    }
    public void Initialize()
    {
        // _canService.Init();
       
    }

    public async void Start()
    {

        if(_eoLState == EoLAppState.ERROR)
        {
            return;
        }
        
        _testState = TestState.RUNNING;
        Task.Run(() => Monitor());
        TestRun();
        await SimulateMessageReception();
    }

    public void Stop()
    {

        if(_testState != TestState.RUNNING)
        {
            return;
        }

        _batteryModel = new BatteryModel();
        _psbModel = new PSBModel();
        _testState = TestState.IDLE;
        StopTest();
    }

    private Task Monitor()
    {
        while(_testState == TestState.RUNNING)
        {

            var deltaV = _batteryModel.DeltaCellV;
            var deltaT = _batteryModel.DeltaCellT;

            if(_batteryModel.DeltaCellV > 0.5)
            {
                _eoLState = EoLAppState.ERROR;
                Console.WriteLine(deltaV);
                StopTest();
            }
             
            if(_batteryModel.DeltaCellT > 5)
            {
                _eoLState = EoLAppState.ERROR;
                Console.WriteLine(deltaT);
                StopTest();
            }
           Task.Delay(50);
        }
        return Task.CompletedTask;
    }

    private void TestRun()
    {
        var cts = new CancellationTokenSource(500);
        SendCyclicCommand(cts.Token);
        
        // TODO:
        // log following
        // criteria,
        // batteryid,
        // cellvdelta,
        // maxcellv,
        // maxcellvno,
        // mincellv,
        // mincellvno,
        // maxcellt

        ChargeTest();
        //LoadTest();




    }

    private void SendCyclicCommand(CancellationToken cts)
    {
        while (!cts.IsCancellationRequested)
        {
            // TODO:
            // simulate send command 
            
            if(_psbModel.Voltage>0)
            {
                StopTest();
            }

            Thread.Sleep(50); 
        }

    }

    private void ChargeTest()
    {

        // TODO:
        // send charge_req true
        Thread.Sleep(5000);

        if (_psbModel.Voltage == 0)
        {
            // TODO:
            // log 
            StopTest();
        }

        // TODO:
        // send voltage 53 V
        // send psb current 5 A
        // send psb power 15000 W
        // send psb on 
        Thread.Sleep(20000);

        // TODO:
        // send psb current 10 A
        Thread.Sleep(20000);

        // TODO:
        // send psb current 20 A
        while (_psbModel.Voltage == 52.5)
        {
            Task.Delay(200);
        }

        // TODO:
        // send psb current 0 A
        // send psb off
        // send charge_req false

        Thread.Sleep(5000);
        if (_psbModel.Voltage == _batteryModel.Voltage)
        {
            // TODO:
            // log
            StopTest();
        }
    }

    private void LoadTest()
    {   
        // TODO:
        // send load_req true
        if(_psbModel.Voltage == 0)
        {
            // TODO:
            // log 
            StopTest();
        }

        // TODO:
        // send psb sink current 5 A
        // send psb on

        Thread.Sleep(20000);

        // TODO:
        // send psb sink current 10 A
        Thread.Sleep(20000);
        
        // TODO:
        // send psb sink current 70 A
        Thread.Sleep(30000);

        // TODO:
        // send psb sink current 50 A
        Thread.Sleep(120000);

        // TODO:
        // send psb sink current 20 A
        Thread.Sleep(10000);

        // TODO:
        // send psb sink current 10 A
        // send voltage 46.6 V

        while(_psbModel.CurrentDischarge != 1 || _psbModel.CurrentDischarge!=-1)
        {
            Task.Delay(200);
        }

        // TODO:
        // send psb off
        // send load_req false

    }

    private void StopTest()
    {
        _testState = TestState.IDLE;
    }

     private async Task SimulateMessageReception()
     {
        await Task.Delay(600);
        while(_testState != TestState.IDLE)
        {
            var cells = new double[cellCount];

            for (int i = 0; i < cells.Length; i++)
            {
                // Simulate individual cell voltage with fluctuations
                double cellChargeFactor = chargeLevel + (random.NextDouble() * 0.05 - 0.001); // Small variation in charge rate
                cells[i] = Math.Clamp(minCellVoltage + (cellChargeFactor * (maxCellVoltage - minCellVoltage)), minCellVoltage, maxCellVoltage);
            }

            // Increase charge level over time
            chargeLevel += chargeRate;
            chargeLevel = Math.Min(chargeLevel, 1.0); // Cap charge level at 100%

            var maxCellV = cells.Max();
            var minCellV = cells.Min();
            var deltaCellV = maxCellV - minCellV;
            var totalVoltage = cells.Sum(); // Total pack voltage

            // Simulate charging current decreasing over time
            double current = Math.Max((1 - chargeLevel) * 10, 0.5); // High at low charge, decreasing over time

            _batteryModel.LoadActive = false;
            _batteryModel.ChargeActive = chargeLevel < 1.0;
            _batteryModel.BalancingActive = deltaCellV > 0.5;
            _batteryModel.Voltage = totalVoltage;
            _batteryModel.MinCellV = minCellV;
            _batteryModel.MaxCellV = maxCellV;
            _batteryModel.Current = current;
            _batteryModel.Dcli = current * 0.9;
            _batteryModel.Dclo = 0;
            _batteryModel.Temperature = 25 + (chargeLevel * 10) + random.NextDouble() * 2;
            _batteryModel.DeltaCellV = deltaCellV;
            _batteryModel.CellVoltages = cells;
            _psbModel.Active = chargeLevel < 1.0;
            _psbModel.Voltage = totalVoltage;
            _psbModel.CurrentCharge = current;
            _psbModel.CurrentDischarge = 0;
            _psbModel.PowerCharge = current * totalVoltage;
            _psbModel.PowerDischarge = 0;

            await Task.Delay(100);
        }
        
     }


}