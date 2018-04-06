using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfoSearch {
    class Parser {

        int[,] matrix = new int[10, 10];
        List<string> links = new List<string>();

        public void InitParse() {
            string site;
            int n = 0;

            Console.WriteLine("\nEnter site name");
            site = Console.ReadLine();
            Console.WriteLine("\nEnter n");
            n = int.Parse(Console.ReadLine());
            var getHtmlWeb = new HtmlWeb();
            var document = getHtmlWeb.Load("http://" + site);
            var aTags = document.DocumentNode.SelectNodes("//div");

            var hrefs = aTags.Descendants("a")
                               .Select(node => node.GetAttributeValue("href", ""))
                               .ToList();

            Console.WriteLine(hrefs.Count);
            Console.WriteLine("RepoTest");

            foreach (string s in hrefs) { 
                if (!s.Contains(site) && !s.Contains("http://")) {
                    links.Add(site + s);
                }
                else {
                    links.Add(s);
                }
                if (links.Count == n) break;
            }

                /*hrefs.RemoveRange(10, hrefs.Count - 10);
            links.AddRange(hrefs);*/

            foreach (string s in links) {
                Console.WriteLine(s);
            }
            Console.WriteLine("\\\\\\" + links.Count);
        }

        public void ParseSite(String site) {
            var getHtmlWeb = new HtmlWeb();
            var document = getHtmlWeb.Load("http://" + site);
            var aTags = document.DocumentNode.SelectNodes("//div");

            var hrefs = aTags.Descendants("a")
                               .Select(node => node.GetAttributeValue("href", ""))
                               .ToList();
        }
    }

    /*            int countOverall = 0, countRedirect = 0, countLocal = 0;

            foreach (string s in hrefs) {

                string counterString = countOverall + " || ";

                if (s.Contains(site)) {
                    Console.WriteLine(counterString + s);
                    countLocal++;
                }
                else {
                    if (!s.Contains("http")) {
                        Console.WriteLine(counterString + "http://" + site + s);
                        countLocal++;
                    }
                    else {
                        Console.WriteLine(counterString + "http://" + site + " -> " + s);
                        countRedirect++;
                    }
                }
                //if (countOverall == 100) break;
                countOverall++;
            }

            Console.WriteLine(
                "Количество обработанных ссылок = " + hrefs.Count +
                "\nВнутренние ссылки = " + countLocal +
                "\nВнешние ссылки = " + countRedirect);*/
}
