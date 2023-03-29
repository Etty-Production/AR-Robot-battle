using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public GameObject Camera;
    // Update is called once per frame
    void Update()
    {
        transform.LookAt(Camera.transform, Vector3.up);
        transform.rotation = new Quaternion(0.0f, transform.rotation.y, 0.0f, transform.rotation.w);
    }
}