using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Carbontally
{
    public static class Helpers
    {
        public static T ThrowIfNull<T>(this T value, string variableName) where T : class 
        {
            if (value == null) {
                throw new NullReferenceException(string.Format("Value is Null: {0}", variableName));
            }

            return value;
        } 
    }
}