using DynamicWorldSandbox.Model.Plants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DynamicWorldSandbox.Scheduler;

namespace DynamicWorldSandbox.Engine.Plants
{
    public class TreeProcessor : Processor<Tree>
    {
        public int MinimumTreeMass;
        public int MaximumTreeMass;
        
        

        public override void RegisterShedules(Tree element, Scheduler.Scheduler sheduler)
        {
            //throw new NotImplementedException();
            
        }
    }
}
