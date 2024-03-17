namespace Lab_03;

public class BaseContainer : IContainer
{
    private static int _counter = 0;
    public double MaxLoad { get; }
    public double Height { get; }
    public double OwnWeight { get; }
    public double Depth { get; }
    public string SerialNumber { get; }
    private double CurrentLoad { get; set; }

    public BaseContainer(double maxLoad, double height, double ownWeight, double depth, string containerType)
    {
        MaxLoad = maxLoad;
        Height = height;
        OwnWeight = ownWeight;
        Depth = depth;
        SerialNumber = $"KON-{containerType}-{++_counter}";
    }

    public void Load(double loadWeight)
    {
        if (CurrentLoad + loadWeight > MaxLoad)
        {
            throw new OverfillException("Load weight exceeds the container's maximum load.");
        }
        CurrentLoad += loadWeight;
    }

    public void Unload()
    {
        CurrentLoad = 0;
    }
}