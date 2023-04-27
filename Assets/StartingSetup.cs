using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingSetup : MonoBehaviour
{
    public Transform lid, needle;

    public RecordThrow record;
    private void Start()
    {
        Invoke("Setup", 2.25f);
    }

    void Setup()
    {
        record.ThrowForce();
    }
}
