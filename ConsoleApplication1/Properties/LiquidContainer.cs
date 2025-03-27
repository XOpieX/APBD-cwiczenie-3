using System;

namespace ConsoleApplication1.Properties
{
    public class LiquidContainer : Container, IHazardNotifier
    {
        public bool IsHazardous { get; private set; }

        public LiquidContainer(double height, double tareWeight, double depth, double maxPayload, bool isHazardous) 
            : base(height, tareWeight, depth, maxPayload, "L")
        {
            IsHazardous = isHazardous;
        }

        public override void LoadCargo(double mass)
        {
            double maxAllowed = IsHazardous ? MaxPayload * 0.5 : MaxPayload * 0.9;
        
            if (mass > maxAllowed)
            {
                NotifyHazard(SerialNumber);
                throw new OverfillException($"Proba zaladowania {mass}kg do {(IsHazardous ? "niebezpiecznego" : "zwyklego")} kontenera z plynamy {SerialNumber}. Maksymalna dopuszczalna: {maxAllowed}kg");
            }

            base.LoadCargo(mass);
        }

        public void NotifyHazard(string containerNumber)
        {
            Console.WriteLine($"OSTRZEZENIE O NIEBEZPIECZENSTWIE: Proba niebezpiecznej operacji na kontenerze {containerNumber}");
        }

        public override void PrintInfo()
        {
            Console.WriteLine($"Kontener na plyny {SerialNumber}");
            Console.WriteLine($"  Niebezpieczny: {(IsHazardous ? "Tak" : "Nie")}");
            Console.WriteLine($"  Masa ladunku: {CargoMass}kg");
            Console.WriteLine($"  Waga wlasna: {TareWeight}kg");
            Console.WriteLine($"  Laczna waga: {GetTotalWeight()}kg");
            Console.WriteLine($"  Wymiary: {Height}x{Depth}cm");
            Console.WriteLine($"  Maksymalna ladownosc: {MaxPayload}kg");
        }
    }
}