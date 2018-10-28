using System.Reflection;

namespace Cannabis
{
    public static class ProjectInfo
    {
        private static readonly Assembly CurrentAssembly = Assembly.GetCallingAssembly();

        public static readonly string ProjectName = CurrentAssembly.GetName().Name;
    }
}
