using Quartz.Spi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinite.GreenBill.WebFront.App_Start
{
    public interface IQuartzInitializer
    {
        void RegisterJobs();
    }
}
