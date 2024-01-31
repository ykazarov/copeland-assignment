using System.Reflection;
using AutoMapper;

namespace copeland.assignment.services.mappers;

public static class MapperConfigurator
{
    public static MapperConfiguration ConfigureMapper()
    {
        return new MapperConfiguration(cfg =>
        {
            var loadedAssemblies = Assembly.GetExecutingAssembly().GetLoadedModules().Select(m => m.Assembly).ToList();
            loadedAssemblies.AddRange(Assembly.GetExecutingAssembly().GetReferencedAssemblies().Select(a => Assembly.Load(a)));
    
            var profiles = loadedAssemblies.SelectMany(a => a.GetTypes())
                .Where(t => t.IsSubclassOf(typeof(Profile)))
                .ToList();
    
            cfg.AddProfiles(profiles.Select(t => (Profile)Activator.CreateInstance(t)));
        });
    }
}