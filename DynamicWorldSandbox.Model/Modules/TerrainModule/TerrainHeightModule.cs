using DynamicWorldSandbox.Model.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicWorldSandbox.Model.Modules.TerrainModule
{
    public class TerrainHeightModule : IModule
    {
        public static TerrainHeightModule LastInitializedInstance;
        public double[,] TerrainHeightValues;

        public void Initialize(World world)
        {
            TerrainHeightValues = new double[world.Height, world.Width];
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
        public double TerrainHeight
        {
            get
            {
                return DynamicWorldSandbox.Model.Modules.TerrainModule.TerrainHeightModule.LastInitializedInstance.TerrainHeightValues[X, Y];
            }

            set
            {
                DynamicWorldSandbox.Model.Modules.TerrainModule.TerrainHeightModule.LastInitializedInstance.TerrainHeightValues[X, Y] = value;
            }
        }
    }
}
