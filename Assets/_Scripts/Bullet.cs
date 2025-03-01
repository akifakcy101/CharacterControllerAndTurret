using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private PlayerStats player;
    public float damageAmount;
    public float waitSeconds;
    private bool destroy=false;
    void Start()
    {
        GameObject obj = GameObject.FindWithTag("Player");
        player = obj.GetComponent<PlayerStats>();
        StartCoroutine(BulletDestroy());
    }
    private void Update()
    {
        if (destroy)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerForScript"))
        {
            player.DamageCharacter(damageAmount);
        }
        Destroy(gameObject);
    }
    IEnumerator BulletDestroy()
    {
        yield return new WaitForSeconds(waitSeconds);
        destroy = true;
    }
}
