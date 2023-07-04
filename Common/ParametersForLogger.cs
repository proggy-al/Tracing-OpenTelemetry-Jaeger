using System.Reflection;

namespace Common
{
    public static class ParametersForLogger
    {

        public static string GetServiceName(Assembly? entryAssembly)
        {
            var entryAssemblyName = entryAssembly?.GetName();
            return entryAssemblyName?.Name??String.Empty;            
        }

        public static string GetServiceVersion(Assembly entryAssembly)
        {            
            var entryAssemblyName = entryAssembly?.GetName();
            var versionAttribute = entryAssembly?.GetCustomAttributes(false)
                .OfType<System.Reflection.AssemblyInformationalVersionAttribute>()
                .FirstOrDefault();

            return  versionAttribute?.InformationalVersion ?? entryAssemblyName?.Version?.ToString() ?? String.Empty;
        }

    }
}