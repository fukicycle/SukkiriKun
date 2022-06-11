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
            if (shortCutFileAccessManager.Load(out string contents))
                JsonConvert.DeserializeObject<List<ShortCutItemGroup>>(contents).ForEach(a => Repository.ShortCutItemGroups.Add(a));
            else MessageBox.Show(shortCutFileAccessManager.ErrorMsg);
        }

        public void WriteShortCutItemToFile()
        {
            if (shortCutFileAccessManager.Write(JsonConvert.SerializeObject(Repository.ShortCutItemGroups))) return;
            MessageBox.Show(shortCutFileAccessManager.ErrorMsg);
        }

        public void AddGroup()
        {
            List<ShortCutItemControl> item = new List<ShortCutItemControl>();
            item.Add(new ShortCutItemControl(new ShortCutItem
            {
                Title = "Drop here!",
                Icon = new BitmapImage(new Uri("DragDropIcon.png", UriKind.Relative)),
                OriginalName = "",
                OriginalPath = "",
                WorkingDirectory = ""
            }));
            Repository.ShortCutItemGroups.Add(new ShortCutItemGroup
            {
                Header = "Test",
                Items = item
            });
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
        }
    }
}
