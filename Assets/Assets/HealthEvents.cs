using UnityEngine;

public enum HealType
{
    Damage, Heal
}

public class HealthEvents : MonoBehaviour
{
    public HealType type;
    public float amount=10;
    public PlayerStats player;

    void Start()
    {
        switch (type)
        {
            case HealType.Damage: GetComponent<MeshRenderer>().material.color = Color.red; break;
            case HealType.Heal: GetComponent<MeshRenderer>().material.color = Color.green; break;
            default: break;
        }
    }


    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            switch (type)
            {
                case HealType.Damage: player.DamageCharacter(amount); break;
                case HealType.Heal: player.HealCharacter(amount); break;
                default: break;
            }
            Destroy(gameObject);
        }
    }
}
