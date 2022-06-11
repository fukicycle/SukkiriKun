using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        private void ShortCutItemOnClick(object sender, MouseButtonEventArgs e)
        {
            if (_shortCutItem != null && _shortCutItem.OriginalName != String.Empty)
                Process.Start(_shortCutItem.OriginalName);
        }

        private void DeleteButtonOnClick(object sender, RoutedEventArgs e)
        {
            var shortCutItemGroup = Repository.ShortCutItemGroups.FirstOrDefault(a => a.Items.Contains(this));
            shortCutItemGroup.Items.Remove(this);
            shortCutItemGroup.ListBox.Items.Refresh();
        }
    }
}
