namespace ApplicationNo1.Vehicle_
{
    public interface IVehicle
    {
        #region Properties
        double FuelLevel { get; }
        double MaxFuel { get; }
        double RefuelAmount { get; }
        double KmCounter { get; set; }
        string? SearchTerm { get; set; }
        string? Name { get; set; }
        #endregion

        #region Methods
        VehicleBase.RefuelResults Refuel(double money, double gasPrice);
        bool Drive(double distance);

        #endregion

    }
}
