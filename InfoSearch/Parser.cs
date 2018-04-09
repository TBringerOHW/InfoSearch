using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfoSearch {
    class Parser
    {
        private List<String> rootLinks = new List<String>();
        private int[,] matrix;
        private int[,] matrixRunnable;

        public List<String> getRootLinks()
        {
            return rootLinks;
        }

        public int[,] getMatrix(String rootLink, int limit)
        {
            this.matrix = new int[limit, limit];
            this.matrixRunnable = new int[limit, limit];

            createRootLinks(rootLink, limit);
            createMatrix();
            // createMatrixRunnable();


            return this.matrix;
        }

        private void createRootLinks(String rootLink, int limit)
        {
            List<String> links;
            int i = 0;
            bool isUnique = true;
            this.rootLinks = parseLink(rootLink, limit);
            while (rootLinks.Count() < limit)
            {
                links = parseLink(this.rootLinks.ElementAt(i), 0);
                i++;
                for (int j = 0; j < links.Count() && this.rootLinks.Count() < limit; j++)
                {
                    for (int k = 0; k < this.rootLinks.Count() && this.rootLinks.Count() < limit; k++)
                    {
                        if (links.ElementAt(j).Equals(this.rootLinks.ElementAt(k)))
                        {
                            isUnique = false;
                        }
                    }
                    if (isUnique)
                    {
                        this.rootLinks.Add(links.ElementAt(j));
                    }
                    isUnique = true;
                }
            }
        }

        public List<String> parseLink(String rootLink, int limit)
        {
            List<String> links = new List<String>();

            try
            {
                var getHtmlWeb = new HtmlWeb();
                var doc = getHtmlWeb.Load(rootLink);
                var aTags = doc.DocumentNode.SelectNodes("//div");

                var urls = aTags.Descendants("a")
                                   .Select(node => node.GetAttributeValue("href", ""))
                                   .ToList();

                foreach (string url in urls)
                {
                    if (limit != 0)
                    {
                        if (limit == links.Count())
                        {
                            break;
                        }
                        else
                        {
                            if (links.Count() < limit && url.Length > 4 && url.Substring(0, 4).Equals("http"))
                            {
                                if (links.Count() == 0)
                                {
                                    links.Add(url);
                                }
                                else
                                {
                                    if (!checkDuplication(links, url))
                                    {
                                        links.Add(url);
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (url.Length > 4 && url.Substring(0, 4).Equals("http"))
                        {
                            if (links.Count() == 0)
                            {
                                links.Add(url);
                            }
                            else
                            {
                                if (!checkDuplication(links, url))
                                {
                                    links.Add(url);
                                }
                            }
                        }
                    }
                }
            }
            catch (IOException) { }
            return links;
        }

        public bool checkDuplication(List<String> links, String link)
        {
            for (int i = 0; i < links.Count(); i++)
            {
                if (link.Equals(links.ElementAt(i)))
                {
                    return true;
                }
            }
            return false;
        }

        private void createMatrix()
        {
            DateTime startTime = DateTime.Now;

            List<String> links;
            for (int i = 0; i < rootLinks.Count(); i++)
            {
                links = this.parseLink(rootLinks.ElementAt(i), 0);
                for (int j = 0; j < rootLinks.Count(); j++)
                {
                    matrix[i, j] = 0;
                    for (int k = 0; k < links.Count(); k++)
                    {
                        if (rootLinks.ElementAt(j).Equals(links.ElementAt(k)))
                        {
                            matrix[i, j] = 1;
                            break;
                        }
                    }
                }
            }

            DateTime stopTime = DateTime.Now;

            Console.WriteLine("Basic: " + (stopTime - startTime).Seconds.ToString() + " seconds");
        }

    }
}
