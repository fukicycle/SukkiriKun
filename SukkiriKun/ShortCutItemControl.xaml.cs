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
        private ShortCutItemManager shortCutItemManager = new ShortCutItemManager();
        private NotifyChanged _notifyChanged;
        public ShortCutItemControl(ShortCutItem shortCutItem, NotifyChanged notifyChanged)
        {
            _shortCutItem = shortCutItem;
            InitializeComponent();
            DataContext = shortCutItem;
            _notifyChanged = notifyChanged;
        }

        private void ShortCutItemOnClick(object sender, MouseButtonEventArgs e)
        {
            if (_shortCutItem != null && _shortCutItem.OriginalName != string.Empty)
            {
                try
                {
                    _notifyChanged.ItemClicked();
                    ProcessStartInfo info = new ProcessStartInfo();
                    info.FileName = _shortCutItem.OriginalName;
                    info.UseShellExecute = true;
                    info.WorkingDirectory = _shortCutItem.WorkingDirectory;
                    Process.Start(info);
                }
                catch (Exception ex)
                {
                    _notifyChanged.ThrowException($"{ex.Message}\nFile:{_shortCutItem.OriginalPath}");
                }
            }
        }

        private void DeleteButtonOnClick(object sender, RoutedEventArgs e)
        {
            if (_shortCutItem.OriginalName == string.Empty) return;
            shortCutItemManager.DeleteFile(this);
        }

        private void EditButtonOnClick(object sender, RoutedEventArgs e)
        {
            if (_shortCutItem.OriginalName == string.Empty) return;
            _notifyChanged.ItemEdit(_shortCutItem);
        }
    }
}
