using CommunityToolkit.Mvvm.Messaging;

public class MockEoLApplicationService(ICanCommunicationService _canService) : IEoLApplicationService
{

    public bool IsActive = false;
    private double chargeLevel = 0.2; // Initial charge level (20%)
    private readonly double maxCellVoltage = 4.2; // Fully charged voltage per cell
    private readonly double minCellVoltage = 3.0; // Minimum voltage per cell
    private readonly double chargeRate = 0.00005; // Charge rate per update
    private readonly int cellCount = 14;
    private readonly Random random = new();
    public bool CheckIfActive()
    {
        return IsActive;
    }

    public async Task<bool> Evaluate()
    {
        throw new NotImplementedException();


    }
    public async Task<BmsData?> GetLatestBmsData()
    {
        if (!IsActive)
        {
            return null;
        }

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

        var brickData = new BrickData(
            LoadActive: false, // No load during charging
            ChargeActive: chargeLevel < 1.0, // Charging until full
            BalancingActive: deltaCellV > 0.05, // Start balancing if cell differences exceed 50mV
            Voltage: totalVoltage,
            MaxCellV: maxCellV,
            MinCellV: minCellV,
            Current: current,
            Dcli: current * 0.9, // Charge input slightly lower than total current
            Dclo: 0, // No discharge during charging
            Temperature: 25 + (chargeLevel * 10) + random.NextDouble() * 2, // Slight temperature fluctuations
            DeltaCellV: deltaCellV,
            Cells: cells
        );

        var powerData = new PowerData(
            Active: chargeLevel < 1.0, // Charging active until full
            Voltage: totalVoltage,
            CurrentDischarge: 0, // No discharge during charging
            CurrentCharge: current, // Charging current
            PowerDischarge: 0,
            PowerCharge: current * totalVoltage // Power in watts
        );

        return await Task.FromResult(new BmsData(brickData, powerData));
    }
    public void Initialize()
    {
       // _canService.Init();
    }
    
    public void Start()
    {
       
            IsActive = true;
        
    }

    public void Stop()
    {
       
            IsActive = false;
        
    }


}