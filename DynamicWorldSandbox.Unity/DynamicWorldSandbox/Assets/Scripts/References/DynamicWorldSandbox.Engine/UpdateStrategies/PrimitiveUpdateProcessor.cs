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
      m_world = world;
      m_function = function;
    }

    public void ProcessStep(int tickCount)
    {
      for (int x = 0; x < m_world.Width; x++)
      {
        for (int y = 0; y < m_world.Height; y++)
        {
          m_function.Invoke(tickCount, x, y);
        }
      }
    }
  }
}