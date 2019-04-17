using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace terms_prepaid.Helper_Classes
{
    public static class PrivateFiledGetter
    {
        internal static object GetInstanceField(Type type, object instance, string fieldName)
        {
            BindingFlags bindFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic
                | BindingFlags.Static;
            FieldInfo field = type.GetField(fieldName, bindFlags);
            return field.GetValue(instance);
        }
    }
}
