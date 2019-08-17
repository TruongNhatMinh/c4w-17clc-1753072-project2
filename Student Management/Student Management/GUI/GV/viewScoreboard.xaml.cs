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
        public viewScoreboard(Components _components)
        {
            DataContext = _components;
            InitializeComponent();
        }
    }
}
