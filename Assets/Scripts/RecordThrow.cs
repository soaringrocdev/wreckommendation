using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecordThrow : MonoBehaviour
{
    public AudioClip discAudio;
    public AudioSource thunkSource;
    public Sprite coverArt;
    public Image grabArt, displayArt;

    public AudioClip[] throwClips;
    public AudioClip[] thunkClips;

    public GameObject wallSavePrefab;
    public GameObject wallBreakPrefab;

    public GameObject displayAlbum;
    public GameObject grabAlbum;

    public LayerMask layerMask;

    public Transform headTransform;

    public float throwForceAdjust;

    public bool stuck;

    private bool _thrown;
    private Rigidbody _rb;
    private Spinner _spinner;
    private RecordTaker _taker;
    private AudioSource _throwAudio;

    public bool Thrown()
    {
        return _thrown;
    }

    public void Preview()
    {
        DiscPreview.Instance.Preview(coverArt);
    }

    public void Take()
    {
        _rb.isKinematic = false;

        stuck = false;

        if (grabAlbum)
        {
            grabAlbum.SetActive(false);
        }

        if (displayAlbum)
        {
            displayAlbum.SetActive(false);
        }

        if (_taker)
        {
            _taker.PlaySong(null);

            _taker = null;
        }
    }

    public void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _spinner = GetComponent<Spinner>();
        _throwAudio = GetComponent<AudioSource>();

        if (throwClips != null)
        {
            int index = Random.Range(0, throwClips.Length);
            _throwAudio.clip = throwClips[index];
        }

        grabArt.sprite = coverArt;
        displayArt.sprite = coverArt;
    }

    public void ThrowDelay()
    {
        Invoke("ThrowForce", .025f);
    }

    private void FixedUpdate()
    {
        if (_taker)
        {
            transform.position = _taker.transform.position;
        }
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
                _spinner.rotateSpeed = .25f;

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

                    _throwAudio.Play();

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
                //GameObject saveDisc = Instantiate(wallSavePrefab, transform.position, Quaternion.identity);

                Vector3 wallPos = collision.collider.ClosestPoint(transform.position);

                _spinner.spin = false;

                _rb.isKinematic = true;
                transform.position = wallPos;

                displayAlbum.SetActive(true);

                transform.rotation = Quaternion.identity;

                transform.right = -collision.collider.gameObject.transform.forward;

                if (thunkClips != null)
                {
                    int index = Random.Range(0, thunkClips.Length);
                    thunkSource.clip = thunkClips[index];

                    thunkSource.Play();
                }

                stuck = true;

                WallHandler wall = collision.collider.gameObject.GetComponent<WallHandler>();
                wall.goodWall = true;

                DiscPreview.Instance.Saved(wall);

                _thrown = false;

                //saveDisc.transform.position = wallPos;

                //gameObject.SetActive(false);
            }
            else
            {
                stuck = false;

                GameObject breakDisc = Instantiate(wallBreakPrefab, transform.position, transform.rotation);

                Rigidbody[] rbs = breakDisc.GetComponentsInChildren<Rigidbody>();

                foreach(Rigidbody rb in rbs)
                {
                    rb.AddExplosionForce(.75f, transform.position, 1f, .075f, ForceMode.Impulse);
                }

                DiscPreview.Instance.Smashed();

                gameObject.SetActive(false);
            }
        }
    }
}
