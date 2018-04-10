using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace InfoSearch {
    class ParallelWorker {
        private List<String> rootLinks;
        List<int> result = new List<int>();
        int[,] matrix;
        private int thAmount, thNum;
        Thread th;
        Parser parent;

        public ParallelWorker(List<String> links, int i, int thAmount, Parser parent) {
            this.rootLinks = links;
            this.thAmount = i;
            this.thNum = thAmount;
            this.parent = parent;
        }

        public void DoWork() {
            ThreadStart threadDelegate = new ThreadStart(ThreadWorkload);
            th = new Thread(threadDelegate);
            th.Name = DateTime.UtcNow.Millisecond.ToString();
            th.Start();
        }
        private void ThreadWorkload() {

            Parser parser = new Parser();

            int current;
            matrix = new int[rootLinks.Count, rootLinks.Count];
            for(int i = 0; i < rootLinks.Count; i++) {
                List<String> links = parser.parseLink(rootLinks[i], 0);
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
            parent.FillMatrix(matrix, thNum, thAmount);
        }
    }
}
