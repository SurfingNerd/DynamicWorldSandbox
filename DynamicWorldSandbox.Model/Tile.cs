using DynamicWorldSandbox.Model.Wildlife;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicWorldSandbox.Model
{
    public partial class Tile
    {
        public readonly int X;
        public readonly int Y;

        //List<Building> Buildings = new List<Building>();

        Dictionary<LocalWildlife, double> Wildlife = new Dictionary<LocalWildlife, double>();


        public Tile(int x, int y)
        {
            X = x;
            Y = y;
        }



        




        //public double Temperature = 0;
        //Inventory Inventory = new Inventory();        

        //public void AddBuilding(Building building)
        //{
        //    Buildings.Add(building);
        //}

        //public void AddCreature(Unit creature)
        //{
        //    Units.Add(creature);
        //}
    }
}
