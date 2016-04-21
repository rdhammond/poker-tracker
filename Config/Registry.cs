using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerTracker.Config
{
    public class Registry : StructureMap.Registry
    {
        public Registry()
        {
            For<IConfig>().Use<Config>().Singleton();
        }
    }
}
