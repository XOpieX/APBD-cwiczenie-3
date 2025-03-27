
namespace ConsoleApplication1.Properties
{
    public abstract class Container
    {
        public double CargoMass { get; protected set; }
        public double Height { get; private set; }
        public double TareWeight { get; private set; }
        public double Depth { get; private set; }
        public string SerialNumber { get; private set; }
        public double MaxPayload { get; private set; }

        private static int _containerCounter = 1;

        protected Container(double height, double tareWeight, double depth, double maxPayload, string containerType)
        {
            Height = height;
            TareWeight = tareWeight;
            Depth = depth;
            MaxPayload = maxPayload;
            SerialNumber = GenerateSerialNumber(containerType);
        }

        private string GenerateSerialNumber(string containerType)
        {
            return $"KON-{containerType}-{_containerCounter++}";
        }

        public virtual void LoadCargo(double mass)
        {
            if (mass > MaxPayload)
            {
                throw new OverfillException($"Proba zaladowania {mass}kg do kontenera {SerialNumber} z maksymalna ladownoscia {MaxPayload}kg");
            }
            CargoMass = mass;
        }

        public virtual void EmptyCargo()
        {
            CargoMass = 0;
        }

        public double GetTotalWeight()
        {
            return CargoMass + TareWeight;
        }

        public abstract void PrintInfo();
    }
}