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
    /// Interaction logic for viewSchedule.xaml
    /// </summary>
    public partial class viewSchedule : Page
    {
        public viewSchedule(Components _components)
        {
            DataContext = _components;
            InitializeComponent();
        }
    }
}
