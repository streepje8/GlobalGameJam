using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pivot : MonoBehaviour
{

       
    public bool isOpen;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        isOpen = true;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, isOpen ? Quaternion.Euler(0, 90, 0) : Quaternion.Euler(0, 0, 0), speed * Time.deltaTime);
    }
}
