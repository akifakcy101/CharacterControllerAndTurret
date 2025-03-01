using NUnit.Framework;
using System.Collections;
using System.IO;
using TMPro;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Rendering; //List bunu koyunca düzeldi

public class PlayerStats : MonoBehaviour
{
    public int coin;
    public TextMeshProUGUI coinAmountUI;

    [Header("Gun")]
    public GameObject bullet;
    public GameObject bulletSpawn;
    public float bulletSpeed = 10f;
    public float fireRate = 0.5f;
    private bool canShoot = true;

    public float characterHealth = 100;
    public List<KeyType> KeyList;
    public GameObject RedKeyIco;
    public GameObject GreenKeyIco;
    public GameObject BlueKeyIco;
    public GameObject ErrorText;
    public GameObject HealthAmount;

    private void Start()
    {
        HealthAmount.GetComponent<TextMeshProUGUI>().text = characterHealth.ToString();
    }

    void Update()
    {
        if (Input.GetMouseButton(0) && canShoot == true)
        {
            StartCoroutine(Shoot());
        }
    }

    IEnumerator Shoot()
    {
        canShoot = false;
        GameObject bullets = Instantiate(bullet, bulletSpawn.transform.position, bulletSpawn.transform.rotation * Quaternion.Euler(90, 0, 0));
        bullets.GetComponent<Rigidbody>().AddForce(bulletSpawn.transform.forward * bulletSpeed, ForceMode.Impulse);
        yield return new WaitForSeconds(fireRate);
        canShoot = true;
    }

    public void AddCoin()
    {
        coin++;
        coinAmountUI.text = coin.ToString();
    }

    public void AddKeys(KeyType type)
    {
        KeyList.Add(type);
        switch (type)
        {
            case KeyType.Red: RedKeyIco.SetActive(true); break;
            case KeyType.Green: GreenKeyIco.SetActive(true); break;
            case KeyType.Blue: BlueKeyIco.SetActive(true); break;
        }
    }

    public void HealCharacter(float healAmount)
    {
        characterHealth += healAmount;
        characterHealth = Mathf.Min(characterHealth, 100);
        HealthAmount.GetComponent<TextMeshProUGUI>().text = characterHealth.ToString();
    }

    public void DamageCharacter(float damageAmount)
    {
        characterHealth -= damageAmount;
        characterHealth = Mathf.Max(characterHealth, 0);
        HealthAmount.GetComponent<TextMeshProUGUI>().text = characterHealth.ToString();
    }
}
