using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelectMoreThings
{
    class Sample
    {
        static Tuple<string, string> GetName()

        {
            return Tuple.Create("erik", "jongbloed");
        }
        static string InitialOf(string name)
        {
            if (name == null) return null;
            if (name.Length == 0) return "";
            return Char.ToUpper(name[0]) + ".";
        }
        static string Capitalize(string name)
        {
            if (name == null) return null;
            if (name.Length == 0) return "";
            if (name.Length == 1) return name.ToUpper();
            return Char.ToUpper(name[0]) + name.Substring(1);
        }
        public static void Main()
        {
            var myname = GetName();

            var mynameOnFile = 
                GetName()
                .Map(InitialOf, Capitalize)
                .Map((initial, surname) => $"Regards, {initial} {surname}.");

            Console.WriteLine(mynameOnFile);

            Console.ReadLine();
        }
    }
}
