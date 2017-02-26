using DynamicWorldSandbox.Model.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace DynamicWorldSandbox.Model.Modules.HydrationModule
{
    public class HydrationModule : IModule
    {
        public static HydrationModule LastInitializedInstance;

        
        public double[,] HydrationValues;
        public World World;

        public void Initialize(World world)
        {
            LastInitializedInstance = this;
            HydrationValues = new double[world.Height, world.Width];
            World = world;
        }

        public double GetTotalWaterAmount()
        {
            double result = 0;
            for (int x = 0; x < World.Width; x++)
            {
                for (int y = 0; y < World.Height; y++)
                {
                    result += HydrationValues[x, y];
                }
            }
            return result;
        }
    }
}



namespace DynamicWorldSandbox.Model
{
    public partial class Tile
    {
        /// <summary>
        /// Hydration means how much water is on the tile.
        /// 0 means a desert tile with no water at all. almost nothing grows there.
        /// 80 means somthing like a swamp.
        /// 100 means a 100% water tile.
        /// some plants and fungi prefer high hydration, some of them require low hydration.
        /// Hydration is also used for other stuff, like you cannot build a house in a ocean tile.
        /// </summary>
        public double Hydration
        {
            get
            {
                return DynamicWorldSandbox.Model.Modules.HydrationModule.HydrationModule.LastInitializedInstance.HydrationValues[X, Y];
            }

            set
            {
                DynamicWorldSandbox.Model.Modules.HydrationModule.HydrationModule.LastInitializedInstance.HydrationValues[X, Y] = value;
            }
        }

        public double WaterLevelHeight
        {
            get
            {
                return TerrainHeight + Hydration;
            }
        }
    }
}