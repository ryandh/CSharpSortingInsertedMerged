using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortingLab
{
    class Program
    {
        static void Main(string[] args)
        {

            var bigArray = GivemeAbigArray(1000 * 8000);

            Stopwatch st = Stopwatch.StartNew();
            var result = SystemSorting(bigArray);
            var p2 = result.ToList();
            DumpTop5(p2);
            Console.WriteLine("System Sorting" + st.Elapsed.ToString());

            st = Stopwatch.StartNew();
            result = MergeSorting(bigArray);
            p2 = result.ToList();
            DumpTop5(p2);
            Console.WriteLine("Merge Sorting" + st.Elapsed.ToString());


            st = Stopwatch.StartNew();
            result = InsertionSorting(bigArray);
            p2 = result.ToList();
            DumpTop5(p2);
            Console.WriteLine("Insert Sorting" + st.Elapsed.ToString());
            


        }

        private static void DumpTop5(List<double> p2)
        {
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine(p2[i]);
            }

            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine(p2[p2.Count - i - 1]);
            }

        }

        private static double[] MergeSorting(double[] bigArray)
        {
            if (bigArray.Length == 1)
                return bigArray;

            if (bigArray.Length == 2)
            {
                var a = bigArray[0];
                var b = bigArray[1];
                if (a > b)
                {
                    //switch
                    bigArray[0] = b;
                    bigArray[1] = a;
                }
                return bigArray;
            }

            var halflengh = bigArray.Length / 2;
            var left = new double[halflengh];
            var right = new double[bigArray.Length - halflengh];
            for (int i = 0; i < halflengh; i++)
                left[i] = bigArray[i];
            for (int i = halflengh; i < bigArray.Length; i++)
                right[i - halflengh] = bigArray[i];
            return MergeSortingIMpl(left, right);

        }
        private static double[] MergeSortingIMpl(double[] bigLeft, double[] bigRight)
        {
            double[] l = MergeSorting(bigLeft);
            double[] r = MergeSorting(bigRight);

            //l and r are sorted, we need merge
            //1,2,9
            //4,7,10
            double[] result = new double[bigLeft.Length + bigRight.Length];

            //down the left and right, switch the head, and pick up the right value download the road. 
            Boolean headonleft = true;

            int leftI = 0;
            int rightI = 0;
            int j = 0;
            //if on left head, compare each element in left with right, if left < rightI,
            //keep going on left,otherwise, pickuo the right , /
            //and keep goon on right, keep switching till to the end

            double previousleftvalue = l[0];
            double prevousrightvalue = r[0];
            if (l[leftI] <= r[rightI])
                headonleft = true;
            else
                headonleft = false;
            do
            {


                while ((leftI < l.Length && rightI < r.Length && l[leftI] <= prevousrightvalue && headonleft) || (rightI == r.Length && leftI != l.Length))
                {
                    //left is small, or the right is done
                    result[j++] = l[leftI];
                    if (leftI == l.Length - 1)
                    {
                        previousleftvalue = l[leftI];
                    }
                    else
                    {
                        previousleftvalue = l[leftI + 1];
                    }
                    leftI++;
                }
                headonleft = false;
                while ((rightI < r.Length && previousleftvalue > r[rightI] && headonleft == false) || (leftI == l.Length && rightI != r.Length))
                {
                    //right is small or the left is done
                    result[j++] = r[rightI];
                    if (rightI == r.Length - 1)
                    {
                        prevousrightvalue = r[rightI];
                    }
                    else
                    {
                        prevousrightvalue = r[rightI + 1];
                    }
                    rightI++;

                }
                headonleft = true;



            } while ((leftI < l.Length || rightI < r.Length));
            return result;
        }




        private static double[] InsertionSorting(double[] bigArray)
        {
            for (int j = 1; j < bigArray.Length; j++)
            {
                double k = bigArray[j];

                for (int l = j - 1; l >= 0; l--)
                {
                    if (k < bigArray[l])
                    {
                        var t = bigArray[l + 1];
                        bigArray[l + 1] = bigArray[l];
                        bigArray[l] = t;
                    }
                }
            }
            return bigArray;
        }

        private static double[] SystemSorting(double[] bigArray)
        {
            var result = bigArray.OrderBy(p => p).ToArray();
            return result;
        }

        private static double[] GivemeAbigArray(int p)
        {
            double[] ar = new double[p];
            Random r = new Random();
            for (int i = 0; i < p; i++)
            {
                ar[i] = r.NextDouble();
            }

            return ar;
        }
    }
}
