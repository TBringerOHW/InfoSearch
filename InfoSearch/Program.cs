using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfoSearch {
    class Program {
        static void Main(string[] args) {
            String rootLink = "https://elderscrolls.net";
            int limit = 4;

            int[,] matrix = new int[0,0];
            List<String> rootLinks;
            double[] pageRankMatrix = new double[limit];
            double[] pageRankMatrixCoord = new double[limit];

            int index = rootLink.LastIndexOf("//");
            String fileName = rootLink.Substring(index + 1) + ".txt";
            fileName = fileName.Substring(fileName.IndexOf('/') + 1);
            fileName = fileName.Replace('/', '.');

            Parser parser = new Parser();

            Console.WriteLine("Starting work with matrix size = {0}", limit);

            matrix = parser.getMatrix(rootLink, limit);
            rootLinks = parser.getRootLinks();

            WriteMatrix writeMatrix = new WriteMatrix();
            writeMatrix.writeMatrix(matrix, rootLinks, "results/simple/" + fileName);

            Console.WriteLine("Matrix writed to \"{0}\"", "results/simple/" + fileName);

            Console.Write("Calculating Page Rank...");
            PageRank rank = new PageRank();
            Console.WriteLine("Done!\nPage Rank:");
            double[] pageRank = rank.getPageRank(matrix);
            foreach(double item in pageRank) {
                Console.Write(" {0}", item.ToString("0.000000", System.Globalization.CultureInfo.InvariantCulture));
            }


            Console.ReadLine();
        }
    }
}
