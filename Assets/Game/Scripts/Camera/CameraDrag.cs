using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraDrag : MonoBehaviour
{
    public Camera cam;
    public CameraSettings settings;
    
    private Coroutine _dragCoroutine;
    private Vector3 _dragOrigin;
    
    private void Awake()
    {
        Eventer.Subscribe<SceneStarted>(OnSceneStarted);
        Eventer.Subscribe<SceneEnded>(OnSceneEnded);
    }

    private void OnSceneStarted(SceneStarted obj)
    {
        _dragCoroutine = StartCoroutine(Drag());
    }

    private IEnumerator Drag()
    {
        while (true)
        {
            if (Mouse.current.middleButton.wasPressedThisFrame) _dragOrigin = cam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            if (Mouse.current.middleButton.isPressed)
            {
                var currentPos = cam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
                var diff = _dragOrigin - currentPos;
                if (IsDiffValuable(diff)) transform.position += diff * (Time.deltaTime * settings.dragSpeed);
            }
            yield return new WaitForEndOfFrame();
        }
    }

    private bool IsDiffValuable(Vector3 diff)
    {
        return !(Mathf.Abs(diff.x) < 0.05f) ||
               !(Mathf.Abs(diff.y) < 0.05f) ||
               !(Mathf.Abs(diff.z) < 0.05f);
    }

    private void OnSceneEnded(SceneEnded obj)
    {
        Eventer.Unsubscribe<SceneStarted>(OnSceneStarted);
        Eventer.Unsubscribe<SceneEnded>(OnSceneEnded);
        if (_dragCoroutine != null) StopCoroutine(_dragCoroutine);
    }
}