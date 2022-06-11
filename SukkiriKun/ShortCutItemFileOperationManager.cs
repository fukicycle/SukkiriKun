using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SukkiriKun.Setting;

namespace SukkiriKun
{
    public class ShortCutItemFileOperationManager : FileOperationManager
    {
        public string ErrorMsg = "";
        public bool Exists()
        {
            return File.Exists(REPOSITORY_FILE_NAME);
        }
        public bool Create()
        {
            if (Exists()) return true;
            try
            {
                if (!DirectoryExists()) Directory.CreateDirectory(new FileInfo(REPOSITORY_FILE_NAME).DirectoryName);
                using (StreamWriter sw = new StreamWriter(REPOSITORY_FILE_NAME))
                {
                    sw.Write("[]");
                }
            }
            catch (Exception ex)
            {
                ErrorMsg = ex.Message;
                return false;
            }
            return true;
        }

        public bool Delete()
        {
            if (Exists()) return true;
            try
            {
                File.Delete(REPOSITORY_FILE_NAME);
            }
            catch (Exception ex)
            {
                ErrorMsg = ex.Message;
                return false;
            }
            return true;
        }



        public bool ReCreate()
        {
            if (Exists()) return true;
            try
            {
                if (Delete()) Create();
                else throw new Exception("Can not delete repository file.");
            }
            catch (Exception ex)
            {
                ErrorMsg = ex.Message;
                return false;
            }
            return true;
        }

        private bool DirectoryExists()
        {
            return Directory.Exists(new FileInfo(REPOSITORY_FILE_NAME).DirectoryName);
        }
    }
}
