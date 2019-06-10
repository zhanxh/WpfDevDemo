using ConsoleAppDev.CacheMgrTest;
using ConsoleAppDev.sort;
using ConsoleAppDev.typetest;
using Google.Protobuf;
using Google.Protobuf.Examples.AddressBook;
using LarkspurStudio;
using Sorting.CSharpStringSort;
using Sorting.SedgewickSort;
using SortTests;
using SortTests.Sorting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppDev
{
    class Program
    {
        static void Main(string[] args)
        {
            //TpTest tt = new TpTest();
            //tt.Test1();

            //CMTest ct = new CMTest();
            //ct.Test1();
            SortTest();
        }

        public static void SortTest()
        {
            SortStringTest sst = new SortStringTest();
            sst.Init();
            sst.Sort();
            Console.ReadKey();
        }

        public static void GpbTest()
        {
            Person p1 = new Person();
            Person p = new Person();
            p.Email = "zdf123d@xx.com";
            p.Id = 99;
            p.Name = "zxh";
            byte[] mem = new byte[p.CalculateSize()];
            p.WriteTo(new CodedOutputStream(mem));
            Console.WriteLine(p.ToString());
            using (FileStream fs = new FileStream("test.txt", FileMode.OpenOrCreate))
            {
                //StreamWriter sw = new StreamWriter(fs);
                CodedOutputStream cos = new CodedOutputStream(fs);
                p.WriteTo(cos);
                //p1.MergeDelimitedFrom(fs);
                fs.Flush();
            };

            p1.MergeFrom(mem);
        }
        static void SortTest1(string[] args)
        {
            CreateRandomFile(@"..\..\dic.txt");
            string[] aArray = File.ReadAllLines(@"Rndm-dic.txt", Encoding.Default);
            string[] bArray = File.ReadAllLines(@"Rndm-dic.txt", Encoding.Default);
            List<string> aList = new List<string>(aArray);
            List<string> sList = new List<string>(aArray);
            List<string> sList2 = new List<string>(aArray);
            SortTests.Sorting.sfList sList3 = new sfList(aArray);

            KeyedList<string, int> kList = new KeyedList<string, int>(aArray.Length);
            KeyedList<string, int> kListDesc = new KeyedList<string, int>(aArray.Length);
            int val = 1;
            foreach (string s in sList)
            {
                kList.Add(s, val); //val contains original line position of string
                kListDesc.Add(s, val);
                ++val;
            }
            sList.Clear();

            TimeCounter StopWatch = new TimeCounter();
            StopWatch.SetOneProcessorAffinity();
            Console.WriteLine("base Cycles = {0} IsHighRes = {1}", TimeCounter.Frequency, TimeCounter.IsHighResolution);
            StopWatch.Start();
            //List<string> cList = StringSort.Sort(aList);
            List<string> cList = new List<string>(aList);
            StopWatch.Stop();
            Console.WriteLine("MS to sort copy: {0}", StopWatch.ElapsedMilSec);
            int n = 0;
            foreach (string s in cList)
            {
                Console.WriteLine(s);
                if (++n > 1) break;
            }

            aList.Clear();

            StopWatch.StartNew();
            Array.Sort<string>(bArray, CompareString);
            StopWatch.Stop();
            Console.WriteLine("MS to Array.Sort<string> bArray: {0}", StopWatch.ElapsedMilSec);
            for (int i = 0; i < bArray.Length; ++i)
            {
                Console.WriteLine(bArray[i]);
                if (i == 1) break;
            }
            //reset bArray to unsorted
            aArray.CopyTo(bArray, 0);

            StopWatch.StartNew();
            StringSort.Sort(aArray);
            StopWatch.Stop();
            Console.WriteLine("MS to sort aArray: {0}", StopWatch.ElapsedMilSec);
            for (int i = 0; i < aArray.Length; ++i)
            {
                Console.WriteLine(aArray[i]);
                if (i == 1) break;
            }

            StopWatch.StartNew();
            Sedgewick.Sort(ref bArray);
            StopWatch.Stop();
            Console.WriteLine("MS to Sedgewick Sort bArray: {0}", StopWatch.ElapsedMilSec);
            for (int i = 0; i < bArray.Length; ++i)
            {
                Console.WriteLine(bArray[i]);
                if (i == 1) break;
            }

            StopWatch.StartNew();
            sList2.Sort(CompareString); //sort using List<string> method
                                        //sList2.Sort(CompareString);
            StopWatch.Stop();
            Console.WriteLine("MS to List.Sort() sList2: {0}", StopWatch.ElapsedMilSec);
            for (int i = 0; i < sList2.Count; ++i)
            {
                Console.WriteLine(sList2[i]);
                if (i == 1) break;
            }
            sList2.Clear();

            StopWatch.StartNew();
            sList3.Sort(); //sort using StringSort replacement
            StopWatch.Stop();
            Console.WriteLine("MS to sfList.Sort() sList3: {0}", StopWatch.ElapsedMilSec);
            for (int i = 0; i < sList3.Count; ++i)
            {
                Console.WriteLine(sList3[i]);
                if (i == 1) break;
            }

            StopWatch.StartNew();
            kList.Sort();
            StopWatch.Stop();
            Console.WriteLine("MS to KeyedList.Sort() kList: {0}", StopWatch.ElapsedMilSec);
            for (int i = 0; i < kList.Count; ++i)
            {
                Console.WriteLine(kList[i]);
                if (i == 1) break;
            }
            //test the sort
            bool sorted = IsSortedTest(kList, kList.SortAscd);
            Console.WriteLine("Test: kList is sorted = {0}", sorted);

            kListDesc.SortAscd = false; //try sorting in descending order
            StopWatch.StartNew();
            kListDesc.Sort();
            StopWatch.Stop();
            Console.WriteLine("MS to KeyedList.Sort() kListDesc: {0}", StopWatch.ElapsedMilSec);
            for (int i = 0; i < kListDesc.Count; ++i)
            {
                Console.WriteLine(kListDesc[i]);
                if (i == 1) break;
            }
            //test the sort
            sorted = IsSortedTest(kListDesc, kListDesc.SortAscd);
            Console.WriteLine("Test: kListDesc is sorted descending = {0}", sorted);

            string[] wArray = File.ReadAllLines(@"Rndm-dic.txt", Encoding.Default);

            //this is slower than stringSort
            //WQSort wqs = new WQSort();
            //StopWatch.StartNew();
            //wqs.Sort(wArray);
            //StopWatch.Stop();
            //Console.WriteLine("MS to WQSort.Sort() wArray: {0}", StopWatch.ElapsedMilSec);
            //for (int i = 0; i < wArray.Length; ++i)
            //{
            //  Console.WriteLine(wArray[i]);
            //  if (i == 1) break;
            //}
            List<int> lstInt = new List<int>(wArray.Length);
            Random rand = new Random((int)StopWatch.ElapsedMilSec);
            for (int i = 0; i < wArray.Length; ++i)
            {
                lstInt.Add(rand.Next());
            }
            StopWatch.StartNew();
            lstInt.Sort();
            StopWatch.Stop();
            Console.WriteLine("MS to List<int>.Sort() with {0} elements: {1:f1}", wArray.Length, StopWatch.ElapsedMilSec);
            Console.WriteLine("First, Second, Last integer in sorted List");
            Console.WriteLine(lstInt[0]);
            Console.WriteLine(lstInt[1]);
            Console.WriteLine(lstInt[wArray.Length - 1]);
            StopWatch.RestoreProcessorAffinity(); //set this thread back to default Proccessor Affinity
        }

        private static bool IsSortedTest(KeyedList<string, int> kList, bool Ascending)
        {
            for (int i = 1; i < kList.Count; ++i)
            {
                int dif = String.Compare(kList[i - 1].Key, kList[i].Key, StringComparison.Ordinal);
                if (!Ascending) { dif = -dif; }
                if (dif > 0)
                    return false;
            }
            return true;
        }

        private static int CompareString(string x, string y)
        {
            return String.Compare(x, y, StringComparison.Ordinal);
        }

        static Random rnd = new Random();

        static void CreateRandomFile(string filePath)
        {
            string filename = Path.GetFileName(filePath);
            string directory = Path.GetDirectoryName(filePath);
            string outfile;
            string outputDir = Environment.CurrentDirectory;
            if (outputDir.Length > 0)
            {
                outfile = outputDir + "\\Rndm-" + filename;
            }
            else
            {
                outfile = "Rndm-" + filename;
            }
            if (File.Exists(outfile)) { return; } //don't create randomized file if it exists

            Console.WriteLine("Creating random dictionary for sorting");

            string[] sArray = File.ReadAllLines(filePath, Encoding.Default);
            int len = sArray.Length;

            string[] shuffle = new string[len * 4];
            int maxNmber = 20000;
            int pos = 0;
            for (int k = 0; k < 4; ++k)
            {
                List<string> sList = new List<string>(sArray);
                for (int ix = len; ix > 0; --ix)
                {
                    int nmbr = rnd.Next(1, maxNmber);
                    int index = rnd.Next(0, ix); //ix is up to (not including) the upper bound
                                                 //shuffle[pos++] = String.Concat(nmbr.ToString(), " ", sList[index]);
                    shuffle[pos++] = String.Concat(sList[index], nmbr.ToString());
                    sList.RemoveAt(index);
                }
            }
            File.WriteAllLines(outfile, shuffle, Encoding.Default);
        }
    }
}
