using System;
using System.Collections.Generic;

namespace ConsoleApplication1.Properties
{
    public class RefrigeratedContainer : Container
    {
        public string ProductType { get; private set; }
        public double Temperature { get; private set; }

        private static readonly Dictionary<string, double> ProductTemperatures = new Dictionary<string, double>
        {
            {"Bananas", 13.3},
            {"Chocolate", 18},
            {"Fish", 2},
            {"Meat", -15},
            {"Ice cream", -18},
            {"Frozen pizza", -30},
            {"Cheese", 7.2},
            {"Sausages", 5},
            {"Butter", 20.5},
            {"Eggs", 19}
        };

        public RefrigeratedContainer(double height, double tareWeight, double depth, double maxPayload, 
            string productType, double temperature) 
            : base(height, tareWeight, depth, maxPayload, "C")
        {
            if (!ProductTemperatures.ContainsKey(productType))
            {
                throw new ArgumentException($"Nieznany typ produktu: {productType}");
            }

            double requiredTemp = ProductTemperatures[productType];
            if (temperature < requiredTemp)
            {
                throw new ArgumentException($"Temperatura {temperature} jest za niska dla {productType} (wymagane min {requiredTemp})");
            }

            ProductType = productType;
            Temperature = temperature;
        }

        public override void PrintInfo()
        {
            Console.WriteLine($"Kontener chlodniczy {SerialNumber}");
            Console.WriteLine($"  Produkt: {ProductType}");
            Console.WriteLine($"  Temperatura: {Temperature}C");
            Console.WriteLine($"  Masa ladunku: {CargoMass}kg");
            Console.WriteLine($"  Waga wlasna: {TareWeight}kg");
            Console.WriteLine($"  Laczna waga: {GetTotalWeight()}kg");
            Console.WriteLine($"  Wymiary: {Height}x{Depth}cm");
            Console.WriteLine($"  Maksymalna ladownosc: {MaxPayload}kg");
        }
    }
}