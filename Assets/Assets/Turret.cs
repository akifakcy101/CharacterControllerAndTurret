using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class Turret : MonoBehaviour
{
    [Header("Turret Gun")]
    public GameObject bullet;
    public GameObject bulletSpawn;
    public float bulletSpeed = 10f;
    public float fireRate;
    public bool canShoot = false;
    public float raycastRange = 10f;

    [Header("Turret Settings")]
    public float maxAngle = 60f;
    public float rotateSpeed = 5;
    public float wait = 0.5f;
    public bool rightDirection = true;
    public GameObject rotator;

    [Header("Turret Actions")]
    public bool isSearching = true;
    public bool canEscape = false;
    public bool isWaiting = false;
    public float currentRotation;
    public Transform target;

    void Start()
    {

    }


    void Update()
    {
        currentRotation = rotator.transform.eulerAngles.y;
        if (currentRotation > 180)
        {
            currentRotation -= 360;
        }
        //Debug.Log(currentRotation);


        RaycastHit hit;
        if (Physics.Raycast(bulletSpawn.transform.position, -bulletSpawn.transform.forward, out hit, raycastRange))
        {
            if (hit.collider.CompareTag("PlayerForScript"))
            {
                target = hit.collider.transform;
                canEscape = true;
            }
        }
        else
        {
            target = null;
        }


        if (target == null)
        {
            if (canEscape)
            {
                Debug.Log("a");
                canEscape = false;
            }

            if (isWaiting == false)
            {
                if (currentRotation >= 60f && isWaiting == false)
                {
                    StartCoroutine(Wait(wait));
                    rightDirection = false;
                }
                else if (currentRotation <= -60f && isWaiting == false)
                {
                    StartCoroutine(Wait(wait));
                    rightDirection = true;
                }

                if (rightDirection)
                {
                    rotator.transform.localRotation = Quaternion.RotateTowards(rotator.transform.localRotation, Quaternion.Euler(0, maxAngle, 0), Time.deltaTime * rotateSpeed);
                }
                else
                {
                    rotator.transform.localRotation = Quaternion.RotateTowards(rotator.transform.localRotation, Quaternion.Euler(0, -maxAngle, 0), Time.deltaTime * rotateSpeed);
                }
            }
        }


        else//Player Found
        {
            var targetRotation = Quaternion.LookRotation(-1 * (target.position - rotator.transform.position)).eulerAngles.y;
            if (!(currentRotation >= 59f) && !(currentRotation <= -59f))
            {
                rotator.transform.localRotation = Quaternion.RotateTowards(rotator.transform.localRotation, Quaternion.Euler(0, targetRotation, 0), Time.deltaTime * rotateSpeed);
            }
        }



        if (canShoot)
        {
            StartCoroutine(Shoot());
        }

    }

    IEnumerator Shoot()
    {
        canShoot = false;
        GameObject bullets = Instantiate(bullet, bulletSpawn.transform.position, bulletSpawn.transform.rotation * Quaternion.Euler(90, 0, 0));
        bullets.GetComponent<Rigidbody>().AddForce(-bulletSpawn.transform.forward * bulletSpeed, ForceMode.Impulse);
        yield return new WaitForSeconds(fireRate);
        canShoot = true;
    }

    IEnumerator Wait(float wait)
    {
        isWaiting = true;
        yield return new WaitForSeconds(wait);
        isWaiting = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(bulletSpawn.transform.position, -bulletSpawn.transform.forward * raycastRange);
    }
}
