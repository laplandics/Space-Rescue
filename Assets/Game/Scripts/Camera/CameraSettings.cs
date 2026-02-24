using UnityEngine;

[CreateAssetMenu(fileName = "Camera", menuName = "Settings/Camera")]
public class CameraSettings : ScriptableObject
{
    public float moveSpeed;
    public float dragSpeed;
    public int[] pixelScales;
}