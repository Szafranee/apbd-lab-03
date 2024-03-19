using Lab_03.Interfaces;

namespace Lab_03.Containers;

public class GasContainer : BaseContainer, IHazardNotifier
{
    public GasContainer(double maxLoad, double ownWeight, double height,  double depth) : base(maxLoad,  ownWeight, height, depth, "G")
    {
    }

    public override void Unload()
    {
        CurrentLoad *= 0.05;
    }


    public void NotifyHazard()
    {
        Console.WriteLine($"Hazard situation in container {SerialNumber}!");
    }

    public override string ToString()
    {
        return $"Container: {SerialNumber}\n" +
               $"Container Type: Gas\n" +
               $"Load: {CurrentLoad} out of {MaxLoad}\n";
    }
}