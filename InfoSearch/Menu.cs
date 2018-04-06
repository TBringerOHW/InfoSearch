using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfoSearch {
    class Menu {
        public void Init() {

            ConsoleKey key = ConsoleKey.D1;

            while (!key.Equals(ConsoleKey.D0) && !key.Equals(ConsoleKey.Escape)) {
                Console.WriteLine("Choose what you want: \n0. Stop execution.\n1. Parse site.");
                key = Console.ReadKey().Key;
                switch (key) {
                    case ConsoleKey.D1: {

                            InitSiteParse();
                            Console.WriteLine("\nCase #1");
                            break;
                        }
                    case ConsoleKey.D2: {
                            Console.WriteLine("\nCase #2");
                            break;
                        }
                    case ConsoleKey.D3: {
                            Console.WriteLine("\nCase #3");
                            break;
                        }
                    case ConsoleKey.D4: {
                            Console.WriteLine("\nCase #4");
                            break;
                        }
                    case ConsoleKey.D5: {
                            Console.WriteLine("\nCase #5");
                            break;
                        }
                    case ConsoleKey.D6: {
                            Console.WriteLine("\nCase #6");
                            break;
                        }
                    default: {
                            Console.WriteLine("\nWrong input!");
                            break;
                        }
                }

            }

        }

        public void InitSiteParse() {
            Parser parser = new Parser();
            parser.InitParse();
        }

    }
}