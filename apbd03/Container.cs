public abstract class Container
{
    private static int counter = 1;
    public string SerialNumber { get; private set; }
    public double MaxPayload { get; protected set; }
    public double CargoWeight { get; protected set; }
    public double TareWeight { get; protected set; }
    public double Height { get; protected set; }
    public double Depth { get; protected set; }

    public Container(string type, double tareWeight, double height, double depth, double maxPayload)
    {
        SerialNumber = $"KON-{type}-{counter++}";
        TareWeight = tareWeight;
        Height = height;
        Depth = depth;
        MaxPayload = maxPayload;
    }

    public virtual void LoadCargo(double weight)
    {
        if (CargoWeight + weight > MaxPayload)
            throw new OverfillException("OverfillException: Exceeded container capacity!");
        CargoWeight += weight;
    }

    public virtual void UnloadCargo()
    {
        CargoWeight = 0;
    }

    public override string ToString()
    {
        return
            $"{SerialNumber}: {CargoWeight}/{MaxPayload} kg, Tare Weight: {TareWeight} kg, Height: {Height} cm, Depth: {Depth} cm";
    }
}