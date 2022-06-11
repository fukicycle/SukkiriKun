using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SukkiriKun
{
    public static class Repository
    {
        public static ObservableCollection<ShortCutItemGroup> ShortCutItemGroups = new ObservableCollection<ShortCutItemGroup>();
    }
}
