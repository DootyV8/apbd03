
public interface IHazardNotifier
{
    void NotifyHazard(string message);
}

public abstract class Container
{
    private static int counter = 1;
    public string SerialNumber { get; private set; }
    public double MaxPayload { get; protected set; }
    public double CargoWeight { get; protected set; }
    public string ProductType { get; protected set; }

    public Container(string type)
    {
        SerialNumber = $"KON-{type}-{counter++}";
    }

    public virtual void LoadCargo(double weight)
    {
        if (CargoWeight + weight > MaxPayload)
            throw new Exception("OverfillException: Exceeded container capacity!");
        CargoWeight += weight;
    }
    
    public virtual void UnloadCargo()
    {
        CargoWeight = 0;
    }

    public override string ToString()
    {
        return $"{SerialNumber}: {CargoWeight}/{MaxPayload} kg";
    }
}

public class LiquidContainer : Container, IHazardNotifier
{
    public bool IsHazardous { get; private set; }

    public LiquidContainer(bool isHazardous) : base("L")
    {
        MaxPayload = isHazardous ? 5000 : 10000;
        IsHazardous = isHazardous;
    }

    public override void LoadCargo(double weight)
    {
        double allowedWeight = IsHazardous ? MaxPayload * 0.5 : MaxPayload * 0.9;
        if (weight > allowedWeight)
            NotifyHazard("Attempted dangerous load operation!");
        base.LoadCargo(weight);
    }

    public void NotifyHazard(string message)
    {
        Console.WriteLine($"[HAZARD] {message} - {SerialNumber}");
    }
}

public class GasContainer : Container, IHazardNotifier
{
    public double Pressure { get; private set; }

    public GasContainer(double pressure) : base("G")
    {
        Pressure = pressure;
        MaxPayload = 3000;
    }

    public override void UnloadCargo()
    {
        CargoWeight *= 0.05; // Leave 5% inside
    }

    public void NotifyHazard(string message)
    {
        Console.WriteLine($"[HAZARD] {message} - {SerialNumber}");
    }
}

public class RefrigeratedContainer : Container
{
    public double Temperature { get; private set; }

    public RefrigeratedContainer(string productType, double temperature) : base("C")
    {
        ProductType = productType;
        Temperature = temperature;
        MaxPayload = 8000;
    }
}

public class Ship
{
    public string Name { get; private set; }
    public int MaxContainers { get; private set; }
    public double MaxWeight { get; private set; } // in tons
    private List<Container> containers = new List<Container>();

    public Ship(string name, int maxContainers, double maxWeight)
    {
        Name = name;
        MaxContainers = maxContainers;
        MaxWeight = maxWeight;
    }

    public void LoadContainer(Container container)
    {
        if (containers.Count >= MaxContainers)
            throw new Exception("Ship has reached maximum container capacity!");
        if (GetTotalWeight() + container.MaxPayload > MaxWeight * 1000)
            throw new Exception("Ship will exceed weight limit!");
        containers.Add(container);
    }

    public void RemoveContainer(string serialNumber)
    {
        containers.RemoveAll(c => c.SerialNumber == serialNumber);
    }

    public double GetTotalWeight()
    {
        double total = 0;
        foreach (var container in containers)
            total += container.MaxPayload;
        return total;
    }

    public void PrintInfo()
    {
        Console.WriteLine($"Ship: {Name}, Max Containers: {MaxContainers}, Max Weight: {MaxWeight} tons");
        foreach (var container in containers)
            Console.WriteLine(container);
    }
}

class Program
{
    static void Main()
    {
        Ship ship = new Ship("Poseidon", 10, 50);
        RefrigeratedContainer bananaContainer = new RefrigeratedContainer("Bananas", 5);
        GasContainer heliumContainer = new GasContainer(10);
        LiquidContainer milkContainer = new LiquidContainer(false);
        LiquidContainer fuelContainer = new LiquidContainer(true);

        try
        {
            milkContainer.LoadCargo(8000);
            fuelContainer.LoadCargo(3000); 
            heliumContainer.LoadCargo(2500);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        ship.LoadContainer(bananaContainer);
        ship.LoadContainer(heliumContainer);
        ship.LoadContainer(milkContainer);
        ship.LoadContainer(fuelContainer);

        ship.PrintInfo();
    }
}
