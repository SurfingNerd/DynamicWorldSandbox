using DynamicWorldSandbox.Model.Wildlife;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicWorldSandbox.Model
{
    public class Tile
    {
        public readonly int X;
        public readonly int Y;

        List<Building> Buildings = new List<Building>();
        List<Creature> Creatures = new List<Creature>();

        Dictionary<LocalWildlife, double> Wildlife = new Dictionary<LocalWildlife, double>();


        public Tile(int x, int y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// Hydration means how much water is on the tile.
        /// 0 means a desert tile with no water at all. almost nothing grows there.
        /// 80 means somthing like a swamp.
        /// 100 means a 100% water tile.
        /// some plants and fungi prefer high hydration, some of them require low hydration.
        /// Hydration is also used for other stuff, like you cannot build a house in a ocean tile.
        /// </summary>
        public double Hydration = 0;

        /// <summary>
        /// 0 Means an ocen tile.
        /// </summary>
        public double TerrainHeight = 0;


        public double WaterLevelHeight
        {
            get
            {
                return TerrainHeight + Hydration;
            }
        }

        //public double Temperature = 0;
        Inventory Inventory = new Inventory();        

        public void AddBuilding(Building building)
        {
            Buildings.Add(building);
        }

        public void AddCreature(Creature creature)
        {
            Creatures.Add(creature);
        }
    }
}
