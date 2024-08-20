using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop : MonoBehaviour
{
    MeshRenderer renderer; 
    Rigidbody rigidbody;
    [SerializeField] float w_time = 5f;
    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<MeshRenderer>();
        rigidbody = GetComponent<Rigidbody>();
        w_time = 0;
        renderer.enabled = false;
        rigidbody.useGravity = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > w_time)
        {
            renderer.enabled = true;
            rigidbody.useGravity = true;
        }

    }
}
 