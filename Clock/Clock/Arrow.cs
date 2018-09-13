using System;
using System.Windows.Shapes;

namespace Clock
{
    /// <summary>
    /// Line can only be set once
    /// </summary>
    public class Arrow
    {
        private Line line;

        public ArrowKind Kind { get; }
        public short DegreePerTimeUnit { get; }
        public double Thickness { get; }
        public Line Line
        {
            get => line;
            set
            {
                if (line is null)
                {
                    line = value;
                }
                else
                {
                    throw new ArgumentException("Line is already set");
                }
            }
        }

        public Arrow(ArrowKind kind, short degreePerTimeUnit, double thickness)
        {
            Kind = kind;
            DegreePerTimeUnit = degreePerTimeUnit;
            Thickness = thickness;
        }
    }
}
