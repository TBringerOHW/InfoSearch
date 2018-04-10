using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfoSearch {
    class ParallelWorker {
        private List<String> rootLinks;
        private int i;

        public ParallelWorker(List<String> rootLinks, int i) {
            this.rootLinks = rootLinks;
            this.i = i;
        }

        public List<int> DoWork() {
            Parser parser = new Parser();
            List<int> result = new List<int>();
            int current;

            List<String> links = parser.parseLink(rootLinks[i], 0);

            for(int j = 0; j < rootLinks.Count(); j++) {
                current = 0;
                for(int k = 0; k < links.Count(); k++) {
                    if(rootLinks[j].Equals(links[k])) {
                        current = 1;
                        break;
                    }
                }
                result[j] = current;
            }
            return result;
        }

    }
}
