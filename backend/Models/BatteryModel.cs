public class BatteryModel
{
    public bool LoadActive { get; set; } = false;
    public bool ChargeActive { get; set; } = false;
    public bool BalancingActive { get; set; } = false;
    public double Voltage { get; set; } = 0;
    public double Current { get; set; } = 0;
    public double MaxCellV { get; set; } = 0;
    public double MinCellV { get; set; } = 0;
    public double Dcli { get; set; } = 0;
    public double Dclo { get; set; } = 0;
    public double Temperature { get; set; } = 0;
    public double DeltaCellV { get; set; } = 0;
    public double DeltaCellT { get; set; } = 0;
    public double[] CellVoltages {  get; set; } = new double[14];
}