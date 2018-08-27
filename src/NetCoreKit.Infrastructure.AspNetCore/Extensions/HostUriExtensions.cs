using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace NetCoreKit.Infrastructure.AspNetCore.Extensions
{
  public static class HostUriExtensions
  {
    public static string GetHostUri(this IConfiguration config, IHostingEnvironment env, string groupName)
    {
      return env.IsDevelopment() ? config.GetExternalHostUri(groupName) : config.GetInternalHostUri(groupName);
    }

    public static string GetInternalHostUri(this IConfiguration config, string groupName)
    {
      var group = config
          .GetSection("Hosts")
          ?.GetSection("Internals")
          ?.GetSection(groupName);

      var serviceHost = $"{Environment.GetEnvironmentVariable(group.GetValue<string>("Host"))}";
      var servicePort = $"{Environment.GetEnvironmentVariable(group.GetValue<string>("Port"))}";
      var basePath = $"{group.GetValue("BasePath", string.Empty)}";

      return $"http://{serviceHost}:{servicePort}{basePath}";
    }

    public static string GetExternalHostUri(this IConfiguration config, string groupName)
    {
      return config
          .GetSection("Hosts")
          ?.GetSection("Externals")
          ?.GetSection(groupName)
          ?.GetValue<string>("Uri");
    }

    public static string GetBasePath(this IConfiguration config)
    {
      return config
        .GetSection("Hosts")
        ?.GetValue<string>("BasePath");
    }

    public static string GetExternalCurrentHostUri(this IConfiguration config)
    {
      return config
        .GetSection("Hosts")
        ?.GetSection("Externals")
        ?.GetValue<string>("CurrentUri");
    }
  }
}
