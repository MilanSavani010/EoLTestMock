public class PSBModel
{
    public bool Active { get; set; } = false;
    public double Voltage { get; set; } = 0;
    public double CurrentCharge { get; set; } = 0;
    public double CurrentDischarge { get; set; } = 0;
    public double PowerCharge { get; set; } = 0;
    public double PowerDischarge { get; set; } = 0;
}

