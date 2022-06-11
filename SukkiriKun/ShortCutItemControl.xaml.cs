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

namespace SukkiriKun
{
    /// <summary>
    /// Interaction logic for ShortCutItemControl.xaml
    /// </summary>
    public partial class ShortCutItemControl : UserControl
    {
        public ShortCutItem _shortCutItem = null;
        public ShortCutItemControl(ShortCutItem shortCutItem)
        {
            _shortCutItem = shortCutItem;
            InitializeComponent();
            DataContext = shortCutItem;
        }
    }
}
