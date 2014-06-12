using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Carbontally.Controllers;
using Carbontally.Concrete;
using Carbontally.Models;
using Carbontally.Infrastructure;
using System.Data.Entity;
using Carbontally.Domain.Persistence;

namespace Carbontally.IntegrationTests
{
    [TestClass]
    public class AccountControllerTests
    {
        private CarbontallySecurityProvider security;

        [TestInitialize]
        public void Initialize() {
            Initializer.InitializeSimpleMemberShip();
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<CarbontallyContext>());
            security = new CarbontallySecurityProvider();
        }
    }
}
