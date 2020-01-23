using DynamicWorldSandbox.Model.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace DynamicWorldSandbox.Model.Modules.UnitModule
{
    public class UnitModule : IModule
    {
        public static UnitModule LastInitializedInstance;
        public List<Unit>[,] Units;

        public void Initialize(World world)
        {
            Units = new List<Unit>[world.Width,world.Height];
            LastInitializedInstance = this;
        }
    }
}

namespace DynamicWorldSandbox.Model
{
    public partial class Tile
    {
        /// <summary>
        /// 0 Means an ocen tile.
        /// </summary>
        public List<Unit> Units
        {
            get
            {
                return DynamicWorldSandbox.Model.Modules.UnitModule.UnitModule.LastInitializedInstance.Units[X, Y];
            }
        }
    }
}
