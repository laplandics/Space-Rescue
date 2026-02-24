public class SpaceStation : Unit
{
    public override void Initialize(UnitData unitData)
    {
        base.Initialize(unitData);
        CreateSystem<SpaceStationConstructor>();
        CreateSystem<SpaceStationInitializer>();
        CreateSystem<SpaceStationInteractor>();
        CreateSystem<SpaceStationSelector>();
        CreateSystem<SpaceStationObserver>();
        
        CreateSystem<UnitColliderBuilder>();
        StartSystems();
        
        FindSystem<SpaceStationInitializer>().BeginInitialization();
    }

    public override void Deinitialize()
    {
        base.Deinitialize();
        EndSystems();
        DeleteSystem<SpaceShipConstructor>();
        DeleteSystem<SpaceStationInitializer>();
        DeleteSystem<SpaceStationInteractor>();
        DeleteSystem<SpaceStationSelector>();
        DeleteSystem<SpaceStationObserver>();
        
        DeleteSystem<UnitColliderBuilder>();
    }
}