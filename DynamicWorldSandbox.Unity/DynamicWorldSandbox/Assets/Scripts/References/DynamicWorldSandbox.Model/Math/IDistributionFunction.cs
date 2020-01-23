using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace DynamicWorldSandbox.Model.Wildlife
{
    public interface IDistributionFunction
    {
        double Calculate(double value);
    }

    public class GaussianStandardFunction : IDistributionFunction
    {
        public double Calculate(double value)
        {
            return Math.Sqrt(-2.0 * Math.Log(value)) * Math.Sin(2.0 * Math.PI * value);
        }
    }
}
