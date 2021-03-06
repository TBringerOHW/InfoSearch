﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace InfoSearch {
    class Program {
        static void Main(string[] args) {
            String rootLink = "https://ya.ru";
            int limit = 100;
            int[,] matrix = new int[0,0];
            List<String> rootLinks;
            double[] pageRankMatrix = new double[limit];
            double[] pageRankMatrixCoord = new double[limit];

            // for coordinate method
            List<int> values = new List<int>();
            List<int> iIndex = new List<int>();
            List<int> jIndex = new List<int>();

            int index = rootLink.LastIndexOf("//");
            String fileName = rootLink.Substring(index + 1) + ".txt";
            fileName = fileName.Substring(fileName.IndexOf('/') + 1);
            fileName = fileName.Replace('/', '.');
            if(File.Exists("results/simple/" + fileName)) {
                ReadMatrix readMatrix = new ReadMatrix();
                if (readMatrix.readMatrix("results/simple/" + fileName))
                {
                    Console.WriteLine("Getting matrix from file...");
                    matrix = readMatrix.getMatrix();
                    rootLinks = readMatrix.getRootLinks();
                }

                ReadMatrixCoord readMatrixCoord = new ReadMatrixCoord();
                if (readMatrixCoord.readMatrix("results/coord/" + fileName))
                {
                    Console.WriteLine("Getting matrix from file...");
                    values = readMatrixCoord.getValues();
                    iIndex = readMatrixCoord.getiIndex();
                    jIndex = readMatrixCoord.getjIndex();
                    rootLinks = readMatrixCoord.getRootLinks();
                }
            }
            else {
                Parser parser = new Parser(); 

                Console.WriteLine("Starting work with matrix size = {0}", limit);

                matrix = parser.getMatrix(rootLink, limit);
                rootLinks = parser.getRootLinks();
                values = parser.getValues();
                iIndex = parser.getiIndex();
                jIndex = parser.getjIndex();

                WriteMatrix writeMatrix = new WriteMatrix();
                writeMatrix.writeMatrix(matrix, rootLinks, "results/simple/" + fileName);
                Console.WriteLine("Matrix saved to \"{0}\"", "results/simple/" + fileName);

                WriteMatrixCoord writeMatrixCoord = new WriteMatrixCoord();
                writeMatrixCoord.writeMatrixCoord(values, iIndex, jIndex, rootLinks, "results/coord/" + fileName);
                Console.WriteLine("Matrix saved to \"{0}\"", "results/coord/" + fileName);
            }           


            Console.Write("Calculating Page Rank...");
            PageRank rank = new PageRank();
            PageRankCoord rankCoord = new PageRankCoord();
            Console.WriteLine("Done!\nPage Rank:");
            double[] pageRank = rank.getPageRank(matrix);
            double[] pageRankCoord = rankCoord.getPageRankCoord(values, iIndex, jIndex, limit);
            foreach(double item in pageRank) {
                Console.Write(" {0}", item.ToString("0.000000", System.Globalization.CultureInfo.InvariantCulture));
            }
            Console.WriteLine();
            foreach (double item in pageRankCoord)
            {
                Console.Write(" {0}", item.ToString("0.000000", System.Globalization.CultureInfo.InvariantCulture));
            }


            Console.ReadLine();
        }
    }
}
