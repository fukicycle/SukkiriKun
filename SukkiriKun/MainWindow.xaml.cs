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

        private void FileDrop(object sender, DragEventArgs e)
        {
            var list = (sender as ListBox).ItemsSource as List<ShortCutItemControl>;
            List<string> exceptFiles = new List<string>();
            foreach (string fileName in e.Data.GetData(DataFormats.FileDrop) as string[])
            {
                if (fileName.Contains(".lnk"))
                {
                    exceptFiles.Add(fileName);
                    continue;
                }
                shortItemCutManager.AddFile(fileName, list);
                (sender as ListBox).Items.Refresh();
            }
            if (exceptFiles.Count > 0)
                MessageBox.Show($"Can not add following file.\r\nNote:This system is not accepted .lnk.\r\n{string.Join("\r\n", exceptFiles)}");
        }

        private void CreateGroupButtonOnClick(object sender, RoutedEventArgs e)
        {
            shortItemCutManager.AddGroup();
        }

        private void ListBoxLoaded(object sender, RoutedEventArgs e)
        {
            var listBox = sender as ListBox;
            (listBox.Tag as ShortCutItemGroup).ListBox = listBox;
        }
    }
}
