using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SukkiriKun
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, NotifyChanged
    {
        private ShortCutItemFileOperationManager shortCutItemFileOperationManager = new ShortCutItemFileOperationManager();
        private ShortCutItemManager shortItemCutManager = new ShortCutItemManager();
        private ShortCutItem editItem = null;
        public MainWindow()
        {
            InitializeComponent();
            if (shortCutItemFileOperationManager.Create())
            {
                shortItemCutManager.GetShortCutItemFromFile(this);
                mainContentsItemsControl.ItemsSource = Repository.ShortCutItemGroups;
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
                RaiseEvent(e);
            }
            catch (Exception ex)
            {

            }
        }

        private void CloseButtonOnClick(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }


        private void FileDrop(object sender, DragEventArgs e)
        {
            var list = (sender as ItemsControl).ItemsSource as List<ShortCutItemControl>;
            List<string> exceptFiles = new List<string>();
            foreach (string fileName in e.Data.GetData(DataFormats.FileDrop) as string[])
            {
                if (fileName.Contains(".lnk"))
                {
                    exceptFiles.Add(fileName);
                    continue;
                }
                shortItemCutManager.AddFile(fileName, list, this);
                (sender as ItemsControl).Items.Refresh();
            }
            if (exceptFiles.Count > 0)
            {
                InitializeErrorDialog();
                errorMsgTextBlock.Text = $"以下のファイルを追加することはできません。\r\nこのアプリでは「.lnk」ファイルの追加は許可されていません。\r\n{string.Join("\r\n", exceptFiles)}";
            }
        }

        private void CreateGroupButtonOnClick(object sender, RoutedEventArgs e)
        {
            InitializeAddDialog();
        }

        private void ListBoxLoaded(object sender, RoutedEventArgs e)
        {
            var itemsControl = sender as ItemsControl;
            (itemsControl.Tag as ShortCutItemGroup).ItemsControl = itemsControl;
        }

        private void DeleteGroupButtonOnClick(object sender, RoutedEventArgs e)
        {
            var item = (sender as Button).Tag as ShortCutItemGroup;
            shortItemCutManager.DeleteGroup(item);
        }

        private void OkButtonOnClick(object sender, RoutedEventArgs e)
        {
            if (editItem != null)
            {
                if (groupNameTextBox.Text == string.Empty)
                {
                    errorMsgTextBlock.Text = "ショートカット名が空です";
                    return;
                }
                editItem.Title = groupNameTextBox.Text;
                shortItemCutManager.UpdateFile();
                editItem = null;
                Repository.ShortCutItemGroups.Clear();
                shortItemCutManager.GetShortCutItemFromFile(this);
                mainContentsItemsControl.ItemsSource = Repository.ShortCutItemGroups;
            }
            else
            {
                if (groupNameTextBox.Text == string.Empty)
                {
                    errorMsgTextBlock.Text = "グループ名が空です";
                    return;
                }
                shortItemCutManager.AddGroup(groupNameTextBox.Text, this);
            }
            FinalizeDialogPanel();
        }

        private void CancelButtonOnClick(object sender, RoutedEventArgs e)
        {
            FinalizeDialogPanel();
        }

        private void FinalizeDialogPanel()
        {
            groupNameTextBox.Text = "";
            errorMsgTextBlock.Text = "";
            dialogPanel.Visibility = Visibility.Collapsed;
            okErrorButton.Visibility = Visibility.Collapsed;
            SetCursorPos(0, 0);
        }

        [DllImport("User32.dll")]
        private static extern bool SetCursorPos(int X, int Y);

        private void InitializeErrorDialog()
        {
            dialogPanel.Visibility = Visibility.Visible;
            addGroupPanel.Visibility = Visibility.Collapsed;
            okErrorButton.Visibility = Visibility.Visible;
        }

        private void InitializeAddDialog()
        {
            if (editItem != null)
            {
                groupNameTextBox.Text = editItem.Title;
            }
            dialogPanel.Visibility = Visibility.Visible;
            addGroupPanel.Visibility = Visibility.Visible;
            okErrorButton.Visibility = Visibility.Collapsed;
            groupNameTextBox.Focus();
            groupNameTextBox.SelectAll();
        }

        private void OkErrorButtonOnClick(object sender, RoutedEventArgs e)
        {
            FinalizeDialogPanel();
        }

        public void ItemClicked()
        {
        }

        public void ThrowException(string message)
        {
            InitializeErrorDialog();
            errorMsgTextBlock.Text = message;
        }

        public void ItemEdit(ShortCutItem shortCutItem)
        {
            editItem = shortCutItem;
            InitializeAddDialog();
        }
    }
}
