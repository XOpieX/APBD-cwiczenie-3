using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApplication1.Properties
{
    public class ContainerShip
    {
        public string Name { get; private set; }
        public double MaxSpeed { get; private set; }
        public int MaxContainerCount { get; private set; }
        public double MaxWeight { get; private set; }
        private List<Container> Containers { get; set; }

        public ContainerShip(string name, double maxSpeed, int maxContainerCount, double maxWeight)
        {
            Name = name;
            MaxSpeed = maxSpeed;
            MaxContainerCount = maxContainerCount;
            MaxWeight = maxWeight * 1000;
            Containers = new List<Container>();
        }

        public void LoadContainer(Container container)
        {
            if (Containers.Count >= MaxContainerCount)
            {
                throw new InvalidOperationException($"Nie mozna zaladowac kontenera - statek {Name} osiagnal maksymalna liczbe kontenerow ({MaxContainerCount})");
            }

            double totalWeight = Containers.Sum(c => c.GetTotalWeight()) + container.GetTotalWeight();
            if (totalWeight > MaxWeight)
            {
                throw new InvalidOperationException($"Nie mozna zaladowac kontenera - statek {Name} przekroczylby maksymalna wage ({MaxWeight/1000}t)");
            }

            Containers.Add(container);
        }

        public void LoadContainers(List<Container> containers)
        {
            foreach (var container in containers)
            {
                LoadContainer(container);
            }
        }

        public void UnloadContainer(string serialNumber)
        {
            var container = Containers.FirstOrDefault(c => c.SerialNumber == serialNumber);
            if (container != null)
            {
                container.EmptyCargo();
                Containers.Remove(container);
            }
            else
            {
                throw new KeyNotFoundException($"Nie znaleziono kontenera o numerze seryjnym {serialNumber} na statku {Name}");
            }
        }

        public void ReplaceContainer(string serialNumber, Container newContainer)
        {
            var index = Containers.FindIndex(c => c.SerialNumber == serialNumber);
            if (index != -1)
            {
                Containers[index] = newContainer;
            }
            else
            {
                throw new KeyNotFoundException($"Nie znaleziono kontenera o numerze seryjnym {serialNumber} na statku {Name}");
            }
        }

        public void TransferContainer(ContainerShip destinationShip, string serialNumber)
        {
            var container = Containers.FirstOrDefault(c => c.SerialNumber == serialNumber);
            if (container == null)
            {
                throw new KeyNotFoundException($"Nie znaleziono kontenera o numerze seryjnym {serialNumber} na statku {Name}");
            }

            try
            {
                destinationShip.LoadContainer(container);
                Containers.Remove(container);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Nie mozna przetransportowac kontenera: {ex.Message}");
            }
        }

        public void PrintShipInfo()
        {
            Console.WriteLine($"Statek: {Name}");
            Console.WriteLine($"  Maksymalna predkosc: {MaxSpeed} wezlow");
            Console.WriteLine($"  Maksymalna liczba kontenerow: {MaxContainerCount}");
            Console.WriteLine($"  Maksymalna waga: {MaxWeight/1000}t");
            Console.WriteLine($"  Biezaca liczba kontenerow: {Containers.Count}");
            Console.WriteLine($"  Biezaca laczna waga: {Containers.Sum(c => c.GetTotalWeight())/1000}t");
        
            Console.WriteLine("Kontenery na pokladzie:");
            foreach (var container in Containers)
            {
                container.PrintInfo();
            }
        }
    }
}