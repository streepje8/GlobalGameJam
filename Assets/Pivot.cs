using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pivot : MonoBehaviour
{
    public Vector3 openPos = new Vector3(0, 90, 0);
    public bool isOpen;
    public float speed;

    void Update()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, isOpen ? Quaternion.Euler(openPos) : Quaternion.Euler(0, 0, 0), speed * Time.deltaTime);
    }
}
