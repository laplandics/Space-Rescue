public class SpaceShip : Unit
{
    public override void Initialize(UnitData unitData)
    {
        base.Initialize(unitData);
        CreateSystem<SpaceShipConstructor>();
        CreateSystem<SpaceShipInitializer>();
        CreateSystem<UnitColliderBuilder>();
        
        StartSystems();
        
        FindSystem<SpaceShipInitializer>().BeginInitialization();
    }

    public override void Deinitialize()
    {
        base.Deinitialize();
        EndSystems();
        DeleteSystem<SpaceShipConstructor>();
        DeleteSystem<SpaceShipInitializer>();
        DeleteSystem<UnitColliderBuilder>();
    }
}