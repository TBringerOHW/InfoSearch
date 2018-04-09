using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfoSearch
{
    class WriteMatrix
    {
        public void writeMatrix(int[,] matrix, List<String> rootLinks, String fileName)
        {
            String info = "";

            for (int i = 0; i < matrix.Length; i++)
            {
                for (int j = 0; j < matrix.Length; j++)
                {
                    info = string.Concat(info, matrix[i, j] + "");
                }
                info = string.Concat(info, "\n");
            }

            for (int i = 0; i < rootLinks.Count(); i++)
            {
                info = string.Concat(info, "\n");
                info = string.Concat(info, i + ") " + rootLinks.ElementAt(i));
            }
            
            try
            {
                StreamWriter writer = new StreamWriter("results/simple/" + fileName);
                writer.Write(info);
                writer.Flush();
                writer.Close();
            }
            catch (IOException) {}
        }

    }
}
