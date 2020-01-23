using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicWorldSandbox.Scheduler
{
    public interface ISchedule
    {
        //public virtual string Validate()
        //{
        //    return null;
        //}

        void Run(int tickNumber);
        //{
        //    //TODO: Aufruf gegen neues interface tätigen.
        //}
    }

    public interface IEveryTickSchedule : ISchedule
    {
    }

    public interface IOnceSchedule : ISchedule
    {
        /// <summary>
        /// on what tick the event should happen ?
        /// </summary>
        int Tick { get; }
    }

    public interface IPeriodicSchedule : ISchedule
    {
        /// <summary>
        /// how often will the schedule happen.
        /// if on every tick, use EveryTickSchedule
        /// </summary>
        int Period { get; }

        //public override string Validate()
        //{
        //    if (Period == 1)
        //        return "Use EveryTickSchedule for Period = 1";

        //    return null;
        //}
    }


}
