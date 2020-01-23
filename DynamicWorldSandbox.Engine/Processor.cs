using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DynamicWorldSandbox.Scheduler;

namespace DynamicWorldSandbox.Engine
{
    public abstract class Processor<T>
    {
        //public void RegisterShedules()

        public abstract void RegisterShedules(T element, Scheduler.Scheduler sheduler);
    }

    //public abstract class PeriodicScheduleProcessor<T> : Processor<T>
    //{
    //    int ScheduleFrequency;
    //    public override void RegisterShedules(T element, Scheduler.Scheduler scheduler)
    //    {
    //        //scheduler.RegisterJob(PeriodicSchedule)     
    //    }
    //    public void UnregisterSchedule(Scheduler.Scheduler scheduler)
    //    {
    //
    //    }
    //}
}
