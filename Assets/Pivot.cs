using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Pivot : MonoBehaviour
{
    public Vector3 openPos = new Vector3(0, 90, 0);
    public bool isOpen;
    public float speed;
    public NodeEditableObject obj;

    private void Awake()
    {
        obj.OnBuild += (a) =>
        {
            MeshFilter rend = a.GetComponentInChildren<MeshFilter>();
            if (rend != null)
            {
                isOpen = rend.sharedMesh.name.Equals("Doorhandle1", StringComparison.OrdinalIgnoreCase);
            }
        };
    }

    void Update()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, isOpen ? Quaternion.Euler(openPos) : Quaternion.Euler(0, 0, 0), speed * Time.deltaTime);
    }
}
