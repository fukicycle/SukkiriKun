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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ShortCutItemFileOperationManager shortCutItemFileOperationManager = new ShortCutItemFileOperationManager();
        private ShortCutItemManager shortItemCutManager = new ShortCutItemManager();
        public MainWindow()
        {
            InitializeComponent();
            if (shortCutItemFileOperationManager.Create())
            {
                shortItemCutManager.GetShortCutItemFromFile();
                mainContentsListBox.ItemsSource = Repository.ShortCutItemGroups;
            }
            else
            {
                MessageBox.Show(shortCutItemFileOperationManager.ErrorMsg);
            }
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                DragMove();
            }
            catch (Exception ex)
            {

            }
        }

        private void CloseButtonOnClick(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }

        private void switchLabel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                if (mainContensPanel.Visibility == Visibility.Visible)
                {
                    mainContensPanel.Visibility = Visibility.Collapsed;
                    switchLabel.Content = "表示";
                }
                else
                {
                    mainContensPanel.Visibility = Visibility.Visible;
                    switchLabel.Content = "非表示";
                }
            }
        }
    }
}
