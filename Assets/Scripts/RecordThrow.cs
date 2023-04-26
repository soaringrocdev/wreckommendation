using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordThrow : MonoBehaviour
{
    public GameObject wallSavePrefab;
    public GameObject wallBreakPrefab;

    public LayerMask layerMask;

    public Transform headTransform;

    public float throwForceAdjust;

    private bool _thrown;
    private Rigidbody _rb;

    public bool Thrown()
    {
        return _thrown;
    }

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
        if (!headTransform)
        {
            headTransform = Camera.main.transform;
        }

        if (_rb && headTransform && !_thrown)
        {
            //_rb.Sleep();

            RaycastHit hit;
            
            if (Physics.Raycast(headTransform.position, headTransform.forward, out hit, Mathf.Infinity, layerMask))
            {
                Vector3 hitDir = hit.point - transform.position;

                //TODO: Add force adjustment based on wrist speed

                _rb.AddForce(hitDir * throwForceAdjust, ForceMode.Impulse);

                _thrown = true;
            }

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_thrown)
        {
            if (collision.gameObject.tag == "SaveWall")
            {
                GameObject saveDisc = Instantiate(wallSavePrefab, transform.position, transform.rotation);

                Vector3 wallPos = collision.collider.ClosestPoint(transform.position);

                saveDisc.transform.position = wallPos;

                gameObject.SetActive(false);
            }
            else
            {
                GameObject breakDisc = Instantiate(wallBreakPrefab, transform.position, transform.rotation);

                Rigidbody[] rbs = breakDisc.GetComponentsInChildren<Rigidbody>();

                foreach(Rigidbody rb in rbs)
                {
                    rb.AddExplosionForce(.75f, transform.position, 1f, .075f, ForceMode.Impulse);
                }

                gameObject.SetActive(false);
            }
        }
    }
}
