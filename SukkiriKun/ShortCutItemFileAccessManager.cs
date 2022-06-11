using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SukkiriKun.Setting;

namespace SukkiriKun
{
    public class ShortCutItemFileAccessManager : FileAccessManager
    {
        public string ErrorMsg = "";
        public bool Load(out string contents)
        {
            try
            {
                using (StreamReader sr = new StreamReader(REPOSITORY_FILE_NAME))
                {
                    contents = sr.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                ErrorMsg = ex.Message;
                contents = null;
                return false;
            }
            return true;
        }

        public bool Write(string contents)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(REPOSITORY_FILE_NAME))
                {
                    sw.WriteLine(contents);
                }
            }
            catch (Exception ex)
            {
                ErrorMsg = ex.Message;
                return false;
            }
            return true;
        }
    }
}
