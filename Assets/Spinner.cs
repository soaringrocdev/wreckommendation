using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour
{
    public float rotateSpeed;
    public bool spin;

    [ContextMenu("Spin!")]
    public void SetSpin()
    {
        spin = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (spin)
        {
            transform.RotateAroundLocal(new Vector3(0, 1, 0), rotateSpeed);
        }
    }
}
