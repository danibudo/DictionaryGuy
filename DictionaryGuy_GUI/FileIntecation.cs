using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DictionaryGuy_GUI
{
    class FileInteraction
    {
        public static String GetTimestamp(DateTime value)
        {
            return value.ToString("yyyyMMddHHmmssffff");
        }

        public static void Write(string action, string word, string text)
        {
            String timeStamp = GetTimestamp(DateTime.Now);
            File.WriteAllText(action + "_" + word + "_" + timeStamp + ".txt", text);
        }
    }
}
