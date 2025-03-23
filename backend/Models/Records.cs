public record BrickData(
    bool LoadActive,
    double ChargeActive,
    bool BalancingActive,
    double Voltage,
    double MaxCellV,
    double MinCellV,
    double Current,
    double Dcli,
    double Dclo,
    double Temperature,
    double DeltaCellV,
    double[] Cells
);

public record PowerData(
    bool Active,
    double Voltage,
    double CurrentDischarge,
    double CurrentCharge,
    double PowerDischarge,
    double PowerCharge
);

public record BmsData(BrickData BrickData, PowerData PowerData);