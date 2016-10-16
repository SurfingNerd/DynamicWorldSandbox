using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicWorldSandbox.Model
{
    public class Building
    {
        public int Birthday;
        public Inventory Inventory;
    }

    public enum BuildingTyp
    {
        //example Bee Hive, Birds nest
        TreeDetail,
        //Bears, Dragons, Wolf, Tiger
        Cave,
        //Rabbits, Mouse, 
        UndergroundNest
    }
}
