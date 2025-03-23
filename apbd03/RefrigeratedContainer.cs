public class RefrigeratedContainer : Container
{
    public string ProductType { get; private set; }
    public double RequiredTemperature { get; private set; }
    public double CurrentTemperature { get; private set; }

    public RefrigeratedContainer(string productType, double requiredTemp, double tareWeight, double height, double depth, double maxPayload)
        : base("C", tareWeight, height, depth, maxPayload)
    {
        ProductType = productType;
        RequiredTemperature = requiredTemp;
        CurrentTemperature = requiredTemp; 
    }

    public void SetTemperature(double temperature)
    {
        if (temperature < RequiredTemperature)
            throw new Exception($"Temperature too low! {ProductType} requires at least {RequiredTemperature}.");
        CurrentTemperature = temperature;
    }
}