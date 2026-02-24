using System;

[Serializable]
public class SpaceStationInteractor : UnitInteractor
{
    public Action OnBuildBay;
    public Action OnBuildConnector;
    
    private SpaceStationInteractionUi _ui;

    public override void ShowUi()
    {
        _ui = Owner.gameObject.AddComponent<SpaceStationInteractionUi>();
        _ui.Initialize(this);
        OnBuildBay += BayBuild;
        OnBuildConnector += ConnectorBuild;
    }

    public override void HideUi()
    { Owner.DestroyComponent<SpaceStationInteractionUi>(); _ui = null; }
    
    private void BayBuild()
    {
        
    }

    private void ConnectorBuild()
    {
        
    }
}
