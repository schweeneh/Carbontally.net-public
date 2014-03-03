using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using log4net;

namespace Carbontally
{
    public static class Helpers
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(Helpers));

        public static T ThrowIfNull<T>(this T value, string variableName) where T : class 
        {
            if (value == null) {
                log.Error(string.Format("{0} cannot be null.", variableName));
                throw new ArgumentNullException(string.Format("{0} cannot be null.", variableName));
            }

            return value;
        } 
    }
}