
using DynamicWorldSandbox.Model.Modules.BuildingModule;
using System.Collections.Generic;

namespace DynamicWorldSandbox.Model.Modules.BuildingModule
{
    public class BuildingModule : IModule
    {
        public static BuildingModule LastInitializedInstance;
        public List<Building>[,] Buildings;

        public void Initialize(World world)
        {
            Buildings = new List<Building>[world.Width, world.Height];
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
        public List<Building> Buildings
        {
            get
            {
                return DynamicWorldSandbox.Model.Modules.BuildingModule.BuildingModule.LastInitializedInstance.Buildings[X, Y];
            }
        }
    }
}
