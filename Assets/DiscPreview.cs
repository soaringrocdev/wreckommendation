using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiscPreview : MonoBehaviour
{
    public static DiscPreview Instance;

    public GameObject introStuff;

    public Image discPreview;

    private bool _started;

    private void Start()
    {
        if (Instance != null)
        {
            //Destroy the previous instance.
            Destroy(Instance);
        }

        //Set the new instance to this object.
        Instance = this;
    }

    public void Preview(Sprite sprite)
    {
        discPreview.sprite = sprite;
    }

    public void Smashed()
    {
        if (!_started)
        {
            _started = true;

            SpawnDiscs();
        }
    }

    public void SpawnDiscs()
    {
        GameObject discs = Instantiate(introStuff);
        discs.SetActive(true);
    }
}
