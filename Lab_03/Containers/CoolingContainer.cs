using Lab_03.Interfaces;

namespace Lab_03.Containers;

public class CoolingContainer : BaseContainer, IHazardNotifier
{
    public string productType { get; }
    public double minTemperature { get; }
    public double currTemperature { get; private set; }

    public CoolingContainer(double maxLoad, double height, double ownWeight, double depth, string productType, double minTemperature) : base(maxLoad, height, ownWeight, depth, "C")
    {
        this.productType = productType;
        this.minTemperature = minTemperature;
        currTemperature = minTemperature;
    }


    public void Load(string productType, double loadWeight)
    {
        if (this.productType != productType)
        {
            throw new ArgumentException("This container can only store products of type " + this.productType);
        }

        if (this.currTemperature < this.minTemperature)
        {
            NotifyHazard();
            Console.WriteLine($"The temperature in container {SerialNumber} is {currTemperature}°C, which is below the minimum of {minTemperature}°C");
        }

        if (CurrentLoad + loadWeight > MaxLoad)
        {
            throw new OverfillException("Load weight exceeds the container's maximum load.");
        }

        CurrentLoad += loadWeight;
    }

    public void NotifyHazard()
    {
        Console.WriteLine($"Hazard situation in container {SerialNumber}!");
    }

    public override string ToString()
    {
        return $"Container: {SerialNumber}\n" +
               $"Container Type: Cooling\n" +
               $"Load: {CurrentLoad} out of {MaxLoad}\n" +
               $"Product Type: {productType}\n" +
               $"Temperature: {currTemperature}°C\n";
    }
}