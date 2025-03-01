using JetBrains.Annotations;
using NUnit.Framework;
using UnityEngine;

public class CharacterTrigger : MonoBehaviour
{
    private PlayerStats player;
    void Start()
    {
        player = transform.parent.GetComponent<PlayerStats>();
    }

    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Cyan"))
        {
            other.gameObject.GetComponent<MeshRenderer>().material.color = Color.cyan;
        }
        if (other.CompareTag("Coin"))
        {
            player.AddCoin();
            Destroy(other.gameObject);
        }
        if (other.CompareTag("Key"))
        {
            KeyType key=other.GetComponent<Key>().Keytype;
            player.AddKeys(key);
            Destroy(other.gameObject);

        }
        
    }
}
