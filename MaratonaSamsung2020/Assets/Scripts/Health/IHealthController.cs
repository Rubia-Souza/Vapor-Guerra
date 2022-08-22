using UnityEngine;

public abstract class IHealthController : MonoBehaviour {

    public abstract float Health { get; }

    public abstract void RecieveDamage(float value);


}
