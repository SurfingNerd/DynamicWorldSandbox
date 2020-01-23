using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace DynamicWorldSandbox.Engine.UpdateStrategies
{
    public delegate void ProcessFunction(int tickNumber, int x, int y);

    public interface IGraduallyUpdateProcessor
    {
        void Initialize(DynamicWorldSandbox.Model.World world, ProcessFunction function);
        void ProcessStep(int tickCount);
    }
}
