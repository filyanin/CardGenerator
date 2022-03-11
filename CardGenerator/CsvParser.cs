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

        public static string[] getLinesFromCSV(string path, string EncodingName)
        {

            var lines = System.IO.File.ReadAllLines(path, SetEncoding(EncodingName));
            return lines;

        }

    }
}
