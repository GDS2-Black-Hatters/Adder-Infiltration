using UnityEngine;

public class PlayerVirus : MonoBehaviour
{
    [field: SerializeField] public PlayerVirusController VirusController { get; private set; }
    [field: SerializeField] public GameObject PlayerVisual { get; private set; }
    [field: SerializeField] public Health HP { get; private set; } = new(20);
    public bool IsProtected { get; private set; } = false;
    private PlayerVirusMoveControl movement;

    [SerializeField] protected AK.Wwise.Event detectedSoundEffect;
    [SerializeField] protected AK.Wwise.Event hurtSoundEffect;
    [SerializeField] protected AK.Wwise.Event lowHealthSoundEffect;
    [SerializeField] protected AK.Wwise.Event deathSoundEffect;

    private bool isOnLowHealth = false;

    private void Awake()
    {
        movement = GetComponent<PlayerVirusMoveControl>();

        HP.onDeath += () =>
        {
            GameManager.LevelManager.OnDeath();
            deathSoundEffect.Post(gameObject);
        };

        HP.onHurt += () => { hurtSoundEffect.Post(gameObject); };
    }

    private void Start()
    {
        GameManager.LevelManager.ActiveSceneController.enemyAdmin.OnFullAlert += () =>
        {
            detectedSoundEffect.Post(gameObject);
        };
    }

    public void Dash(float strength)
    {
        movement.Dash(strength);
    }

    public void Heal(float percentage)
    {
        HP.ReduceHealth(HP.health.originalValue * -percentage);
        CheckDamage();
    }

    public void Damage(float amount)
    {
        if (!IsProtected)
        {
            HP.ReduceHealth(amount);
            CheckDamage();
        }
    }

    public void DamagePercentage(float percentage)
    {
        if (!IsProtected)
        {
            HP.ReduceHealth(HP.health.originalValue * percentage);
            CheckDamage();
        }
    }

    public void CheckDamage()
    {
        if (!isOnLowHealth && HP.healthPercentage <= 0.5f)
        {
            print("Low health");
            isOnLowHealth = true;
            lowHealthSoundEffect.Post(gameObject);
        }
        else if (isOnLowHealth && HP.healthPercentage > 0.5f)
        {
            print("Normal health");
            isOnLowHealth = false;
            lowHealthSoundEffect.Stop(gameObject);
        }
    }

    public void SetProtected(bool protection)
    {
        IsProtected = protection;
    }
}
