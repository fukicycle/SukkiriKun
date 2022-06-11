using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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
    }
}
