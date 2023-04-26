using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordThrow : MonoBehaviour
{
    public AudioClip discAudio;

    public GameObject wallSavePrefab;
    public GameObject wallBreakPrefab;

    public LayerMask layerMask;

    public Transform headTransform;

    public float throwForceAdjust;

    private bool _thrown;
    private Rigidbody _rb;
    private Spinner _spinner;
    private RecordTaker _taker;

    public bool Thrown()
    {
        return _thrown;
    }

    public void Take()
    {
        if (_taker)
        {
            _taker.PlaySong(null);
        }
    }

    public void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _spinner = GetComponent<Spinner>();
    }

    public void ThrowDelay()
    {
        Invoke("ThrowForce", .025f);
    }

    [ContextMenu("Editor Throw")]
    public void ThrowForce()
    {
        RecordTaker[] takers = FindObjectsOfType<RecordTaker>();
        bool play = false;

        if (takers != null)
        {
            RecordTaker closestTaker = null;
            float closestDist = 100f;

            foreach (RecordTaker taker in takers)
            {
                float dist = Vector3.Distance(taker.gameObject.transform.position, transform.position);
                if (dist < closestDist && dist < .15f)
                {
                    closestTaker = taker;
                    closestDist = dist;
                }
            }

            if (closestTaker != null)
            {
                _spinner.spin = true;
                _spinner.rotateSpeed = .05f;

                transform.rotation = Quaternion.identity;
                transform.position = closestTaker.gameObject.transform.position;

                _taker = closestTaker;

                _taker.PlaySong(discAudio);

                play = true;
            }
        }

        if (!play)
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

                    Spinner _spinner = GetComponent<Spinner>();
                    _spinner.spin = true;
                    _spinner.rotateSpeed = 1f;

                    _thrown = true;
                }

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
