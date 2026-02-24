using System.Collections;
using UnityEngine;

public abstract class SceneManager : MonoBehaviour
{
    public virtual IEnumerator Run() { yield break; }
    public virtual IEnumerator End() { yield break; }
}