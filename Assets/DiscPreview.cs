using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiscPreview : MonoBehaviour
{
    public static DiscPreview Instance;

    public Image discPreview;

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
}
