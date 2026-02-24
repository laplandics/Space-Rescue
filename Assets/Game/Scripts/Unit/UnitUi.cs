using UnityEngine;

public abstract class UnitUi : MonoBehaviour
{
    public abstract void Initialize(UnitInteractor interactor);
    public abstract void Deinitialize();
}