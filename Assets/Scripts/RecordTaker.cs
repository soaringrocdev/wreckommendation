using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordTaker : MonoBehaviour
{
    public AudioSource playerAudio;

    public Transform needle;
    public float needleAngle, needleAngleRequired;
    public bool xrequire, yrequire, zrequire;

    private bool canPlay;

    public void PlaySong(AudioClip clip)
    {
        playerAudio.clip = clip;
    }

    private void Update()
    {
        if (xrequire)
        {
            if (needle.localEulerAngles.x > needleAngleRequired)
            {
                canPlay = true;
            }
            else
            {
                canPlay = false;
            }
        }

        if (yrequire)
        {
            if (needle.localEulerAngles.y > needleAngleRequired)
            {
                canPlay = true;
            }
            else
            {
                canPlay = false;
            }
        }

        if (zrequire)
        {
            if (needle.localEulerAngles.z > needleAngleRequired)
            {
                canPlay = true;
            }
            else
            {
                canPlay = false;
            }
        }

        if (canPlay && !playerAudio.isPlaying && playerAudio.clip != null)
        {
            playerAudio.Play();
        }

        if (playerAudio.clip == null && playerAudio.isPlaying)
        {
            playerAudio.Stop();
        }

        if (!canPlay)
        {
            playerAudio.Stop();
        }
    }
}
