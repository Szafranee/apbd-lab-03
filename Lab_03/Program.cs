using Lab_03.Containers;
using Lab_03.Ships;

namespace Lab_03;

public class Program
{
    public static void Main(string[] args)
    {
        Menu();
    }

    private static void Menu()
    {
        Console.WriteLine("====================================================");
        Console.WriteLine(" Hello! Welcome to the container management system.");
        Console.WriteLine("====================================================");
        List<ContainerShip> containerShips = [];
        List<BaseContainer> containersInStorage = [];

        Dictionary<int, string> actions = new()
        {
            { 1, "Add a container ship" },
            { 2, "Add a container" },
            { 3, "Unload a container" },
            { 4, "Unload all containers" },
            { 5, "Remove a container" },
            { 6, "Remove a container ship" },
            { 7, "Load a container onto a ship" },
            { 8, "Load all containers onto a ship" },
            { 9, "Unload a container from a ship" },
            { 10, "Unload all containers from a ship" },
            { 11, "Move a container to another ship" },
            { 12, "Get list of containers on a ship" },
            { 0, "Exit" }
        };
        List<int> actionsAllowed = [0, 1, 2];

        while (true)
        {
            var containerShipsCount = containerShips.Count;
            var containersInStorageCount = containersInStorage.Count;

            Console.WriteLine();
            Console.WriteLine("----------------------------------------");

            // Container ships list printing
            if (containerShipsCount == 0)
            {
                Console.WriteLine("No container ships available.");
            }
            else
            {
                Console.WriteLine("Available container ships:");
                for (var i = 0; i < containerShipsCount; i++)
                {
                    Console.WriteLine($"{i + 1}. {containerShips[i].Name}");
                }
            }

            // Containers list printing
            if (containersInStorageCount == 0)
            {
                Console.WriteLine("No containers available.");
            }
            else
            {
                Console.WriteLine("Available containers:");
                for (var i = 0; i < containersInStorageCount; i++)
                {
                    Console.WriteLine($"{i + 1}. {containersInStorage[i].SerialNumber}");
                }
            }

            Console.WriteLine("----------------------------------------");
            Console.WriteLine();

            Console.WriteLine("----------------------------------------");
            Console.WriteLine("Possible actions:");

            // Rider zamienił if elsy na to:
            actionsAllowed = containerShipsCount switch
            {
                0 when containersInStorageCount == 0 => [1, 2, 0],
                1 when containersInStorageCount == 0 => [1, 2, 6, 9, 10, 12, 0],
                0 when containersInStorageCount == 1 => [1, 2, 3, 4, 5, 0],
                1 when containersInStorageCount == 1 => [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 12, 0],
                0 when containersInStorageCount > 1 => [1, 2, 3, 4, 5, 0],
                > 1 when containersInStorageCount == 0 => [1, 2, 6, 9, 10, 11, 12, 0],
                > 1 when containersInStorageCount == 1 => [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 0],
                1 when containersInStorageCount > 1 => [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 12, 0],
                > 1 when containersInStorageCount > 1 => [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 0],
                _ => actionsAllowed
            };

            // list of possible actions printing
            // print only allowed actions but enumerate them from 1, but also print 0 as exit at the end
            for (var i = 1; i < actionsAllowed.Count; i++)
            {
                Console.WriteLine($"{i}. {actions[actionsAllowed[i-1]]}");
            }
            Console.WriteLine("0. Exit");

            Console.WriteLine("----------------------------------------");

            Console.WriteLine("Please enter the number of the action you want to perform:");
            int actionNumber;

            while (true)
            {
                try
                {
                    var inputNumber = int.Parse(Console.ReadLine());
                    // if inputNumber is 0, then choose the last action which is always exit
                    if (inputNumber == 0)
                    {
                        inputNumber = actionsAllowed.Count;
                    }

                    actionNumber = actionsAllowed[inputNumber - 1];
                    if (actionNumber > actions.Count - 1 || actionNumber < 0)
                    {
                        throw new FormatException();
                    }

                    if (actionsAllowed.Contains(actionNumber))
                    {
                        Console.WriteLine($"You have chosen to {actions[actionNumber].ToLower()}.");
                        Console.WriteLine();
                        break;
                    }

                    Console.WriteLine("Invalid action number.");
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid action number.");
                }
            }

            switch (actionNumber)
            {
                case 1:
                    AddContainerShip(containerShips);
                    break;
                case 2:
                    AddContainer(containersInStorage);
                    break;
                case 3:
                    UnloadContainer(containersInStorage);
                    break;
                case 4:
                    foreach (var container in containersInStorage)
                    {
                        container.Unload();
                    }
                    break;
                case 5:
                    RemoveContainer(containersInStorage);
                    break;
                case 6:
                    RemoveContainerShip(containerShips);
                    break;
                case 7:
                    LoadContainerOntoShip(containerShips, containersInStorage);
                    break;
                case 8:
                    LoadAllContainersOntoShip(containerShips, containersInStorage);
                    break;
                case 9:
                    UnloadContainerFromShip(containerShips, containersInStorage);
                    break;
                case 10:
                    UnloadAllContainersFromShip(containerShips, containersInStorage);
                    break;
                case 11:
                    MoveContainerToAnotherShip(containerShips, containersInStorage);
                    break;
                case 12:
                    GetContainersOnShip(containerShips);
                    break;
                case 0:
                    Environment.Exit(0);
                    Console.WriteLine("Goodbye!");
                    break;
            }
        }
    }

