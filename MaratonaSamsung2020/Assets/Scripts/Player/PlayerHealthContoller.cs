public class PlayerHealthContoller : IHealthController {

    private HealthData healthData;

    private float MaxHealth { get => healthData.MaxHealth; }

    private float health;
    public override float Health {

        get => health;

    }

    public FloatEvent playerHealthChangedEvent;
    public GameOverEvent gameOverEvent;

    private void Awake() {

        healthData = GetComponent<HealthStatus>().Status;

    }

    private void Start() {

        health = MaxHealth;
        
    }

    public override void RecieveDamage(float damage) {

        health -= damage;
        playerHealthChangedEvent.Raise(Health);

        if (Health <= 0) {

            gameOverEvent.Raise();

        }

    }
    
}
