using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace K.Helpers.Commands
{
    public static class ArgsConverter
    {
        public static List<Tuple<MethodInfo, object>> ToMethods(string[] args, Assembly assembly = null)
        {
            var parameters = MethodBase.GetCurrentMethod().GetParameters();

            if (args == null || !args.Any())
                throw new ArgumentNullException(parameters[0].Name);

            var methods = new List<Tuple<MethodInfo, object>>();

            var types = assembly == null ? Assembly.GetExecutingAssembly().GetTypes() : assembly.GetTypes();

            foreach (var arg in args)
            {
                if (string.IsNullOrEmpty(arg))
                    continue;

                foreach (var type in types)
                {
                    foreach (var func in type.GetMethods())
                    {
                        if (func.Name.ToLower() != arg.ToLower())
                            continue;

                        methods.Add(Tuple.Create(func, Activator.CreateInstance(type)));
                    }
                }
            }

            return methods.Count > 0 ? methods : null;
        }
    }
}
