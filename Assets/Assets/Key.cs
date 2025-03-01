using UnityEngine;

public enum KeyType
{
    Red, Green, Blue
}
public class Key : MonoBehaviour
{ 
    public KeyType Keytype;
    private Color keycolor;

    void Start()
    {
        MeshRenderer[] meshRenderers = GetComponentsInChildren<MeshRenderer>();
        switch (Keytype)
        {
            case KeyType.Red: keycolor = Color.red; break;
            case KeyType.Green: keycolor = Color.green; break;
            case KeyType.Blue: keycolor = Color.blue; break;
            default: break;
        }

        foreach (MeshRenderer mesh in meshRenderers)
        {
            mesh.material.color = keycolor;
        }
    }

    void Update()
    {

    }
}
