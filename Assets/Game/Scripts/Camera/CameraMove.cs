using System.Collections;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public CameraSettings settings;
    
    private GameInputs _inputs;
    private Coroutine _moveCoroutine;
    
    private void Awake() { Eventer.Subscribe<SceneStarted>(OnSceneStarted); Eventer.Subscribe<SceneEnded>(OnSceneEnded); }

    private void OnSceneStarted(SceneStarted obj)
    {
        _inputs = G.GetService<InputService>().GetInputs();
        _inputs.Camera.Move.Enable();
        _moveCoroutine = StartCoroutine(Move());
    }

    private IEnumerator Move()
    {
        while (true)
        {
            var dir = _inputs.Camera.Move.ReadValue<Vector2>().normalized;
            transform.position += new Vector3(dir.x, dir.y, 0) * (settings.moveSpeed * Time.deltaTime);
            yield return null;
        }
    }

    private void OnSceneEnded(SceneEnded obj)
    {
        _inputs.Camera.Move.Disable();
        if (_moveCoroutine != null) StopCoroutine(_moveCoroutine);
        _inputs = null;
        Eventer.Unsubscribe<SceneStarted>(OnSceneStarted);
        Eventer.Unsubscribe<SceneEnded>(OnSceneEnded);
    }
}
