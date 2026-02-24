using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;

public class CameraZoom : MonoBehaviour
{
    public CameraSettings settings;
    public PixelPerfectCamera pixelPerfectCamera;
    
    private int _currentZoomLevel;
    private GameInputs _inputs;

    private void Awake()
    {
        Eventer.Subscribe<SceneStarted>(OnSceneStarted);
        Eventer.Subscribe<SceneEnded>(OnSceneEnded);
        for (var i = 0; i < settings.pixelScales.Length; i++)
        { if (pixelPerfectCamera.assetsPPU != settings.pixelScales[i]) continue; _currentZoomLevel = i; }
    }

    private void OnSceneStarted(SceneStarted obj)
    {
        _inputs = G.GetService<InputService>().GetInputs();
        _inputs.Camera.Zoom.Enable();
        _inputs.Camera.Zoom.performed += Zoom;
    }

    private void Zoom(InputAction.CallbackContext ctx)
    {
        _currentZoomLevel = Mathf.Clamp(_currentZoomLevel +
            (int)ctx.ReadValue<float>(), 0, settings.pixelScales.Length - 1);
        pixelPerfectCamera.assetsPPU = settings.pixelScales[_currentZoomLevel];
    }

    private void OnSceneEnded(SceneEnded obj)
    {
        _inputs.Camera.Zoom.performed -= Zoom;
        _inputs.Camera.Zoom.Disable();
        _inputs = null;
        Eventer.Unsubscribe<SceneStarted>(OnSceneStarted);
        Eventer.Unsubscribe<SceneEnded>(OnSceneEnded);
    }
}