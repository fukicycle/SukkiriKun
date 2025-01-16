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

        public void GetShortCutItemFromFile(NotifyChanged notifyChanged)
        {
            List<ShortCutItemData> items = null;
            if (shortCutFileAccessManager.Load(out string contents))
                //JsonConvert.DeserializeObject<List<ShortCutItemGroup>>(contents).ForEach(a => Repository.ShortCutItemGroups.Add(a));
                items = JsonConvert.DeserializeObject<List<ShortCutItemData>>(contents);
            else MessageBox.Show(shortCutFileAccessManager.ErrorMsg);
            ConvertToVisualList(items, notifyChanged);
        }

        private void ConvertToVisualList(List<ShortCutItemData> shortCutItemData, NotifyChanged notifyChanged)
        {
            shortCutItemData.ForEach(a =>
            {
                List<ShortCutItemControl> controls = new List<ShortCutItemControl>();
                a.Items.ForEach(b =>
                {
                    controls.Add(new ShortCutItemControl(b, notifyChanged));
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

        public void AddGroup(string groupName, NotifyChanged notifyChanged)
        {
            List<ShortCutItemControl> item = new List<ShortCutItemControl>();
            item.Add(new ShortCutItemControl(new ShortCutItem
            {
                Title = "Drop here!",
                Icon = FileIconManager.ConvertToByteArrayFromBitmapSource(new BitmapImage(new Uri("DragDropIcon.png", UriKind.Relative))),
                OriginalName = "",
                OriginalPath = "",
                WorkingDirectory = ""
            }, notifyChanged));
            Repository.ShortCutItemGroups.Add(new ShortCutItemGroup
            {
                Header = groupName,
                Items = item
            });
            WriteShortCutItemToFile();
        }

        public void AddFile(string fileName, List<ShortCutItemControl> shortCutItemControls, NotifyChanged notifyChanged, bool isUrl = false)
        {
            if (isUrl)
            {
                if(!fileName.StartsWith("http"))
                {
                    fileName = "http://" + fileName;
                }
                shortCutItemControls.Add(new ShortCutItemControl(new ShortCutItem
                {
                    Title = fileName,
                    Icon = null,
                    OriginalName= fileName,
                    OriginalPath= fileName,
                    WorkingDirectory = null
                }, notifyChanged));
            }
            else
            {
                FileInfo fi = new FileInfo(fileName);
                shortCutItemControls.Add(new ShortCutItemControl(new ShortCutItem
                {
                    Title = fi.Name,
                    Icon = FileIconManager.GetIconFromFile(fi.FullName),
                    OriginalName = fileName,
                    OriginalPath = fileName,
                    WorkingDirectory = fi.DirectoryName
                }, notifyChanged));
            }
            WriteShortCutItemToFile();
        }

        public void DeleteFile(ShortCutItemControl shortCutItemControl)
        {
            var shortCutItemGroup = Repository.ShortCutItemGroups.FirstOrDefault(a => a.Items.Contains(shortCutItemControl));
            shortCutItemGroup.Items.Remove(shortCutItemControl);
            shortCutItemGroup.ItemsControl.Items.Refresh();
            WriteShortCutItemToFile();
        }

        public void DeleteGroup(ShortCutItemGroup shortCutItemGroup)
        {
            Repository.ShortCutItemGroups.Remove(shortCutItemGroup);
            WriteShortCutItemToFile();
        }

        public void UpdateFile()
        {
            WriteShortCutItemToFile();
        }
    }
}
