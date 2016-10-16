using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicWorldSandbox.Scheduler
{

    internal static class SomeGeneralPerformanecTests
    {
        public static void RunHashCodeTest()
        {
            //shows collision chance distribution of standard implementation of hashset.
            //the first collision appears about the 10.000 element
            //thats pretty good Performence.

            int total = 0;
            int countOfTestIterations = 100000;

            Dictionary<int, int> hashCollisions = new Dictionary<int, int>();
            for (int j = 0; j < countOfTestIterations; j++)
            {
                Dictionary<int, object> objs = new Dictionary<int, object>();
                for (int i = 0; i < int.MaxValue; i++)
                {
                    object obj = new object();
                    int hash = obj.GetHashCode();
                    if (objs.ContainsKey(hash))
                    {
                        if (hashCollisions.ContainsKey(i))
                        {
                            hashCollisions[i] = hashCollisions[i] + 1;
                        }
                        else
                        {
                            hashCollisions.Add(i, 1);
                        }

                        total += i;

                        break;
                    }
                    else
                    {
                        objs.Add(hash, obj);
                    }
                }
            }

            foreach (var kvp in hashCollisions)
            {
                if (kvp.Value > 1)
                    Console.WriteLine(kvp.Key + " - " + kvp.Value + " times.");
            }

            double average = total / countOfTestIterations;
            Console.WriteLine("Average: " + average.ToString("#.######"));

        }

        //public static void RunHashSetVsListPerformanceTest()
        //{
        //    //Testresult:
        //    //HashSet is benefitual over List, even in small sets.
        //    //theres almost no penelty in Add-Operations of HashSets.

        //    int count = 10000000;

        //    IEveryTickSchedule[] shedulesToAdd =
        //        new IEveryTickSchedule[count];

        //    for (int i = 0; i < count; i++)
        //    {
        //        shedulesToAdd[i] = new EveryTickSchedule();
        //    }



        //    for (int i = 0; i < 10; i++)
        //    {
        //        Scheduler sheduler = new Scheduler();
        //        DateTime startAddTest = DateTime.Now;

        //        for (int j = 0; j < count; j++)
        //        {
        //            sheduler.RegisterJob(shedulesToAdd[0]);
        //        }

        //        TimeSpan ts = DateTime.Now - startAddTest;
        //        Console.WriteLine("Add to List: " + ts.TotalSeconds);

        //    }



        //}

        //static void RunPropertyPerformanceTest()
        //{
        //    //property test shows Property are much slower than Fields.

        //    double totalTime = 0;
        //    int totalruns = 0;


        //    for (totalruns = 0; totalruns < 100; totalruns++)
        //    {
        //        DateTime start = DateTime.Now;

        //        PeriodicSchedule periodicSheduler = new PeriodicSchedule();

        //        for (int i = 0; i < 1000000000; i++)
        //        {
        //            periodicSheduler.Period = i;
        //        }

        //        double timeNeeded = (DateTime.Now - start).TotalSeconds;
        //        Console.WriteLine("Run:" + timeNeeded.ToString("#.##########"));

        //    }


        //    Console.WriteLine("Average:" + (totalTime / totalruns).ToString("#.##########"));
        //    Console.ReadLine();
        //}
    }
}
