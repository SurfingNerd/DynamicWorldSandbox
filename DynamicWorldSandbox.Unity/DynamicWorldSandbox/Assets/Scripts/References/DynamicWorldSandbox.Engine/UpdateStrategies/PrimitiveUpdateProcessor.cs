using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DynamicWorldSandbox.Model;

namespace DynamicWorldSandbox.Engine.UpdateStrategies
{
  public class PrimitiveUpdateProcessor : IGraduallyUpdateProcessor
  {
    private World m_world;
    private ProcessFunction m_function;

    public void Initialize(World world, ProcessFunction function)
    {
      throw new NotImplementedException();
    }

    public void ProcessStep(int tickCount)
    {
      throw new NotImplementedException();
    }
  }
}