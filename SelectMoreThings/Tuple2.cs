using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelectMoreThings
{
    public static class Tuple2
    {
        public static Tuple<T1_,T2_> Map<T1,T1_,T2, T2_> (this Tuple<T1,T2> before, Func<T1,T1_> map1, Func<T2,T2_> map2)
        {
            return Tuple.Create(map1(before.Item1), map2(before.Item2));
        }
        public static T3 Map<T1,T2,T3> (this Tuple<T1,T2> before, Func<T1,T2,T3> transform)
        {
            return transform(before.Item1, before.Item2);
        }
    }
}
