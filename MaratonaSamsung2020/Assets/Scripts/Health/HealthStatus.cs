using UnityEngine;

public class HealthStatus : IStatus<HealthData> {

    [SerializeField] private HealthData healthStatus;
    public override HealthData Status { get => healthStatus; }

}
