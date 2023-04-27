using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallHandler : MonoBehaviour
{
    public GameObject thumbsUp, thumbsDown;

    public bool goodWall;

    public void SetWallSave()
    {
        goodWall = true;
    }

    public void SetWallDestroy()
    {
        thumbsUp.SetActive(false);
        thumbsDown.SetActive(true);

        gameObject.tag = "Untagged";
    }
}