    private static void AddContainerShip(List<ContainerShip> containerShips)
    {
        while (true)
        {
            try
            {
                Console.WriteLine("Please enter the name of the container ship:");
                var shipName = Console.ReadLine();

                Console.WriteLine("Please enter the maximum speed of the ship (in knots):");
                var shipMaxSpeed = double.Parse(Console.ReadLine() ?? throw new FormatException());

                Console.WriteLine("Please enter the maximum number of containers the ship can carry:");
                var shipMaxContainers = int.Parse(Console.ReadLine() ?? throw new FormatException());

                Console.WriteLine("Please enter the maximum load the ship can carry (in tonnes):");
                var shipMaxLoad = double.Parse(Console.ReadLine() ?? throw new FormatException());

                Console.WriteLine("Do you want to add this ship to the system? (y/n)");
                Console.WriteLine($"Name: {shipName}\n" +
                                  $"Max speed: {shipMaxSpeed} knots\n" +
                                  $"Max containers: {shipMaxContainers}\n" +
                                  $"Max load: {shipMaxLoad} tonnes");

                while (true)
                {
                    try
                    {
                        var addShip = Console.ReadLine();
                        if (addShip == "y")
                        {
                            containerShips.Add(
                                new ContainerShip(shipName, shipMaxSpeed, shipMaxContainers, shipMaxLoad));
                            Console.WriteLine($"The ship {shipName} has been added to the system.");
                            break;
                        }

                        if (addShip == "n")
                        {
                            Console.WriteLine("The ship has not been added to the system.");
                            break;
                        }

                        throw new FormatException();
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Invalid value. Please try again.");
                    }
                }

                break;
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid value. Please try again.");
            }
        }
    }

    private static void AddContainer(List<BaseContainer> containersInStorage)
    {
        while (true)
        {
            try
            {
                Console.WriteLine("Please enter the type of the container (liquid (l), gas (g), cooling (c)):");
                var containerType = Console.ReadLine();

                containerType = containerType?.ToLower();

                containerType = containerType switch
                {
                    "liquid" => "l",
                    "gas" => "g",
                    "cooling" => "c",
                    _ => containerType
                };

                if (containerType != "l" && containerType != "g" && containerType != "c")
                {
                    throw new FormatException();
                }

                Console.WriteLine("Please enter the maximum load of the container (in tonnes):");
                var containerMaxLoad = double.Parse(Console.ReadLine() ?? throw new FormatException());

                var containerOwnWeight = double.Parse(Console.ReadLine() ?? throw new FormatException());
                Console.WriteLine("Please enter the own weight of the container (in tonnes):");

                Console.WriteLine("Please enter the height of the container (in meters):");
                var containerHeight = double.Parse(Console.ReadLine() ?? throw new FormatException());

                Console.WriteLine("Please enter the depth of the container (in meters):");
                var containerDepth = double.Parse(Console.ReadLine() ?? throw new FormatException());

                switch (containerType)
                {
                    case "l":
                        while (true)
                        {
                            try
                            {
                                Console.WriteLine("Is the load dangerous? (y/n)");
                                var isDangerousLoad = Console.ReadLine() == "y";
                                containersInStorage.Add(
                                    new LiquidContainer(
                                        containerMaxLoad,
                                        containerOwnWeight,
                                        containerHeight,
                                        containerDepth,
                                        isDangerousLoad
                                    )
                                );
                                break;
                            }
                            catch (FormatException)
                            {
                                Console.WriteLine("Invalid value. Please try again.");
                            }
                        }

                        break;

                    case "g":
                        containersInStorage.Add(
                            new GasContainer(
                                containerMaxLoad,
                                containerOwnWeight,
                                containerHeight,
                                containerDepth
                            )
                        );
                        break;

                    case "c":
                        Console.WriteLine("Please enter the product type:");
                        var productType = Console.ReadLine();
                        Console.WriteLine("Please enter the minimum temperature:");
                        var minTemperature = double.Parse(Console.ReadLine() ?? throw new FormatException());
                        containersInStorage.Add(
                            new CoolingContainer(
                                containerMaxLoad,
                                containerOwnWeight,
                                containerHeight,
                                containerDepth,
                                productType,
                                minTemperature
                            )
                        );
                        break;
                }

                break;
            }
            catch (FormatException e)
            {
                Console.WriteLine("Invalid value. Please try again.");
            }
        }
    }

