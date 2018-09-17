using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Clock
{
    public partial class MainWindow : Window
    {
        private static readonly IDictionary<ArrowKind, Arrow> arrowsByKind = new Dictionary<ArrowKind, Arrow>();
        private Point Center => new Point(clockCircumference.Width / 2, clockCircumference.Height / 2);
        private double Radius => Center.X;

        public MainWindow()
        {
            InitializeComponent();
            InitializeTimeArrows();
            InitializeTimer();

        }

        private void InitializeTimer()
        {
            var timer = new Timer { Interval = 100 };
            timer.Elapsed += (sender, args) =>
            {
                var timeNow = DateTime.Now;
               UpdateTime(ArrowKind.Second, timeNow.Second);
               UpdateTime(ArrowKind.Minute, timeNow.Minute);
               UpdateTime(ArrowKind.Hour, timeNow.Hour);
            };
            timer.Start();
        }

        private void UpdateTime(ArrowKind arrowType, int timeUnit)
        {
            Arrow arrow = arrowsByKind[arrowType];
            Line timeArrowLine = arrow.Line;
            arrow.Line.Dispatcher.Invoke(() =>
            {
                // calculated degrees in radians; 0,5*PI is an offset so as to start straitght above the center (0,0)
                double rotation = (arrow.DegreePerTimeUnit * timeUnit * Math.PI / 180) - (0.5 * Math.PI);
                timeArrowLine.Y2 = Radius * Math.Sin(rotation);
                timeArrowLine.X2 = Radius * Math.Cos(rotation);
            });
        }

        private void InitializeTimeArrows()
        {
            DisplayTimeArrow(new Arrow(ArrowKind.Second, degreePerTimeUnit: 6, thickness: 1, color: Brushes.Red));
            DisplayTimeArrow(new Arrow(ArrowKind.Minute, degreePerTimeUnit: 6, thickness: 1));
            DisplayTimeArrow(new Arrow(ArrowKind.Hour, degreePerTimeUnit: 30, thickness: 3));
        }

        private void DisplayTimeArrow(Arrow arrow)
        {
            var line = new Line
            {
                Stroke = arrow.Color,
                StrokeThickness = arrow.Thickness,
                X2 = 0,
                Y2 = Center.X,
            };

            arrow.Line = line;
            arrowsByKind[arrow.Kind] = arrow;
            clock.Children.Add(line);
        }
    }

}
