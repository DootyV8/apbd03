class Program
{
    static void Main()
    {
        try
        {
            ContainerShip ship1 = new ContainerShip(30, 10, 50000);
            ContainerShip ship2 = new ContainerShip(28, 8, 40000);

            RefrigeratedContainer bananaContainer = new RefrigeratedContainer("Bananas", 5, 2000, 250, 600, 8000);
            LiquidContainer fuelContainer = new LiquidContainer(true, 1500, 300, 700, 12000);
            GasContainer heliumContainer = new GasContainer(15, 1800, 280, 650, 10000);

            bananaContainer.LoadCargo(7000);
            fuelContainer.LoadCargo(5000);
            heliumContainer.LoadCargo(9500);

            ship1.LoadContainers(new List<Container> { bananaContainer, fuelContainer });
            ship2.LoadContainer(heliumContainer);

            Console.WriteLine("Before Transfer:");
            ship1.PrintShipInfo();
            ship2.PrintShipInfo();

            ship1.TransferContainerTo(ship2, bananaContainer.SerialNumber);

            Console.WriteLine("\nAfter Transfer:");
            ship1.PrintShipInfo();
            ship2.PrintShipInfo();

            ship2.RemoveContainer(heliumContainer.SerialNumber);
            ship2.PrintShipInfo();
        }
        catch (OverfillException ex)
        {
            Console.WriteLine(ex.Message);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
    }
}