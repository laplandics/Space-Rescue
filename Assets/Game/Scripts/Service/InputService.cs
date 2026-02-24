using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "InputService", menuName = "Services/InputService")]
public class InputService : GameService
{
    private GameInputs _inputs;
    
    public override IEnumerator Run()
    {
        _inputs = new GameInputs();
        yield break;
    }

    public void EnableInputs()
    {
        _inputs.Enable();
    }

    public void DisableInputs()
    {
        _inputs.Disable();
    }

    public GameInputs GetInputs() => _inputs;
    
     public override IEnumerator End()
     {
         _inputs.Disable();
         _inputs = null;
         yield break;
     }
}