public class GasContainer : Container, IHazardNotifier
{
    public double Pressure { get; private set; }

    public GasContainer(double pressure, double tareWeight, double height, double depth, double maxPayload)
        : base("G", tareWeight, height, depth, maxPayload)
    {
        Pressure = pressure;
    }

    public override void UnloadCargo()
    {
        CargoWeight *= 0.05;
    }

    public void NotifyHazard(string message)
    {
        Console.WriteLine($"Hazard Alert: {message} (Container: {SerialNumber})");
    }
}