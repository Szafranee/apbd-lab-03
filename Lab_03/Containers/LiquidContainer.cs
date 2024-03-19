using Lab_03.Interfaces;

namespace Lab_03.Containers;

public class LiquidContainer : BaseContainer, IHazardNotifier
{
    private bool IsDangerousLoad { get; }

    public LiquidContainer(double maxLoad, double ownWeight, double height, double depth, bool isDangerousLoad) : base(maxLoad, ownWeight, height, depth, "L")
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

    public override string ToString()
    {
        return $"Container: {SerialNumber}\n" +
               $"Container Type: Liquid\n" +
               $"Load: {CurrentLoad} out of {MaxLoad}\n" +
               $"Dangerous Load: {IsDangerousLoad}\n";
    }
}