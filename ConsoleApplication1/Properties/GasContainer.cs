using System;

namespace ConsoleApplication1.Properties
{
    public class GasContainer : Container, IHazardNotifier
    {
        public double Pressure { get; private set; }

        public GasContainer(double height, double tareWeight, double depth, double maxPayload, double pressure) 
            : base(height, tareWeight, depth, maxPayload, "G")
        {
            Pressure = pressure;
        }

        public override void EmptyCargo()
        {
            CargoMass = MaxPayload * 0.05;
        }

        public void NotifyHazard(string containerNumber)
        {
            Console.WriteLine($"OSTRZEZENIE O NIEBEZPIECZENSTWIE: Proba niebezpiecznej operacji na kontenerze gazowym {containerNumber}");
        }

        public override void PrintInfo()
        {
            Console.WriteLine($"Kontener na gaz {SerialNumber}");
            Console.WriteLine($"  Cisnienie: {Pressure} atm");
            Console.WriteLine($"  Masa ladunku: {CargoMass}kg");
            Console.WriteLine($"  Waga wlasna: {TareWeight}kg");
            Console.WriteLine($"  Laczna waga: {GetTotalWeight()}kg");
            Console.WriteLine($"  Wymiary: {Height}x{Depth}cm");
            Console.WriteLine($"  Maksymalna ladownosc: {MaxPayload}kg");
        }
    }
}