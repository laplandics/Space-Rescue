using System.Collections;
using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    public GameService[] services;
    public SceneManager[] managers;
    
    private IEnumerator Start()
    {
        yield return RunServices();
        yield return RunManagers();
        Eventer.Invoke(new SceneStarted());
        Application.quitting += End;
    }

    private IEnumerator RunServices()
    {
        foreach (var service in services) { yield return service.Run(); }
        G.CacheServices(services);
    }

    private IEnumerator RunManagers()
    {
        foreach (var manager in managers) { yield return manager.Run(); }
        G.CacheManagers(managers);
    }

    private void End()
    {
        Eventer.Invoke(new SceneEnded());
        Eventer.ClearSubscribers();
        EndManagers();
        EndServices();
    }

    private void EndManagers()
    {
        
        G.ClearManagers();
    }

    private void EndServices()
    {
        
        G.ClearServices();
    }
}