using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : MonoBehaviour
{

    public float thrust = 100;
    public Rigidbody rb;

    [Header("Set in Inspector")]
    public static float bottomY = -20f;
    public Material fastAppleMaterial;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (this.tag == "FastApple")
        {
            GetComponent<Renderer>().material = fastAppleMaterial;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < bottomY)
        {
            Destroy(this.gameObject);

            // Get a reference of the ApplePicker component of Main Camera
            ApplePicker apScript = Camera.main.GetComponent<ApplePicker>();
            // Call the public AppleDestroyed() method of apScript
            apScript.AppleDestroyed();
        }
    }

    void FixedUpdate()
    {
        if (this.tag == "FastApple")
        {
            // Increase gravity for FastApples
            rb.AddForce(-transform.up * thrust);
        }
    }
}
