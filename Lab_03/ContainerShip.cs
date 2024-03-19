using Lab_03.Containers;

namespace Lab_03;


public class ContainerShip(double maxSpeed, int maxContainers, double maxLoad, string name)
{
    public string Name { get; } = name;
    public double MaxSpeed { get; } = maxSpeed;
    private int MaxContainers { get; } = maxContainers;
    private double MaxLoad { get; } = maxLoad * 1000;
    public List<BaseContainer> Containers { get; } = [];

    public void LoadContainers(BaseContainer container)
    {
        if (Containers.Count >= MaxContainers)
        {
            throw new InvalidOperationException("The ship is full.");
        }

        foreach (var cont in Containers)
        {
            if (cont.CurrentLoad + container.CurrentLoad > MaxLoad)
            {
                throw new InvalidOperationException("The container's load exceeds the ship's maximum load.");
            }
        }
        Containers.Add(container);
    }

    public void UnloadAllContainers()
    {
        Containers.Clear();
    }

    private void UnloadContainer(string serialNumber)
    {
        var containerToRemove = Containers.Find(c => c.SerialNumber == serialNumber);
        if (containerToRemove == null)
        {
            throw new InvalidOperationException("The container with the given serial number does not exist.");
        };
        Containers.Remove(containerToRemove);
    }

    private void SwapContainers(string serialNumberToUnload, BaseContainer containerToLoad)
    {
        UnloadContainer(serialNumberToUnload);
        LoadContainers(containerToLoad);
    }

    public void MoveContainerToShip(string serialNumber, ContainerShip ship)
    {
        var containerToMove = Containers.Find(c => c.SerialNumber == serialNumber);
        if (containerToMove == null)
        {
            throw new InvalidOperationException("The container with the given serial number does not exist.");
        }
        ship.LoadContainers(containerToMove);
        Containers.Remove(containerToMove);
    }

    public override string ToString()
    {
        var containerCount = Containers.Count;
        var containerLoad = Containers.Sum(c => c.CurrentLoad);

        return $"Ship: {Name}\n" +
               $"Max Speed: {MaxSpeed} knots\n" +
               $"Loaded Containers: {containerCount}, Max: {MaxContainers} - {(containerCount / MaxContainers) * 100}% loaded\n" +
               $"Current Load: {containerLoad} out of {MaxLoad} tons\n - {(containerLoad / MaxLoad) * 100}% loaded\n" +
               $"Loaded containers:\n" +
               $"{Containers}";

    }
}