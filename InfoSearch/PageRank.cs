using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfoSearch {
    class PageRank {
        private static readonly double DIFFERENCE = 1.0e-4, ATTENUATION_COEF = 0.85;
        private int[,] matrix;
        private int[] sumLinks;
        private int length;
        //decimal df = new decimal("#.###");

        public double[] getPageRank(int[,] matrix) {
            length = matrix.GetLength(1);
            this.matrix = matrix;
            double[] pageRankOld = new double[length];
            double[] pageRank = new double[length];
            bool stop;
            //this.df.setRoundingMode(RoundingMode.HALF_UP);
            double sum;

            getSumLinks();

            for(int i = 0; i < length; i++) {
                pageRankOld[i] = 1;
            }

            do {
                for(int j = 0; j < length; j++) {
                    sum = 0;
                    pageRank[j] = 1 - ATTENUATION_COEF;
                    for(int i = 0; i < length; i++) {
                        if(this.matrix[i,j] > 0 && sumLinks[i] != 0) {
                            sum += pageRankOld[i] / sumLinks[i];
                        }
                        else {
                            sum += 0;
                        }
                    }
                    pageRank[j] +=  ATTENUATION_COEF * sum;
                }

                stop = checkStop(pageRankOld, pageRank);
                pageRankOld = pageRank;
            } while(!stop);

            return pageRank;
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
                    sumLinks[i] += matrix[i,j];
                }
            }
        }
    }
}
