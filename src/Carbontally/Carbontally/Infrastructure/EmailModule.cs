using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ninject.Modules;
using Carbontally.Abstract;
using Carbontally.Concrete;

namespace Carbontally.Infrastructure
{
    public class EmailModule : NinjectModule
    {
        public override void Load() {
            Bind<ISmtpClient>().To<CarbontallySmtpClient>();
        }
    }
}