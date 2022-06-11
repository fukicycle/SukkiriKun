using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SukkiriKun
{
    public interface FileAccessManager
    {
        bool Load(out string contents);
        bool Write(string contents);
    }
}
