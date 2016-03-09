using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IRepAndServices.IRepository;


namespace IRepAndServices.Services
{
    public class Service:IService
    {
        //Job will run if below code is commented
        private readonly ITestRepository _rep;

        Service(ITestRepository rep)
        {
            if (rep == null)
                throw new ArgumentNullException("rep is null");
            _rep = rep;
        }
        public void testFlow()
        {
            //_rep.Insert();
        }
    }
}
