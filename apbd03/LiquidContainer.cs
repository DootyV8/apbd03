public class LiquidContainer : Container, IHazardNotifier
{
    public bool IsHazardous { get; private set; }

    public LiquidContainer(bool isHazardous, double tareWeight, double height, double depth, double maxPayload)
        : base("L", tareWeight, height, depth, maxPayload)
    {
        IsHazardous = isHazardous;
    }

    public override void LoadCargo(double weight)
    {
        double limit = IsHazardous ? MaxPayload * 0.5 : MaxPayload * 0.9;
        if (CargoWeight + weight > limit)
        {
            NotifyHazard("Attempted to exceed safe loading capacity!");
            throw new OverfillException("OverfillException: Exceeded container capacity!");
        }

        CargoWeight += weight;
    }

    public void NotifyHazard(string message)
    {
        Console.WriteLine($"Hazard Alert: {message} (Container: {SerialNumber})");
    }
}