using NaughtyAttributes;

public class SpaceStationInteractionUi : UnitUi
{
    private SpaceStationInteractor _interactor;

    public override void Initialize(UnitInteractor interactor)
    {
        if (interactor is not SpaceStationInteractor spaceStationInteractor) return;
        _interactor = spaceStationInteractor;
    }

    public override void Deinitialize()
    {
        _interactor = null;
    }
    
    [Button]
    private void BuildBay() => _interactor.OnBuildBay?.Invoke();

    [Button]
    private void BuildConnector() => _interactor.OnBuildConnector?.Invoke();

}