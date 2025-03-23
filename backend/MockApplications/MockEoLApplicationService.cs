public class MockEoLApplicationService : IEoLApplicationService
{

    public bool IsActive = false;

    public bool CheckIfActive()
    {
        return IsActive;
    }

    public  async Task<BmsData?> GetLatestBmsData()
    {
        if(!IsActive)
        {
            return null;
        }

        var cells = new double[14];
        for (int i = 0; i < cells.Length; i++)
        {
            cells[i] = 3000 + Random.Shared.NextDouble() * 1200; // 3000-4200 mV
        }


        var maxCellV = cells.Max();
        var minCellV = cells.Min();
        var deltaCellV = maxCellV - minCellV;
        var totalVoltage = cells.Sum() / 1000.0; // Convert mV to V

        var brickData = new BrickData(
            LoadActive: true,
            ChargeActive: Random.Shared.NextDouble() * 100, // 0-100%
            BalancingActive: true,
            Voltage: totalVoltage,
            MaxCellV: maxCellV,
            MinCellV: minCellV,
            Current: Random.Shared.NextDouble() * 10 - 5, // -5A to 5A
            Dcli: Random.Shared.NextDouble() * 20, // 0-20A
            Dclo: Random.Shared.NextDouble() * 20, // 0-20A
            Temperature: Random.Shared.NextDouble() * 60, // 0-60°C
            DeltaCellV: deltaCellV,
            Cells: cells
        );

        var powerData = new PowerData(
            Active: true,
            Voltage: 40 + Random.Shared.NextDouble() * 10, // 40-50V
            CurrentDischarge: Random.Shared.NextDouble() * 15, // 0-15A
            CurrentCharge: Random.Shared.NextDouble() * 10, // 0-10A
            PowerDischarge: 0,
            PowerCharge: 0
        );
        

        return await Task.FromResult(new BmsData(brickData, powerData));

    }
    public void Initialize()
    {
    }
    public void Pause()
    {
    }
    public void Start()
    {
        IsActive= true;
    }

    public void Stop()
    {
        IsActive = false;
    }


}