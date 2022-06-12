using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace SukkiriKun
{
    public class ShortCutItemManager
    {
        private ShortCutItemFileAccessManager shortCutFileAccessManager = new ShortCutItemFileAccessManager();

        public void GetShortCutItemFromFile()
        {
            List<ShortCutItemData> items = null;
            if (shortCutFileAccessManager.Load(out string contents))
                //JsonConvert.DeserializeObject<List<ShortCutItemGroup>>(contents).ForEach(a => Repository.ShortCutItemGroups.Add(a));
                items = JsonConvert.DeserializeObject<List<ShortCutItemData>>(contents);
            else MessageBox.Show(shortCutFileAccessManager.ErrorMsg);
            ConvertToVisualList(items);
        }

        private void ConvertToVisualList(List<ShortCutItemData> shortCutItemData)
        {
            shortCutItemData.ForEach(a =>
            {
                List<ShortCutItemControl> controls = new List<ShortCutItemControl>();
                a.Items.ForEach(b =>
                {
                    controls.Add(new ShortCutItemControl(b));
                });
                Repository.ShortCutItemGroups.Add(new ShortCutItemGroup
                {
                    Header = a.Header,
                    Items = controls
                });
            });
        }

        public void WriteShortCutItemToFile()
        {
            if (shortCutFileAccessManager.Write(JsonConvert.SerializeObject(Repository.ShortCutItemGroups.Select(a => new ShortCutItemData { Header = a.Header, Items = a.Items.Select(b => b._shortCutItem).ToList() })))) return;
            MessageBox.Show(shortCutFileAccessManager.ErrorMsg);
        }

        public void AddGroup()
        {
            List<ShortCutItemControl> item = new List<ShortCutItemControl>();
            item.Add(new ShortCutItemControl(new ShortCutItem
            {
                Title = "Drop here!",
                Icon = FileIconManager.ConvertToByteArrayFromBitmapSource(new BitmapImage(new Uri("DragDropIcon.png", UriKind.Relative))),
                OriginalName = "",
                OriginalPath = "",
                WorkingDirectory = ""
            }));
            Repository.ShortCutItemGroups.Add(new ShortCutItemGroup
            {
                Header = "Test",
                Items = item
            });
            WriteShortCutItemToFile();
        }

        public void AddFile(string fileName, List<ShortCutItemControl> shortCutItemControls)
        {
            FileInfo fi = new FileInfo(fileName);
            shortCutItemControls.Add(new ShortCutItemControl(new ShortCutItem
            {
                Title = fi.Name,
                Icon = FileIconManager.GetIconFromFile(fi.FullName),
                OriginalName = fileName,
                OriginalPath = fileName,
                WorkingDirectory = fi.DirectoryName
            }));
            WriteShortCutItemToFile();
        }

        public void DeleteFile(ShortCutItemControl shortCutItemControl)
        {
            var shortCutItemGroup = Repository.ShortCutItemGroups.FirstOrDefault(a => a.Items.Contains(shortCutItemControl));
            shortCutItemGroup.Items.Remove(shortCutItemControl);
            shortCutItemGroup.ListBox.Items.Refresh();
            WriteShortCutItemToFile();
        }

        public void DeleteGroup()
        {

        }
    }
}
