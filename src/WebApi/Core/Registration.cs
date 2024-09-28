using System.Reflection;
using System.Runtime.Loader;

namespace WebApi.Core;

public static class Registration
{
  private static IEnumerable<TypeInfo> GetClassTypes()
  {
    var assemblyProject = Assembly.GetCallingAssembly().GetName().Name + ".dll";
    var assemblyPath = Path.GetDirectoryName(Assembly.GetEntryAssembly()?.Location) ?? "";

    var assembly = Assembly.Load(AssemblyLoadContext.GetAssemblyName(Path.Combine(assemblyPath, assemblyProject)));

    var classTypes = assembly.ExportedTypes.Select(t => IntrospectionExtensions.GetTypeInfo(t)).Where(t => t.IsClass && !t.IsAbstract);

    return classTypes;
  }

  public static IServiceCollection AddServices(this IServiceCollection services)
  {
    foreach (var classType in GetClassTypes())
    {
      var isService = classType.BaseType == typeof(Service);

      if (isService)
      {
        services.AddTransient(classType.AsType());
      }
    }

    return services;
  }

  public static IServiceCollection AddRepositories(this IServiceCollection services)
  {
    foreach (var classType in GetClassTypes())
    {
      var isRepository = classType.BaseType == typeof(Repository);

      if (isRepository)
      {
        services.AddTransient(classType.AsType());
      }
    }

    return services;
  }
}
