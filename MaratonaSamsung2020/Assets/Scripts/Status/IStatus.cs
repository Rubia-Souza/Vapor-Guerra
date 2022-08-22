using UnityEngine;

public abstract class IStatus<T> : MonoBehaviour where T : EntityData {
    
    public abstract T Status { get; }

}
