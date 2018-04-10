using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfoSearch
{
    class ReadMatrixCoord
    {
        private List<int> values = new List<int>();
        private List<int> iIndex = new List<int>();
        private List<int> jIndex = new List<int>();
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

        public List<String> getRootLinks()
        {
            return rootLinks;
        }

        private void createMatrix()
        {
            int size = matrixLines.Count();
            String currentLine;
            int index;

            for (int i = 0; i < size; i++)
            {
                if (i == 0)
                {
                    currentLine = matrixLines.ElementAt(i);
                    while (currentLine.Length != 0)
                    {
                        index = currentLine.IndexOf("-");
                        this.values.Add(Int32.Parse(currentLine.Substring(0, index)));
                        currentLine = currentLine.Substring(index + 1, currentLine.Length - index + 1);
                    }
                }
                else
                {
                    if (i == 1)
                    {
                        currentLine = matrixLines.ElementAt(i);
                        while (currentLine.Length != 0)
                        {
                            index = currentLine.IndexOf('-');
                            this.iIndex.Add(Int32.Parse(currentLine.Substring(0, index)));
                            currentLine = currentLine.Substring(index + 1, currentLine.Length - index + 1);
                        }
                    }
                    else
                    {
                        if (i == 2)
                        {
                            currentLine = matrixLines.ElementAt(i);
                            while (currentLine.Length != 0)
                            {
                                index = currentLine.IndexOf('-');
                                this.jIndex.Add(Int32.Parse(currentLine.Substring(0, index)));
                                currentLine = currentLine.Substring(index + 1, currentLine.Length - index + 1);
                            }
                        }
                    }
                }
            }
        }

        public List<int> getValues()
        {
            return values;
        }

        public List<int> getiIndex()
        {
            return iIndex;
        }

        public List<int> getjIndex()
        {
            return jIndex;
        }
    }
}
