using UnityEngine;

public class CollisionCube : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.right * Time.deltaTime * 2;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Green"))
        {
            other.gameObject.GetComponent<MeshRenderer>().material.color = Color.green;
        }
        else if (other.CompareTag("Blue"))
        {
            other.gameObject.GetComponent<MeshRenderer>().material.color = Color.blue;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        Destroy(other.gameObject);
    }
}
