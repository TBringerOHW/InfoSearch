using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfoSearch
{
    public class WriteMatrixCoord
    {
        public void writeMatrixCoord(List<int> values, List<int> iIndex, List<int> jIndex, List<String> rootLinks, String fileName)
        {
            String info = "";

            for (int i = 0; i < values.Count(); i++)
            {
                info = String.Concat(info, values.ElementAt(i) + "-");
            }

            info = String.Concat(info, "\r\n");

            for (int i = 0; i < iIndex.Count(); i++)
            {
                info = String.Concat(info, iIndex.ElementAt(i) + "-");
            }

            info = String.Concat(info, "\r\n");

            for (int i = 0; i < jIndex.Count(); i++)
            {
                info = String.Concat(info, jIndex.ElementAt(i) + "-");
            }

            info = String.Concat(info, "\r\n");

            for (int i = 0; i < rootLinks.Count(); i++)
            {
                info = String.Concat(info, "\r\n");
                info = String.Concat(info, i + ") " + rootLinks.ElementAt(i));
            }

            try
            {
                StreamWriter writer = new StreamWriter(fileName);
                writer.Write(info);
                writer.Flush();
                writer.Close();
            }
            catch (IOException e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
