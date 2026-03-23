using System.Reflection;
using Joyersch.Monogame.Logging;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using MonoGame.Extended.Graphics;

namespace Joyersch.Monogame.Graphics;

public static class AtlasManager
{
    public static void Initialize(ContentManager content)
    {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();
        var atlasProperties = new List<(Type Type, PropertyInfo Property, UsesAtlasAttribute Attribute)>();
        var regionProperties = new List<(Type Type, PropertyInfo Property, UsesAtlasRegionAttribute Attribute)>();
        
        foreach (var assembly in assemblies)
        {
            try
            {
                var types = assembly.GetTypes();
                foreach (var type in types)
                {
                    var properties = type.GetProperties(
                        BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
                    
                    foreach (var prop in properties)
                    {
                        var atlasAttr = prop.GetCustomAttribute<UsesAtlasAttribute>();
                        var regionAttr = prop.GetCustomAttribute<UsesAtlasRegionAttribute>();
                        
                        if (atlasAttr != null)
                            atlasProperties.Add((type, prop, atlasAttr));
                        
                        if (regionAttr != null)
                            regionProperties.Add((type, prop, regionAttr));
                    }
                }
            }
            catch (ReflectionTypeLoadException exception)
            {
               Log.Warning(exception.Message);
            }
        }
        
        if (atlasProperties.Count == 0) return;
        
        var requiredAtlasNames = atlasProperties
            .Select(a => a.Attribute.AtlasName)
            .Distinct()
            .ToList();

        var loadedAtlases = new Dictionary<string, Texture2DAtlas>();
        foreach (var atlasName in requiredAtlasNames)
        {
            try
            {
                var texture = content.GetTexture($"Atlas/{atlasName}");
                loadedAtlases[atlasName] = new Texture2DAtlas(texture);
            }
            catch (Exception ex)
            {
                Log.Error($"Could not load atlas '{atlasName}': {ex.Message}");
            }
        }
        
        foreach (var (type, prop, attr) in atlasProperties)
        {
            if (!loadedAtlases.TryGetValue(attr.AtlasName, out var atlas))
            {
                Log.Error($"Atlas '{attr.AtlasName}' not loaded for {type.Name}.{prop.Name}");
                continue;
            }

            if (prop.CanWrite)
            {
                prop.SetValue(null, atlas);
            }
            else
            {
                Log.Error($"Property '{prop.Name}' on {type.Name} is read-only");
            }
        }
        
        foreach (var (type, regionProp, regionAttr) in regionProperties)
        {
            var atlasProp = type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static)
                .FirstOrDefault(p => p.GetCustomAttribute<UsesAtlasAttribute>() != null);
            
            if (atlasProp == null)
            {
                Log.Error($"No 'Atlas' property with UsesAtlasAttribute found on {type.Name}");
                continue;
            }

            var atlas = (Texture2DAtlas)atlasProp.GetValue(null);
            if (atlas == null)
            {
                Log.Error($"Atlas not initialized for {type.Name}");
                continue;
            }

            var rect = new Rectangle(regionAttr.X, regionAttr.Y, regionAttr.Width, regionAttr.Height);
            var region = atlas.CreateRegion(rect, regionAttr.RegionName);

            if (regionProp.CanWrite)
            {
                regionProp.SetValue(null, region);
            }
            else
            {
                Log.Error($"Property '{regionProp.Name}' on {type.Name} is read-only");
            }
        }
    }
}