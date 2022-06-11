using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SukkiriKun
{
    public static class Setting
    {
        public static readonly string REPOSITORY_FILE_NAME = Path.Combine(Environment.CurrentDirectory, "Repository", "rep.txt");
    }
}
