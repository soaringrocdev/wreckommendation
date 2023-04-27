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
        GameObject record = Instantiate(recordPrefab, transform.position, transform.rotation);
        RecordThrow recordThrower = record.GetComponent<RecordThrow>();
        recordThrower.discAudio = song;

        //TOOD assign cover art

        gameObject.SetActive(false);
    }
}
