using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordThrow : MonoBehaviour
{
    public LayerMask layerMask;

    public Transform headTransform;

    public float throwForceAdjust;

    private Rigidbody _rb;

    public void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public void ThrowDelay()
    {
        Invoke("ThrowForce", .025f);
    }

    [ContextMenu("Editor Throw")]
    public void ThrowForce()
    {
        if (_rb)
        {
            //_rb.Sleep();

            RaycastHit hit;
            
            if (Physics.Raycast(headTransform.position, headTransform.forward, out hit, Mathf.Infinity, layerMask))
            {
                Vector3 hitDir = hit.point - transform.position;

                //TODO: Add force adjustment based on wrist speed

                _rb.AddForce(hitDir * throwForceAdjust, ForceMode.Impulse);
            }

        }
    }
}
