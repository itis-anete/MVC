using Cannabis.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Cannabis
{
    public static class ProjectInfo
    {
        private static readonly Assembly CurrentAssembly = Assembly.GetCallingAssembly();

        public static readonly string ProjectName = CurrentAssembly.GetName().Name;

        private static readonly Type ControllerInterfaceType = typeof(ICannabisController);
        public static readonly IReadOnlyCollection<Type> ControllerTypes = CurrentAssembly
            .GetTypes()
            .Where(type =>
                type != ControllerInterfaceType &&
                ControllerInterfaceType.IsAssignableFrom(type))
            .ToArray();
    }
}
