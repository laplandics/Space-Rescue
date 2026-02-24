using System;
using System.Collections.Generic;

public static class G
{
    private static Dictionary<Type, GameService> _services;
    private static Dictionary<Type, SceneManager> _managers;
    
    public static void CacheServices(GameService[] services)
    {
        _services = new Dictionary<Type, GameService>();
        foreach (var service in services) { _services.Add(service.GetType(), service); }
    }
    
    public static void CacheManagers(SceneManager[] managers)
    {
        _managers = new Dictionary<Type, SceneManager>();
        foreach (var manager in managers) { _managers.Add(manager.GetType(), manager); }
    }

    public static T GetService<T>() where T : GameService => (T)_services.GetValueOrDefault(typeof(T));
    public static T GetManager<T>() where T : SceneManager => (T)_managers.GetValueOrDefault(typeof(T));
    
    public static void ClearServices() { _services.Clear(); _services = null; }
    public static void ClearManagers() { _managers.Clear(); _managers = null; }
}