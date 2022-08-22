using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Status/HealthStatusData")]
public class HealthData : EntityData {

    [SerializeField] private float maxHealth;
    public float MaxHealth { get => maxHealth; }

}
