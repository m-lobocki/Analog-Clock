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

        }

       

        private void DisplayTimeArrow(Arrow arrow)
        {
            var line = new Line
            {
                Stroke = Brushes.Black,
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