    private static void UnloadContainer(List<BaseContainer> containersInStorage)
    {
        Console.WriteLine("Please enter the serial number of the container you want to unload:");
        var serialNumber = Console.ReadLine();
        var containerToUnload = containersInStorage.Find(c => c.SerialNumber == serialNumber);
        if (containerToUnload == null)
        {
            Console.WriteLine("The container with the given serial number does not exist.");
            return;
        }

        containerToUnload.Unload();
        Console.WriteLine($"The container {serialNumber} has been unloaded.");
    }

    private static void RemoveContainer(List<BaseContainer> containersInStorage)
    {
        Console.WriteLine("Please enter the serial number of the container you want to remove:");
        var serialNumber = Console.ReadLine();
        var containerToRemove = containersInStorage.Find(c => c.SerialNumber == serialNumber);
        if (containerToRemove == null)
        {
            Console.WriteLine("The container with the given serial number does not exist.");
            return;
        }

        if (containerToRemove.CurrentLoad > 0)
        {
            Console.WriteLine("The container is not empty. Please unload it first.");
            return;
        }

        containersInStorage.Remove(containerToRemove);
        Console.WriteLine($"The container {serialNumber} has been removed.");
    }

    private static void RemoveContainerShip(List<ContainerShip> containerShips)
    {
        PrintShips(containerShips);

        Console.WriteLine("Please enter the name of the container ship you want to remove:");
        var shipName = Console.ReadLine();
        var shipToRemove = containerShips.Find(s => s.Name == shipName);
        if (shipToRemove == null)
        {
            Console.WriteLine("The ship with the given name does not exist.");
            return;
        }

        if (shipToRemove.Containers.Count > 0)
        {
            Console.WriteLine("The ship is not empty. Please unload it first.");
            return;
        }

        containerShips.Remove(shipToRemove);
        Console.WriteLine($"The ship {shipName} has been removed from the system.");
    }

    private static void LoadContainerOntoShip(List<ContainerShip> containerShips, List<BaseContainer> containersInStorage)
    {
        PrintShips(containerShips);

        Console.WriteLine("Please enter the name of the ship you want to load the container onto:");
        var shipName = Console.ReadLine();
        var shipToLoad = containerShips.Find(s => s.Name == shipName);
        if (shipToLoad == null)
        {
            Console.WriteLine("The ship with the given name does not exist.");
            return;
        }

        Console.WriteLine("Please enter the serial number of the container you want to load:");
        var serialNumber = Console.ReadLine();
        var containerToLoad = containersInStorage.Find(c => c.SerialNumber == serialNumber);
        if (containerToLoad == null)
        {
            Console.WriteLine("The container with the given serial number does not exist.");
            return;
        }

        try
        {
            shipToLoad.LoadContainers(containerToLoad);
            containersInStorage.Remove(containerToLoad);
            Console.WriteLine($"The container {serialNumber} has been loaded onto the ship {shipName}.");
        }
        catch (InvalidOperationException e)
        {
            Console.WriteLine(e.Message);
        }
    }

    private static void LoadAllContainersOntoShip(List<ContainerShip> containerShips, List<BaseContainer> containersInStorage)
    {
        PrintShips(containerShips);

        Console.WriteLine("Please enter the name of the ship you want to load the containers onto:");
        var shipName = Console.ReadLine();
        var shipToLoad = containerShips.Find(s => s.Name == shipName);
        if (shipToLoad == null)
        {
            Console.WriteLine("The ship with the given name does not exist.");
            return;
        }

        foreach (var container in containersInStorage)
        {
            try
            {
                shipToLoad.LoadContainers(container);
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine(e.Message);
                return;
            }
        }
        containersInStorage.Clear();
        Console.WriteLine($"All containers have been loaded onto the ship {shipName}.");
    }

