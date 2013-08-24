using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebMatrix.WebData;
using System.Threading;

namespace Carbontally.Infrastructure
{
    public static class Initializer
    {
        public static void InitializeSimpleMemberShip() {
                WebSecurity.InitializeDatabaseConnection("CarbontallyContext", "UserProfile", "UserId", "UserName", autoCreateTables: true);
        }
    }
}