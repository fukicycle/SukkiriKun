using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SukkiriKun
{
    public class ShortCutItemGroup
    {
        public string Header { get; set; }
        public List<ShortCutItemControl> Items { get; set; }
        public ItemsControl ItemsControl { get; set; }
    }
}
