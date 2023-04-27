using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingSetup : MonoBehaviour
{
    public Transform rotRef, needle;

    public RecordThrow record;
    private void Start()
    {
        Invoke("Setup", 2.25f);
    }

    void Setup()
    {
        needle.rotation = rotRef.rotation;

        record.ThrowForce();
    }
}
