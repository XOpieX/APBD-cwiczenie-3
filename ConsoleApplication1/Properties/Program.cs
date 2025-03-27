using System;
using System.Collections.Generic;
using ConsoleApplication1.Properties;

public class Program
{
    private static List<ContainerShip> ships = new List<ContainerShip>();
    private static List<Container> containers = new List<Container>();

    public static void Main()
    {
        bool exit = false;
        while (!exit)
        {
            Console.Clear();
            PrintMainMenu();
            Console.Write("Wybierz opcje: ");
            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    AddContainerShip();
                    break;
                case "2":
                    RemoveContainerShip();
                    break;
                case "3":
                    AddContainer();
                    break;
                case "4":
                    RemoveContainer();
                    break;
                case "5":
                    LoadContainerToShip();
                    break;
                case "6":
                    UnloadContainerFromShip();
                    break;
                case "7":
                    ReplaceContainerOnShip();
                    break;
                case "8":
                    TransferContainerBetweenShips();
                    break;
                case "9":
                    PrintAllInfo();
                    break;
                case "0":
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Nieprawidlowa opcja. Nacisnij dowolny przycisk aby kontynuowac...");
                    Console.ReadKey();
                    break;
            }
        }
    }

    private static void PrintMainMenu()
    {
        Console.WriteLine("System zarzadzania kontenerowcami");
        Console.WriteLine("===============================");
        
        Console.WriteLine("Lista kontenerowcow:");
        if (ships.Count == 0)
        {
            Console.WriteLine("  Brak");
        }
        else
        {
            foreach (var ship in ships)
            {
                Console.WriteLine($"  {ship.Name} (predkosc={ship.MaxSpeed}, maxKontenerow={ship.MaxContainerCount}, maxWaga={ship.MaxWeight/1000}t)");
            }
        }
        
        Console.WriteLine("\nLista kontenerow:");
        if (containers.Count == 0)
        {
            Console.WriteLine("  Brak");
        }
        else
        {
            foreach (var container in containers)
            {
                Console.WriteLine($"  {container.SerialNumber}");
            }
        }
        
        Console.WriteLine("\nDostepne akcje:");
        Console.WriteLine("1. Dodaj kontenerowiec");
        Console.WriteLine("2. Usun kontenerowiec");
        Console.WriteLine("3. Dodaj kontener");
        Console.WriteLine("4. Usun kontener");
        Console.WriteLine("5. Zaladuj kontener na statek");
        Console.WriteLine("6. Rozladuj kontener ze statku");
        Console.WriteLine("7. Zastap kontener na statku");
        Console.WriteLine("8. Przenies kontener miedzy statkami");
        Console.WriteLine("9. Wyswietl wszystkie informacje");
        Console.WriteLine("0. Wyjdz");
    }

    private static void AddContainerShip()
    {
        Console.Clear();
        Console.WriteLine("Dodaj nowy kontenerowiec");
        Console.WriteLine("----------------------");
        
        Console.Write("Nazwa statku: ");
        string name = Console.ReadLine();
        
        Console.Write("Maksymalna predkosc (wezly): ");
        double speed = double.Parse(Console.ReadLine());
        
        Console.Write("Maksymalna liczba kontenerow: ");
        int containerCount = int.Parse(Console.ReadLine());
        
        Console.Write("Maksymalna waga (tony): ");
        double weight = double.Parse(Console.ReadLine());
        
        ships.Add(new ContainerShip(name, speed, containerCount, weight));
        Console.WriteLine($"Statek {name} dodany pomyslnie. Nacisnij dowolny przycisk aby kontynuowac...");
        Console.ReadKey();
    }

    private static void RemoveContainerShip()
    {
        Console.Clear();
        Console.WriteLine("Usun kontenerowiec");
        Console.WriteLine("---------------------");
        
        if (ships.Count == 0)
        {
            Console.WriteLine("Brak dostepnych statkow. Nacisnij dowolny przycisk aby kontynuowac...");
            Console.ReadKey();
            return;
        }
        
        for (int i = 0; i < ships.Count; i++)
        {
            Console.WriteLine($"{i+1}. {ships[i].Name}");
        }
        
        Console.Write("Wybierz statek do usuniecia: ");
        int choice = int.Parse(Console.ReadLine()) - 1;
        
        if (choice >= 0 && choice < ships.Count)
        {
            string name = ships[choice].Name;
            ships.RemoveAt(choice);
            Console.WriteLine($"Statek {name} usuniety pomyslnie. Nacisnij dowolny przycisk aby kontynuowac...");
        }
        else
        {
            Console.WriteLine("Nieprawidlowy wybor. Nacisnij dowolny przycisk aby kontynuowac...");
        }
        Console.ReadKey();
    }

    private static void AddContainer()
    {
        Console.Clear();
        Console.WriteLine("Dodaj nowy kontener");
        Console.WriteLine("-----------------");
        
        Console.WriteLine("Wybierz typ kontenera:");
        Console.WriteLine("1. Chlodniczy");
        Console.WriteLine("2. Na plyny");
        Console.WriteLine("3. Na gaz");
        Console.Write("Twoj wybor: ");
        int typeChoice = int.Parse(Console.ReadLine());
        
        Console.Write("Wysokosc (cm): ");
        double height = double.Parse(Console.ReadLine());
        
        Console.Write("Waga wlasna (kg): ");
        double tareWeight = double.Parse(Console.ReadLine());
        
        Console.Write("Glebokosc (cm): ");
        double depth = double.Parse(Console.ReadLine());
        
        Console.Write("Maksymalna ladownosc (kg): ");
        double maxPayload = double.Parse(Console.ReadLine());
        
        Container container = null;
        
        try
        {
            switch (typeChoice)
            {
                case 1:
                    Console.Write("Typ produktu: ");
                    string productType = Console.ReadLine();
                    
                    Console.Write("Temperatura (C): ");
                    double temperature = double.Parse(Console.ReadLine());
                    
                    container = new RefrigeratedContainer(height, tareWeight, depth, maxPayload, productType, temperature);
                    break;
                case 2:
                    Console.Write("Czy niebezpieczny (true/false): ");
                    bool isHazardous = bool.Parse(Console.ReadLine());
                    
                    container = new LiquidContainer(height, tareWeight, depth, maxPayload, isHazardous);
                    break;
                case 3:
                    Console.Write("Cisnienie (atm): ");
                    double pressure = double.Parse(Console.ReadLine());
                    
                    container = new GasContainer(height, tareWeight, depth, maxPayload, pressure);
                    break;
                default:
                    Console.WriteLine("Nieprawidlowy typ kontenera. Nacisnij dowolny przycisk aby kontynuowac...");
                    Console.ReadKey();
                    return;
            }
            
            containers.Add(container);
            Console.WriteLine($"Kontener {container.SerialNumber} dodany pomyslnie. Nacisnij dowolny przycisk aby kontynuowac...");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Blad podczas tworzenia kontenera: {ex.Message}. Nacisnij dowolny przycisk aby kontynuowac...");
        }
        
        Console.ReadKey();
    }

    private static void RemoveContainer()
    {
        Console.Clear();
        Console.WriteLine("Usun kontener");
        Console.WriteLine("----------------");
        
        if (containers.Count == 0)
        {
            Console.WriteLine("Brak dostepnych kontenerow. Nacisnij dowolny przycisk aby kontynuowac...");
            Console.ReadKey();
            return;
        }
        
        for (int i = 0; i < containers.Count; i++)
        {
            Console.WriteLine($"{i+1}. {containers[i].SerialNumber}");
        }
        
        Console.Write("Wybierz kontener do usuniecia: ");
        int choice = int.Parse(Console.ReadLine()) - 1;
        
        if (choice >= 0 && choice < containers.Count)
        {
            string serial = containers[choice].SerialNumber;
            containers.RemoveAt(choice);
            Console.WriteLine($"Kontener {serial} usuniety pomyslnie. Nacisnij dowolny przycisk aby kontynuowac...");
        }
        else
        {
            Console.WriteLine("Nieprawidlowy wybor. Nacisnij dowolny przycisk aby kontynuowac...");
        }
        Console.ReadKey();
    }

    private static void LoadContainerToShip()
    {
        Console.Clear();
        Console.WriteLine("Zaladuj kontener na statek");
        Console.WriteLine("----------------------");
        
        if (ships.Count == 0 || containers.Count == 0)
        {
            Console.WriteLine("Brak statkow lub kontenerow. Nacisnij dowolny przycisk aby kontynuowac...");
            Console.ReadKey();
            return;
        }
        
        Console.WriteLine("Dostepne statki:");
        for (int i = 0; i < ships.Count; i++)
        {
            Console.WriteLine($"{i+1}. {ships[i].Name}");
        }
        Console.Write("Wybierz statek: ");
        int shipChoice = int.Parse(Console.ReadLine()) - 1;
        
        Console.WriteLine("\nDostepne kontenery:");
        for (int i = 0; i < containers.Count; i++)
        {
            Console.WriteLine($"{i+1}. {containers[i].SerialNumber}");
        }
        Console.Write("Wybierz kontener: ");
        int containerChoice = int.Parse(Console.ReadLine()) - 1;
        
        if (shipChoice >= 0 && shipChoice < ships.Count && 
            containerChoice >= 0 && containerChoice < containers.Count)
        {
            try
            {
                ships[shipChoice].LoadContainer(containers[containerChoice]);
                containers.RemoveAt(containerChoice);
                Console.WriteLine("Kontener zaladowany pomyslnie. Nacisnij dowolny przycisk aby kontynuowac...");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Blad podczas ladowania kontenera: {ex.Message}. Nacisnij dowolny przycisk aby kontynuowac...");
            }
        }
        else
        {
            Console.WriteLine("Nieprawidlowy wybor. Nacisnij dowolny przycisk aby kontynuowac...");
        }
        Console.ReadKey();
    }

    private static void UnloadContainerFromShip()
    {
        Console.Clear();
        Console.WriteLine("Rozladuj kontener ze statku");
        Console.WriteLine("--------------------------");
        
        if (ships.Count == 0)
        {
            Console.WriteLine("Brak dostepnych statkow. Nacisnij dowolny przycisk aby kontynuowac...");
            Console.ReadKey();
            return;
        }
        
        Console.WriteLine("Dostepne statki:");
        for (int i = 0; i < ships.Count; i++)
        {
            Console.WriteLine($"{i+1}. {ships[i].Name}");
        }
        Console.Write("Wybierz statek: ");
        int shipChoice = int.Parse(Console.ReadLine()) - 1;
        
        if (shipChoice >= 0 && shipChoice < ships.Count)
        {
            var ship = ships[shipChoice];
            try
            {
                Console.Write("Podaj numer seryjny kontenera do rozladunku: ");
                string serial = Console.ReadLine();
                
                ship.UnloadContainer(serial);
                Console.WriteLine("Kontener rozladowany pomyslnie. Nacisnij dowolny przycisk aby kontynuowac...");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Blad podczas rozladunku kontenera: {ex.Message}. Nacisnij dowolny przycisk aby kontynuowac...");
            }
        }
        else
        {
            Console.WriteLine("Nieprawidlowy wybor. Nacisnij dowolny przycisk aby kontynuowac...");
        }
        Console.ReadKey();
    }

    private static void ReplaceContainerOnShip()
    {
        Console.Clear();
        Console.WriteLine("Zastap kontener na statku");
        Console.WriteLine("-------------------------");
        
        if (ships.Count == 0 || containers.Count == 0)
        {
            Console.WriteLine("Brak statkow lub kontenerow. Nacisnij dowolny przycisk aby kontynuowac...");
            Console.ReadKey();
            return;
        }
        
        Console.WriteLine("Dostepne statki:");
        for (int i = 0; i < ships.Count; i++)
        {
            Console.WriteLine($"{i+1}. {ships[i].Name}");
        }
        Console.Write("Wybierz statek: ");
        int shipChoice = int.Parse(Console.ReadLine()) - 1;
        
        Console.WriteLine("\nDostepne kontenery:");
        for (int i = 0; i < containers.Count; i++)
        {
            Console.WriteLine($"{i+1}. {containers[i].SerialNumber}");
        }
        Console.Write("Wybierz nowy kontener: ");
        int containerChoice = int.Parse(Console.ReadLine()) - 1;
        
        if (shipChoice >= 0 && shipChoice < ships.Count && 
            containerChoice >= 0 && containerChoice < containers.Count)
        {
            try
            {
                Console.Write("Podaj numer seryjny kontenera do zastapienia: ");
                string oldSerial = Console.ReadLine();
                
                ships[shipChoice].ReplaceContainer(oldSerial, containers[containerChoice]);
                containers.RemoveAt(containerChoice);
                Console.WriteLine("Kontener zastapiony pomyslnie. Nacisnij dowolny przycisk aby kontynuowac...");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Blad podczas zastapienia kontenera: {ex.Message}. Nacisnij dowolny przycisk aby kontynuowac...");
            }
        }
        else
        {
            Console.WriteLine("Nieprawidlowy wybor. Nacisnij dowolny przycisk aby kontynuowac...");
        }
        Console.ReadKey();
    }

    private static void TransferContainerBetweenShips()
    {
        Console.Clear();
        Console.WriteLine("Przenies kontener miedzy statkami");
        Console.WriteLine("-------------------------------");
        
        if (ships.Count < 2)
        {
            Console.WriteLine("Wymagane sa co najmniej 2 statki do transferu. Nacisnij dowolny przycisk aby kontynuowac...");
            Console.ReadKey();
            return;
        }
        
        Console.WriteLine("Dostepne statki:");
        for (int i = 0; i < ships.Count; i++)
        {
            Console.WriteLine($"{i+1}. {ships[i].Name}");
        }
        
        Console.Write("Wybierz statek zrodlowy: ");
        int sourceChoice = int.Parse(Console.ReadLine()) - 1;
        
        Console.Write("Wybierz statek docelowy: ");
        int destChoice = int.Parse(Console.ReadLine()) - 1;
        
        if (sourceChoice >= 0 && sourceChoice < ships.Count && 
            destChoice >= 0 && destChoice < ships.Count && 
            sourceChoice != destChoice)
        {
            try
            {
                Console.Write("Podaj numer seryjny kontenera do przeniesienia: ");
                string serial = Console.ReadLine();
                
                ships[sourceChoice].TransferContainer(ships[destChoice], serial);
                Console.WriteLine("Kontener przeniesiony pomyslnie. Nacisnij dowolny przycisk aby kontynuowac...");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Blad podczas przenoszenia kontenera: {ex.Message}. Nacisnij dowolny przycisk aby kontynuowac...");
            }
        }
        else
        {
            Console.WriteLine("Nieprawidlowy wybor. Nacisnij dowolny przycisk aby kontynuowac...");
        }
        Console.ReadKey();
    }

    private static void PrintAllInfo()
    {
        Console.Clear();
        Console.WriteLine("Wszystkie informacje");
        Console.WriteLine("---------------");
        
        Console.WriteLine("\nKontenerowce:");
        foreach (var ship in ships)
        {
            ship.PrintShipInfo();
            Console.WriteLine();
        }
        
        Console.WriteLine("\nDostepne kontenery:");
        foreach (var container in containers)
        {
            container.PrintInfo();
            Console.WriteLine();
        }
        
        Console.WriteLine("Nacisnij dowolny przycisk aby kontynuowac...");
        Console.ReadKey();
    }
}