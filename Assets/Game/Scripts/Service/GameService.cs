using System.Collections;
using UnityEngine;

public abstract class GameService : ScriptableObject
{
    public virtual IEnumerator Run() { yield break; }
    public virtual IEnumerator End() { yield break; }
}