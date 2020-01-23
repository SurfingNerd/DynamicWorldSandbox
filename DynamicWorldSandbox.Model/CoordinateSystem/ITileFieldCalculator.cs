using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicWorldSandbox.Model.CoordinateSystem
{
    public interface ITileFieldCalculator
    {
        Tile[] GetAllNeighbours(int x, int y, World world);
        //Tile[] GetAllNeighbours(Tile waterTile, World m_world);
    }
}
