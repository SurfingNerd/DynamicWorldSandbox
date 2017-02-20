using DynamicWorldSandbox.Model.Modules.BuildingModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicWorldSandbox.Model.Plants
{
    public class Tree : Building
    {
        /// <summary>
        /// Mass of the tree. 
        /// an indicator for the tree size.
        /// Trees grow and increase their mass over time.
        /// </summary>
        double Mass;
    }   
}
