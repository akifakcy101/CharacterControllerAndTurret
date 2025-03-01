using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using TMPro;
using System;

public class LockedDoor : MonoBehaviour
{
    public bool isOpen = false;
    public GameObject pivot;
    private Vector3 startRotation;
    public Vector3 endRotation;

    public GameObject door;
    public GameObject player;
    public KeyType doorKey;
    int x=0;

    void Start()
    {
        startRotation = pivot.transform.localRotation.eulerAngles;
        switch (doorKey)
        {
            case KeyType.Red: door.GetComponent<MeshRenderer>().material.color = Color.red; break;
            case KeyType.Green: door.GetComponent<MeshRenderer>().material.color = Color.green; break;
            case KeyType.Blue: door.GetComponent<MeshRenderer>().material.color = Color.blue; break;
        }
    }

    void Update()
    {
        float rotationCompare = Mathf.DeltaAngle(player.transform.localRotation.eulerAngles.y, transform.localRotation.eulerAngles.y);

        if (rotationCompare < 90 && rotationCompare > -90)
        {
            x = 1;
        }
        else
        {
            x = -1;
        }
        // Debug.Log(rotationCompare + "  " + x);


        if (isOpen)
        {
            pivot.transform.localRotation = Quaternion.Slerp(pivot.transform.localRotation, Quaternion.Euler(x * endRotation), Time.deltaTime * 5);
        }
        else
        {
            pivot.transform.localRotation = Quaternion.Slerp(pivot.transform.localRotation, Quaternion.Euler(x * startRotation), Time.deltaTime * 5);
        }
    }

    private void OnTriggerEnter(Collider other)
    {


        if (other.CompareTag("Player"))
        {
            foreach (KeyType type in other.GetComponent<PlayerStats>().KeyList)
            {
                if (type == doorKey)
                {
                    isOpen = true;
                    break;
                }
            }
            if (isOpen == false)
            {
                GameObject message = other.GetComponent<PlayerStats>().ErrorText;
                message.SetActive(true);
                message.GetComponent<TextMeshProUGUI>().text = "You Need " + doorKey.ToString() + " Key";
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerStats>().ErrorText.SetActive(false);
            isOpen = false;

        }
    }
}
