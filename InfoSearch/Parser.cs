using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace InfoSearch {
    class Parser {
        private List<String> rootLinks = new List<String>();
        private int[,] matrix;
        private int[,] matrixRunnable;


        public List<String> getRootLinks() {
            return rootLinks;
        }

        public int[,] getMatrix(String rootLink, int limit) {
            matrix = new int[limit, limit];
            matrixRunnable = new int[limit, limit];

            Console.Write("Creating root links...");
            createRootLinks(rootLink, limit);
            Console.WriteLine("Done!");
            Console.WriteLine("Creating matrix...");
            DateTime startTime = DateTime.Now;
            createMatrix();
            DateTime stopTime = DateTime.Now;
            Console.WriteLine("Done!");
            PrintMatrix();
            Console.WriteLine("Matrix time = {0}sec {1}ms",(stopTime - startTime).Seconds.ToString(), (stopTime - startTime).Milliseconds.ToString());
            // createMatrixRunnable();


            return matrix;
        }

        private void createRootLinks(String rootLink, int limit) {
            List<String> links;
            int i = 0;
            bool isUnique = true;
            rootLinks = parseLink(rootLink, limit);
            while(rootLinks.Count() < limit) {

                links = parseLink(rootLinks.ElementAt(i), 0);
                i++;

                for(int j = 0; j < links.Count() && rootLinks.Count() < limit; j++) {

                    for(int k = 0; k < rootLinks.Count() && rootLinks.Count() < limit; k++) {

                        if(links.ElementAt(j).Equals(rootLinks.ElementAt(k))) {
                            isUnique = false;
                        }

                    }

                    if(isUnique) {
                        rootLinks.Add(links.ElementAt(j));
                    }

                    isUnique = true;

                }
            }

            
        }

        public List<String> parseLink(String rootLink, int limit) {
            List<String> links = new List<String>();           

            
            List<string> urlsGlobal = new List<string>();
            try {
                DateTime startTime = DateTime.Now;
                var getHtmlWeb = new HtmlWeb();
                var doc = getHtmlWeb.Load(rootLink);
                var aTags = doc.DocumentNode.SelectNodes("//div");

                if(aTags != null) {
                    var urls = aTags.Descendants("a")
                                    .Select(node => node.GetAttributeValue("href", ""))
                                    .ToList();
                    urlsGlobal = urls;
                    
                    foreach(string url in urls) {
                        //DateTime sTime = DateTime.Now;
                        if(limit != 0) {//If processing rootlink

                            if(limit == links.Count()) {
                                break;
                            }
                            else {

                                if( (links.Count() < limit) && (url.Length > 4) && (url.Substring(0, 4).Equals("http") ) ) {

                                    if(links.Count() == 0) {                                        
                                        links.Add(url);
                                    }
                                    else {
                                        if(!links.Contains(url)) {
                                            links.Add(url);
                                        }
                                        //if(!checkDuplication(links, url)) {
                                        //    links.Add(url);
                                        //}
                                    }

                                }

                            }
                        }
                        else {
                           
                            if( (url.Length > 4)  ) {

                                if(links.Count() == 0) {
                                    links.Add(url);
                                }
                                else {
                                    
                                    if(!links.Contains(url)) {
                                        links.Add(url);
                                    }
                                    
                                    //if(!checkDuplication(links, url)) {
                                    //    links.Add(url);
                                    //}
                                }
                            }

                        }
                        //Console.WriteLine("Contains statement time: {0}s {1}ms", (DateTime.Now - sTime).Seconds.ToString(), (DateTime.Now - sTime).Milliseconds.ToString());
                    }
                    //Console.WriteLine("Parse time {2} links: {0}s {1}ms", (DateTime.Now - startTime).Seconds.ToString(), (DateTime.Now - startTime).Milliseconds.ToString(), urls.Count);
                }
            }
            catch(IOException e) {
                Console.WriteLine(e);
            }
            

            return links;
        }

        public bool checkDuplication(List<String> links, String link) {
            for(int i = 0; i < links.Count(); i++) {
                if(link.Equals(links.ElementAt(i))) {
                    return true;
                }
            }
            return false;
        }

        private void createMatrix() {

            DateTime startTime = DateTime.Now;

            List<String> links;
            for(int i = 0; i < rootLinks.Count; i++) {

                Console.WriteLine("Parsing link #{0} = \"{1}\"", i+1, rootLinks[i]);
                links = parseLink(rootLinks[i], 0);
                for(int j = 0; j < rootLinks.Count; j++) {

                    matrix[i, j] = 0;
                    for(int k = 0; k < links.Count; k++) {

                        if(rootLinks[j].Equals(links[k])) {
                            matrix[i, j] = 1;
                            break;
                        }

                    }

                }
            }
        }

        public void PrintMatrix() {

            int i = 0;
            Console.WriteLine("====  ====   ====");
            foreach(int item in matrix) {
                
                Console.Write(item + " ");
                i++;
                if (i == Math.Sqrt(matrix.Length) ) {
                    i = 0;
                    Console.WriteLine();
                }
            }
            Console.WriteLine("====  ====   ====");
        }

       /* private void createMatrixParallel() {
            DateTime startTime = DateTime.Now;

            List<String> links;
            //ExecutorService executor = Executors.newFixedThreadPool(5);

            List<ThreadStart> threadDelegates = new List<ThreadStart>();
            //List<Future<ArrayList<Integer>>> future;

            List<IterativeRun> tasks = new ArrayList<>();

            for(int i = 0; i < rootLinks.Count(); i++) {
                tasks.add(new IterativeRun(rootLinks, i));
            }

            try {
                future = executor.invokeAll(tasks);
                for(int i = 0; i < rootLinks.size(); i++) {
                    for(int k = 0; k < rootLinks.size(); k++) {
                        matrixRunnable[i][k] = future.get(i).get().get(k);
                        this.valuesRunnable.add(1);
                        this.iIndexRunnable.add(i);
                        this.jIndexRunnable.add(k);
                    }
                }
            }
            catch(InterruptedException e) {
                e.printStackTrace();
            }
            catch(ExecutionException e) {
                e.printStackTrace();
            }
            finally {
                executor.shutdown();
            }

            DateTime stopTime = DateTime.Now;

            System.out.println("Runnable: " + ((stopTime - startTime) / 1000) + "c");
        }*/

    }
}
