using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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

    public void Saved(WallHandler wall)
    {
        if (!_started && wall)
        {
            _started = true;

            WallHandler[] walls = FindObjectsOfType<WallHandler>();

            if (walls != null)
            {
                foreach(WallHandler waller in walls)
                {
                    if (!waller.goodWall)
                    {
                        waller.SetWallDestroy();
                    }
                }
            }

            SpawnDiscs();
        }

        CheckDiscs();
    }

    public void Smashed()
    {
        if (!_started)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        CheckDiscs();
    }
    
    public void CheckDiscs()
    {
        if (_started)
        {
            RecordThrow[] records = FindObjectsOfType<RecordThrow>(false);
            int numAvailable = 0;

            if (records != null)
            {
                foreach (RecordThrow record in records)
                {
                    if (!record.stuck)
                    {
                        numAvailable++;
                    }
                }
            }

            if (numAvailable == 0)
            {
                //SpawnDiscs();
            }
        }
    }

    [ContextMenu("Spawn")]
    public void SpawnDiscs()
    {
        GameObject discs = Instantiate(introStuff, introStuff.transform.position, introStuff.transform.rotation);
        discs.SetActive(true);
    }
}
