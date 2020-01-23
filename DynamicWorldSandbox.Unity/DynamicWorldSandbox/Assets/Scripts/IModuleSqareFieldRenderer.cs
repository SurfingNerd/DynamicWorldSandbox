using DynamicWorldSandbox.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

    /// <summary>
    /// Adds graphical Assets for a DynamicWorldSandbox Module. Like Water for the Water Module, or Rain for a Weather Module, Flowers for a plant Module.
    /// 
    /// </summary>
    public interface ISquareFieldModuleTileRenderer
{
        void Init();
        void UpdateTile(Tile tile);
    }

