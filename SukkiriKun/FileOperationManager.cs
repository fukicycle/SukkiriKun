using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SukkiriKun
{
    public interface FileOperationManager
    {
        bool Exists();
        bool Create();
        bool Delete();
        bool ReCreate();
    }
}
