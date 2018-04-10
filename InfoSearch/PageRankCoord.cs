using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfoSearch {
    class PageRankCoord {
        
        private static readonly double DIFFERENCE = 1.0e-4, ATTENUATION_COEF = 0.85;
        private List<int> values;
        List<int> iIndex, jIndex;
        private int[] sumLinks;
        private int length;
        //DecimalFormat df = new DecimalFormat("#.###");

        public double[] getPageRankCoord(List<int> values, List<int> iIndex, List<int> jIndex, int length) {
            this.length = length;
            this.values = values;
            this.iIndex = iIndex;
            this.jIndex = jIndex;
            double[] pageRankOld = new double[this.length];
            double[] pageRank = new double[this.length];
            bool stop;
            //this.df.setRoundingMode(RoundingMode.HALF_UP);
            double sum;

            getSumLinks();

            for(int i = 0; i < this.length; i++) {
                pageRankOld[i] = 1;
            }

            do {
                for(int j = 0; j < this.length; j++) {
                    sum = 0;
                    pageRank[j] = 1 - ATTENUATION_COEF;
                    for(int i = 0; i < this.length; i++) {
                        if(checkIndex(i, j) && this.sumLinks[i] != 0) {
                            sum += pageRankOld[i] / this.sumLinks[i];
                        }
                        else {
                            sum += 0;
                        }
                    }
                    pageRank[j] += ATTENUATION_COEF * sum;
                }

                stop = checkStop(pageRankOld, pageRank);
                pageRankOld = pageRank;
            } while(!stop);

            return pageRank;
        }

        private bool checkIndex(int indexI, int indexJ) {
            bool result = false;
            for(int i = 0; i < iIndex.Count(); i++) {
                if(indexI == iIndex[i]) {
                    if(jIndex[i] == indexJ) {
                        result = true;
                        break;
                    }
                }
            }

            return result;
        }

        private bool checkStop(double[] pageRankOld, double[] pageRank) {
            bool result = true;
            double range;
            for(int i = 0; i < length; i++) {
                range = pageRankOld[i] - pageRank[i];
                if(range < 0) {
                    range = -range;
                }
                if((range) > DIFFERENCE) {
                    result = false;
                    break;
                }
            }

            return result;
        }

        private void getSumLinks() {
            sumLinks = new int[length];

            for(int i = 0; i < length; i++) {
                for(int j = 0; j < length; j++) {
                    if(checkIndex(i, j)) {
                        sumLinks[i]++;
                    }
                }
            }
        }
    }
}
