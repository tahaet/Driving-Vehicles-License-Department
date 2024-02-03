using DVLD_Buisness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace MySolution.GlobalClasses
{
    public class clsGlobal
    {
        public static clsUser CurrentUser;

        public static bool RememberUserNameAndPassword(string UserName,string Password)
        {
            try
            {
                string currentDirectory = Directory.GetCurrentDirectory();
                string FilePath = string.Format("{0}{1}", currentDirectory, "//Data.txt");
                if (UserName == "" && File.Exists(FilePath))
                {
                    File.Delete(FilePath);
                    return true;
                }
                string DataToSave = string.Format("{0}#//#{1}", UserName, Password);
                using(StreamWriter Writer = new StreamWriter(FilePath))
                {
                    Writer.WriteLine(DataToSave);
                    return true;
                }
            }
            catch (Exception ex) 
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
                return false;
            }
        }
        public static bool GetStoredCredintial(ref string UserName,ref string Password)
        {
            try
            {
                string currentDirectory = Directory.GetCurrentDirectory();
                string FilePath = string.Format("{0}{1}", currentDirectory, "//Data.txt");
                if (File.Exists(FilePath))
                {
                    using (StreamReader Reader = new StreamReader(FilePath))
                    {
                        string Line = "";
                        while ((Line = Reader.ReadLine()) != null)
                        {
                            Console.WriteLine(Line);
                            string[] result = Line.Split(new string[] { "#//#" }, StringSplitOptions.None);
                            UserName = result[0];
                            Password = result[1];
                        }
                        return true;
                    }

                }
                else
                    return false;
            }
            catch(Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
                return false;
            }

        }
    }
}
