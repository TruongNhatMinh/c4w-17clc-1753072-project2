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
using System.Windows.Shapes;
using Student_Management.BUS;

namespace Student_Management
{
    /// <summary>
    /// Interaction logic for viewScoreboard.xaml
    /// </summary>
    public partial class viewScoreboard : Page
    {
        public viewScoreboard(Components _components, List<int> PaF)
        {
            DataContext = _components;
            InitializeComponent();
            if(PaF != null)
            {
                PP.Content = "Pass Percentage: " + ((double)PaF[1] / (PaF[0] + PaF[1]) * 100).ToString("0.00") + " %";
                FP.Content = "Fail Percentage: " + ((double)PaF[0] / (PaF[0] + PaF[1]) * 100).ToString("0.00") + " %";
                PN.Content = "Pass Number: " + PaF[1].ToString();
                FN.Content = "Fail Number: " + PaF[0].ToString();
            }
        }
    }
}
