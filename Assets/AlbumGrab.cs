using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlbumGrab : MonoBehaviour
{
    public GameObject recordPrefab;

    public AudioClip song;
    public Image coverArt;

    private Grabbable grabbable;

    private void Start()
    {
        grabbable = GetComponent<Grabbable>();
    }

    public void Grabbed()
    {
        //GameObject Instantiate

        gameObject.SetActive(false);
    }
}
