namespace Lab_03;

public class GasContainer : BaseContainer, IHazardNotifier
{
    public GasContainer(double maxLoad, double height, double ownWeight, double depth) : base(maxLoad, height, ownWeight, depth, "G")
    {
    }

    public override void Unload()
    {
        CurrentLoad = CurrentLoad * 0.05;
    }


    public void NotifyHazard()
    {
        Console.WriteLine($"Hazard situation in container {SerialNumber}!");
    }
}