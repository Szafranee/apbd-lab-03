using Lab_03.Interfaces;

namespace Lab_03.Containers;

public class BaseContainer : IContainer
{
    private static int _counter = 0;
    public double MaxLoad { get; }
    public double Height { get; }
    public double OwnWeight { get; }
    public double Depth { get; }
    public string SerialNumber { get; }
    public double CurrentLoad { get; set; }

    public BaseContainer(double maxLoad, double height, double ownWeight, double depth, string containerType)
    {
        MaxLoad = maxLoad;
        Height = height;
        OwnWeight = ownWeight;
        Depth = depth;
        SerialNumber = $"KON-{containerType}-{++_counter}";
    }

    public virtual void Load(double loadWeight)
    {
        if (CurrentLoad + loadWeight > MaxLoad)
        {
            throw new OverfillException("Load weight exceeds the container's maximum load.");
        }
        CurrentLoad += loadWeight;
    }

    public virtual void Unload()
    {
        CurrentLoad = 0;
    }

    public override string ToString()
    {
        return $"Container: {SerialNumber}\n" +
               $"Container Type: Base\n" +
               $"Load: {CurrentLoad} out of {MaxLoad}\n";
    }
}