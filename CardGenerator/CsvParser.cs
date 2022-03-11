using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGenerator
{
    public static class CsvParser
    {
        public static Encoding SetEncoding(string EncodingName)
        {
            return Encoding.GetEncoding(EncodingName);
        }

        public static List<string> getLinesFromCSV(string path, string EncodingName)
        {

            var lines = System.IO.File.ReadAllLines(path, SetEncoding(EncodingName)).ToList<string>();
            List<string> tmpLines = new List<string>();

            foreach (var line in lines)
            {
                if (line[0] == ';')
                {
                   tmpLines.Add(line);
                }
            }

            foreach (var tmpLine in tmpLines)
            {
                lines.Remove(tmpLine);
            }
            return lines;

        }

    }
}
