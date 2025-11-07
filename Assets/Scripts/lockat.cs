using UnityEngine;

public class lockat : MonoBehaviour
{
    public GameObject camara;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(camara.transform.position);
    }
}
