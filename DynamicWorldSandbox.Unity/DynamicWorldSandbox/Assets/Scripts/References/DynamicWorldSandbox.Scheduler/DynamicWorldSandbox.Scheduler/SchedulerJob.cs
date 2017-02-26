using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace DynamicWorldSandbox.Scheduler
{
    /// <summary>
    /// use IOnceSchedule or IPeriodicSchedule instead of this Interface if it can apply. It (should) 
    /// </summary>
    public interface ISchedule
    {       
        void Run(int tickNumber);
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
    }


}