    private static void UnloadContainerFromShip(List<ContainerShip> containerShips, List<BaseContainer> containersInStorage)
    {
        PrintShips(containerShips);

        Console.WriteLine("Please enter the name of the ship you want to unload the container from:");
        var shipName = Console.ReadLine();
        var shipToUnload = containerShips.Find(s => s.Name == shipName);
        if (shipToUnload == null)
        {
            Console.WriteLine("The ship with the given name does not exist.");
            return;
        }

        Console.WriteLine($"{shipName} containers:");
        shipToUnload.Containers.ForEach(Console.WriteLine);
        Console.WriteLine();

        Console.WriteLine("Please enter the serial number of the container you want to unload:");
        var serialNumber = Console.ReadLine();
        var containerToUnload = shipToUnload.Containers.Find(c => c.SerialNumber == serialNumber);
        if (containerToUnload == null)
        {
            Console.WriteLine("The container with the given serial number does not exist.");
            return;
        }

        shipToUnload.Containers.Remove(containerToUnload);
        containersInStorage.Add(containerToUnload);
        Console.WriteLine($"The container {serialNumber} has been unloaded from the ship {shipName}.");
    }

    private static void UnloadAllContainersFromShip(List<ContainerShip> containerShips, List<BaseContainer> containersInStorage)
    {
        PrintShips(containerShips);

        Console.WriteLine("Please enter the name of the ship you want to unload the containers from:");
        var shipName = Console.ReadLine();
        var shipToUnload = containerShips.Find(s => s.Name == shipName);
        if (shipToUnload == null)
        {
            Console.WriteLine("The ship with the given name does not exist.");
            return;
        }

        Console.WriteLine($"{shipName} containers:");
        shipToUnload.Containers.ForEach(Console.WriteLine);
        Console.WriteLine();

        containersInStorage.AddRange(shipToUnload.Containers);
        shipToUnload.UnloadAllContainers();
        Console.WriteLine($"All containers have been unloaded from the ship {shipName}.");
    }

    private static void MoveContainerToAnotherShip(List<ContainerShip> containerShips, List<BaseContainer> containersInStorage)
    {
        PrintShips(containerShips);

        Console.WriteLine("Please enter the name of the ship you want to move the container from:");
        var shipNameFrom = Console.ReadLine();
        var shipFrom = containerShips.Find(s => s.Name == shipNameFrom);
        if (shipFrom == null)
        {
            Console.WriteLine("The ship with the given name does not exist.");
            return;
        }

        Console.WriteLine("Please enter the name of the ship you want to move the container to:");
        var shipNameTo = Console.ReadLine();
        var shipTo = containerShips.Find(s => s.Name == shipNameTo);
        if (shipTo == null)
        {
            Console.WriteLine("The ship with the given name does not exist.");
            return;
        }

        Console.WriteLine($"{shipNameFrom} containers:");
        Console.WriteLine(shipFrom.Containers);
        Console.WriteLine();

        Console.WriteLine("Please enter the serial number of the container you want to move:");
        var serialNumber = Console.ReadLine();
        var containerToMove = shipFrom.Containers.Find(c => c.SerialNumber == serialNumber);
        if (containerToMove == null)
        {
            Console.WriteLine("The container with the given serial number does not exist.");
            return;
        }

        try
        {
            shipFrom.Containers.Remove(containerToMove);
            shipTo.LoadContainers(containerToMove);
            Console.WriteLine($"The container {serialNumber} has been moved from the ship {shipNameFrom} to the ship {shipNameTo}.");
        }
        catch (InvalidOperationException e)
        {
            Console.WriteLine(e.Message);
        }
    }

    private static void GetContainersOnShip(List<ContainerShip> containerShips)
    {
        PrintShips(containerShips);

        Console.WriteLine("Please enter the name of the ship you want to get the list of containers from:");

        var shipName = Console.ReadLine();
        var ship = containerShips.Find(s => s.Name == shipName);
        if (ship == null)
        {
            Console.WriteLine("The ship with the given name does not exist.");
            return;
        }

        Console.WriteLine($"{shipName} containers:\n");
        ship.Containers.ForEach(Console.WriteLine);
    }

    private static void PrintShips(List<ContainerShip> containerShips)
    {
        Console.WriteLine("==================================================");
        Console.WriteLine("Available container ships:");
        containerShips.ForEach(s => Console.WriteLine(s.Name));
        Console.WriteLine("==================================================");
    }
}