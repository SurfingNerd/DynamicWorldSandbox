using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicWorldSandbox.Engine.UpdateStrategies
{
    public class IterativeGraduallyUpdateProcessor : IGraduallyUpdateProcessor
    {
        ProcessFunction m_function;
        DynamicWorldSandbox.Model.World m_world;

        int m_updateDistance;
        int m_numTiles;
        int m_worldWidth;
        int m_worldHeigh;
        int m_counterForDiversityUpdate;


        public IterativeGraduallyUpdateProcessor(int updateDistance)
        {
            m_updateDistance = updateDistance;
        }

        public void Initialize(DynamicWorldSandbox.Model.World world, ProcessFunction function)
        {
            m_world = world;
            m_function = function;
            m_numTiles = m_world.Height * m_world.Width;
            m_worldWidth = m_world.Width;
            m_worldHeigh = m_world.Height;
        }

        public void ProcessStep(int tickCount)
        {

            if (m_counterForDiversityUpdate > m_updateDistance * m_numTiles)
            {
                m_counterForDiversityUpdate -= m_updateDistance * m_numTiles;
            }

            int x = 0;
            int y = 0;

            GetXY(m_counterForDiversityUpdate, ref x, ref y);
            int lastY = 0;
            while (y >= lastY)
            {
                m_function(tickCount, x, y);              
                m_counterForDiversityUpdate += m_updateDistance;
                lastY = y;
                GetXY(m_counterForDiversityUpdate, ref x, ref y);
            }
        }

        private void GetXY(int counter, ref int x, ref int y)
        {
            //todo find better pattern.
            y = (counter / m_worldWidth) % m_worldHeigh;
            x = counter % m_worldWidth;
        }
    }
}
