using System;
using System.Linq;
using System.Reflection;
using System.Windows.Controls;

namespace sarisari
{
    public static class TypeHelper
    {
            public static Type GetTypeByString(string type, Assembly lookIn)
            {
                var types = lookIn.DefinedTypes.Where(t => t.Name == type && t.IsSubclassOf(typeof(Page)));
                if (types.Count() == 0)
                {
                    throw new ArgumentException("The type you were looking for was not found", "type");
                }
                else if (types.Count() > 1)
                {
                    throw new ArgumentException("The type you were looking for was found multiple times.", "type");
                }
                return types.First().AsType();
            }
    }
}
