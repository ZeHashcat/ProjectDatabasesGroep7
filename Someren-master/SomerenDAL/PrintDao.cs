using SomerenDAL;
using SomerenModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SomerenDAL
{
    public class PrintDao
    {
        //We don't know where to put this, so we put this here.
        public void ErrorLog(Exception ex)
        {
            string message = $"\nDate & Time: {DateTime.Now}\nError Log:\n{ex}";
            string path = Path.Combine(Environment.CurrentDirectory, @"ErrorLog.txt");

            if (!File.Exists(path))
            {
                using (StreamWriter CreateFile = File.CreateText(path));
            }

            using (StreamWriter writer = new StreamWriter(path, true))
            {
                writer.WriteLine(message);
                writer.Close();
            }
        }
    }
}
