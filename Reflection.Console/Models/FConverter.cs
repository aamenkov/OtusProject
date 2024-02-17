using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reflection.Console.Models
{
    public static class FConverter
    {
        public static F DeserializeFFromString(string serialized)
        {
            var values = serialized.Split(';');
            var f = new F();

            foreach (var value in values)
            {
                var pair = value.Split('=');
                var propertyName = pair[0];
                var propertyValue = int.Parse(pair[1]);
                typeof(F).GetField(propertyName).SetValue(f, propertyValue);
            }

            return f;
        }

        public static string SerializeFToString(F f)
        {
            return $"i1={f.i1};i2={f.i2};i3={f.i3};i4={f.i4};i5={f.i5}";
        }
    }
}
