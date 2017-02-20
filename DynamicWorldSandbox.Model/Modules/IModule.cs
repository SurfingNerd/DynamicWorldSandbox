using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicWorldSandbox.Model.Modules
{
    public interface IModule
    {
        void Initialize(World world);
    }
}
