namespace Lab_03;

public class LiquidContainer : BaseContainer, IHazardNotifier
{
    private bool IsDangerousLoad { get; }

    public LiquidContainer(double maxLoad, double height, double ownWeight, double depth, bool isDangerousLoad) : base(maxLoad, height, ownWeight, depth, "LIQ")
    {
        IsDangerousLoad = isDangerousLoad;
    }

    public override void Load(double loadWeight)
    {
        var maxAllowedLoad = IsDangerousLoad ? MaxLoad * 0.5 : MaxLoad * 0.9;

        if (CurrentLoad + loadWeight > maxAllowedLoad)
        {
            throw new OverfillException("Load weight exceeds the container's maximum load.");
        }

        CurrentLoad += loadWeight;
    }

    public void NotifyHazard()
    {
        Console.WriteLine($"Hazard situation in container {SerialNumber}!");
    }
}