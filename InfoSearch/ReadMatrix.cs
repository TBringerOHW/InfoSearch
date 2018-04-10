using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfoSearch
{
    public class ReadMatrix
    {
        private int[,] matrix;
        private List<String> rootLinks = new List<String>();
        private List<String> matrixLines = new List<String>();

        public Boolean readMatrix(String fileName)
        {
            bool isFile;
            try
            {
                isFile = true;
                int index;
                bool endMatrix = false;

                StreamReader reader = new StreamReader(fileName);

                String line = reader.ReadLine();

                while (line != null)
                {
                    if (line.Equals(""))
                    {
                        endMatrix = true;
                        line = reader.ReadLine();
                    }
                    if (!endMatrix)
                    {
                        matrixLines.Add(line);
                    }
                    else
                    {
                        index = line.IndexOf(')');
                        rootLinks.Add(line.Substring(index + 1));
                    }

                    line = reader.ReadLine();
                }
                this.createMatrix();

            }
            catch (FileNotFoundException)
            {
                isFile = false;
                return isFile;
            }
            catch (IOException)
            {
                isFile = false;
                return isFile;
            }
            return isFile;
        }

        public int[,] getMatrix()
        {
            return matrix;
        }

        public List<String> getRootLinks()
        {
            return rootLinks;
        }

        private void createMatrix()
        {
            int size = matrixLines.Count();
            this.matrix = new int[size, size];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    matrix[i, j] = Int32.Parse(matrixLines.ElementAt(i).Substring(j, 1));
                }
            }
        }
    }
}
