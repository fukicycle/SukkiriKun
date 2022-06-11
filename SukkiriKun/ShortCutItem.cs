using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace SukkiriKun
{
    public class ShortCutItem
    {
        public string Title { get; set; }
        public BitmapSource Icon { get; set; }
        public string OriginalPath { get; set; }
        public string OriginalName { get; set; }
        public string WorkingDirectory { get; set; }
    }
}
