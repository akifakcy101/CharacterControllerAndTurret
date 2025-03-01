using UnityEngine;

public class Door : MonoBehaviour
{
    public bool isOpen = false;
    public GameObject pivot;
    private Vector3 startRotation;
    public Vector3 endRotation;
    void Start()
    {
        startRotation = pivot.transform.localRotation.eulerAngles;
    }

    void Update()
    {
        if (isOpen)
        {
            pivot.transform.localRotation = Quaternion.Slerp(pivot.transform.localRotation, Quaternion.Euler(endRotation), Time.deltaTime * 5);
        }
        else
        {
            pivot.transform.localRotation = Quaternion.Slerp(pivot.transform.localRotation, Quaternion.Euler(startRotation), Time.deltaTime * 5);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isOpen = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isOpen = false;
        }
    }
}
