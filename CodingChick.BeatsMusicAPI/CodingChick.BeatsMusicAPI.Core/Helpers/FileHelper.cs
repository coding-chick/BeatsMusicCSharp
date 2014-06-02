using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CodingChick.BeatsMusicAPI.Core.Helpers
{
    public class FileHelper
    {
        public string GetResourceTextFile(string filename)
        {
            string result = string.Empty;
            //PlayerCode.html
            using (Stream stream = this.GetType().Assembly.
                       GetManifestResourceStream("CodingChick.BeatsMusicAPI.Core.Data.Player." + filename))
            {
                using (StreamReader sr = new StreamReader(stream))
                {
                    result = sr.ReadToEnd();
                }
            }
            return result;
        }
    }
}
