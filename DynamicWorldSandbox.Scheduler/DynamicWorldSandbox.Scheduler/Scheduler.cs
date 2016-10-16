using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicWorldSandbox.Scheduler
{
    public class Scheduler
    {
        class PeriodicScheduleManager
        {
            public int Period;
            private Dictionary<int, HashSet<IPeriodicSchedule>> m_modValueShedules;

            public PeriodicScheduleManager(int period)
            {
                Period = period;
                //int possibleModResults = period - 1;
                m_modValueShedules = new Dictionary<int, HashSet<IPeriodicSchedule>>();

                //for (int i = 0; i <= possibleModResults; i++)
                //{
                //    m_modValueShedules[i] = new HashSet<PeriodicSchedule>();
                //}
            }
            
            public void Run(int tick)
            {
                int modValue = tick % Period;

                HashSet<IPeriodicSchedule> shedules;

                if (m_modValueShedules.TryGetValue(modValue, out shedules))
                {
                    foreach (var shedule in shedules)
                    {
                        shedule.Run(tick);
                    }
                }
            }

            public void AddPeriodicShedule(int currentTick, IPeriodicSchedule shedule)
            {
                int modValue = currentTick % Period;

                HashSet<IPeriodicSchedule> hashSet = null;
                if (!m_modValueShedules.TryGetValue(shedule.Period, out hashSet))
                {
                    hashSet = new HashSet<IPeriodicSchedule>();
                    m_modValueShedules.Add(modValue, hashSet);
                }

                hashSet.Add(shedule);
            }

            public void RemovePeriodicShedule(int currentTick, IPeriodicSchedule shedule)
            {
                foreach (var kvp in m_modValueShedules)
                {
                    if (kvp.Value.Contains(shedule))
                    {
                        kvp.Value.Remove(shedule);
                    }
                }
            }
        }

        public int NearFutureCashSize
        {
            get
            {
                return m_NearFutureOnceSchedules.Length;
            }
        }

        private int m_lastTick;

        private HashSet<IOnceSchedule>[] m_NearFutureOnceSchedules;
        private int m_nearFutureCashSize;
        private Dictionary<int, PeriodicScheduleManager> m_periodicSheduleManagers = new Dictionary<int, PeriodicScheduleManager>();

        public Scheduler(int nearFutureCashSize = 1000)
        {
            
            m_nearFutureCashSize = nearFutureCashSize;
            //m_NearFutureOnceSchedules = new List<HashSet<OnceSchedule>>(nearFutureCashSize);
            m_NearFutureOnceSchedules = new HashSet<IOnceSchedule>[nearFutureCashSize];
        }

        HashSet<IEveryTickSchedule> m_everTickSchedules = new HashSet<IEveryTickSchedule>();
        //Dictionary<int, HashSet<DynamicWorldSandbox.Scheduler.PeriodicSchedule>> m_periodicShedules;


        #region Unregister job
        public void UnregisterJob(ISchedule schedule)
        {
            throw new NotImplementedException("Need to implement UnregisterJob(" + schedule.GetType().FullName + ")");
        }
        #endregion

        #region register job
        public void RegisterJob(ISchedule schedule)
        {
            throw new NotImplementedException("Need to implement RegisterJob(" + schedule.GetType().FullName + ")");
        }

        public void RegisterJob(IEveryTickSchedule schedule)
        {
            m_everTickSchedules.Add(schedule);
        }
        #endregion //register job

        public void RegisterJob(IPeriodicSchedule periodicSchedule)
        {
            PeriodicScheduleManager manager = null;

            if (!m_periodicSheduleManagers.TryGetValue(periodicSchedule.Period, out manager))
            {
                m_periodicSheduleManagers.Add(periodicSchedule.Period, new PeriodicScheduleManager(periodicSchedule.Period));
            }

            //HashSet<PeriodicSchedule> hashSet = null;
            //if (!m_periodicShedules.TryGetValue(periodicSchedule.Period, out hashSet))
            //{
            //    hashSet = new HashSet<PeriodicSchedule>();
            //    m_periodicShedules.Add(periodicSchedule.Period, hashSet);
            //}

            //hashSet.Add(periodicSchedule);


            manager.AddPeriodicShedule(m_lastTick, periodicSchedule);

            
        }

        public void RunTick(int i)
        {
            m_lastTick = i;
            //Every Tick Shedules
            foreach (IEveryTickSchedule everyTickSchedule in m_everTickSchedules)
            {
                everyTickSchedule.Run(i);
            }

            foreach (var kvp in m_periodicSheduleManagers)
            {
                kvp.Value.Run(i);
                //if (kvp.K)
            }

            //TODO: Far Future shedules.
        }

        
    }
}
