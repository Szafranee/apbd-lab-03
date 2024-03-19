using Lab_03.Interfaces;
using Lab_03.Exceptions;

namespace Lab_03.Containers;

public class BaseContainer : IContainer
{
    private static int _counter = 0;
    public double MaxLoad { get; }
    public double OwnWeight { get; }
    public double Height { get; }
    public double Depth { get; }
    public string SerialNumber { get; }
    public double CurrentLoad { get; set; }

    public BaseContainer(double maxLoad, double ownWeight, double height, double depth, string containerType)
    {
        MaxLoad = maxLoad;
        OwnWeight = ownWeight;
        Height = height;
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