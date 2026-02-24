using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    [ReadOnly][SerializeReference] protected UnitData data;
    [ReadOnly][SerializeField] protected UnitConfig config;
    [ReadOnly][SerializeReference] protected List<UnitSystem> systems;
    [ReadOnly][SerializeField] protected List<UnitComponent> components;

    public virtual void Initialize(UnitData unitData)
    { data = unitData; components = new List<UnitComponent>(); systems = new List<UnitSystem>(); 
        config = Loader.Load<UnitConfig>($"Config/{GetType().Name}"); }

    protected void StartSystems() { foreach (var system in systems) system.Initialize(this); }
    
    protected void EndSystems() { foreach (var system in systems) system.Deinitialize(); }

    public virtual void Deinitialize() { DeleteComponents(); }

    public T GetData<T>() where T : UnitData
    { if (data is T unitData) return unitData; return null; }

    public UnitConfig GetConfig() => config;

    public T CreateSystem<T>() where T : UnitSystem, new()
    { var system = Activator.CreateInstance<T>(); systems.Add(system); system.systemName = typeof(T).Name; return system; }

    public void DeleteSystem<T>() where T : UnitSystem
    { var systemToRemove = systems.Find(x => x.GetType() == typeof(T)); systems.Remove(systemToRemove); }

    public T FindSystem<T>() where T : UnitSystem
    { foreach (var system in systems) { if (system is T unitSystem) return unitSystem; } return null; }

    public T CreateComponent<T>(GameObject componentObj) where T : UnitComponent
    { var component = componentObj.AddComponent<T>(); components.Add(component); component.Initialize(this); return component; }

    public void DeleteComponent<T>() where T : UnitComponent
    { var componentToRemove = components.Find(x => x.GetType() == typeof(T)); componentToRemove.Deinitialize(); components.Remove(componentToRemove); }

    public void DeleteComponents()
    { foreach (var component in components) { component.Deinitialize(); } components.Clear(); }

    public UnitComponent CreateComponent(Type componentType, GameObject componentObj)
    { var component = (UnitComponent)componentObj.AddComponent(componentType); components.Add(component); component.Initialize(this); return component; }

    public void DeleteComponent(Type componentType)
    { var componentToRemove = components.Find(x => x.GetType() == componentType); componentToRemove.Deinitialize(); components.Remove(componentToRemove); }
    
    public T FindComponent<T>() where T : UnitComponent
    { foreach (var component in components) { if (component is T unitComponent) return unitComponent; } return null; }

    public UnitComponent[] GetAllComponents() => components.ToArray();
    
    public void DestroyComponent<T>() where T : Component
    { var component = GetComponent<T>(); if (component == null) return; Destroy(component); }
}