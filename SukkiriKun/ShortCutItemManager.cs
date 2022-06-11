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
        private ShortCutItemFileAccessManager shortCutItemFileAccessManager = new ShortCutItemFileAccessManager();

        public void GetShortCutItemFromFile()
        {
            if (shortCutItemFileAccessManager.Load(out string contents))
                Repository.ShortCutItemGroups = JsonConvert.DeserializeObject<List<ShortCutItemGroup>>(contents);
            else MessageBox.Show(shortCutItemFileAccessManager.ErrorMsg);
        }

        public void WriteShortCutItemToFile()
        {
            if (shortCutItemFileAccessManager.Write(JsonConvert.SerializeObject(Repository.ShortCutItemGroups))) return;
            MessageBox.Show(shortCutItemFileAccessManager.ErrorMsg);
        }
    }
}
