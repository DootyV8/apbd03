public class ContainerShip
{
    public List<Container> Containers { get; private set; } = new List<Container>();
    public double MaxSpeed { get; private set; }
    public int MaxContainers { get; private set; }
    public double MaxWeight { get; private set; }

    public ContainerShip(double maxSpeed, int maxContainers, double maxWeight)
    {
        MaxSpeed = maxSpeed;
        MaxContainers = maxContainers;
        MaxWeight = maxWeight;
    }

    public void LoadContainer(Container container)
    {
        if (Containers.Count >= MaxContainers ||
            GetTotalWeight() + container.TareWeight + container.CargoWeight > MaxWeight)
            throw new Exception("Cannot load container: Ship capacity exceeded!");

        Containers.Add(container);
    }
    
    public void LoadContainers(List<Container> containers)
    {
        foreach (var container in containers)
        {
            LoadContainer(container);
        }
    }

    public void RemoveContainer(string serialNumber)
    {
        Containers.RemoveAll(c => c.SerialNumber == serialNumber);
    }
    
    public void ReplaceContainer(string serialNumber, Container newContainer)
    {
        RemoveContainer(serialNumber);
        LoadContainer(newContainer);
    }
    
    public void TransferContainerTo(ContainerShip destinationShip, string serialNumber)
    {
        Container container = Containers.FirstOrDefault(c => c.SerialNumber == serialNumber);
        if (container != null)
        {
            RemoveContainer(serialNumber);
            destinationShip.LoadContainer(container);
        }
        else
        {
            throw new Exception("Container not found on this ship!");
        }
    }


    public double GetTotalWeight()
    {
        double weight = 0;
        foreach (var container in Containers)
        {
            weight += container.TareWeight + container.CargoWeight;
        }

        return weight;
    }

    public void PrintShipInfo()
    {
        Console.WriteLine(
            $"Ship: MaxSpeed: {MaxSpeed} knots, Containers: {Containers.Count}/{MaxContainers}, Total Weight: {GetTotalWeight()}/{MaxWeight} tons");
        foreach (var container in Containers)
        {
            Console.WriteLine(container);
        }
    }
}