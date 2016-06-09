using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KSPAutoPilot
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void buttonToOrbit_Click(object sender, RoutedEventArgs e)
        {
            IKspEngine engine = new KspEngine();
            if (!engine.IsActive)
            {
                var orbit = 85;
                int.TryParse(OrbitHeight.Text, out orbit);

                engine.TakeOff(orbit, UpdateInformation);
            } else
            {
                MessageBox.Show("Autopilot is running");
            }
        }

        private void UpdateInformation(string message)
        {
            throw new NotImplementedException();
        }
    }
}
