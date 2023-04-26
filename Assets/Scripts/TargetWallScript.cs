using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetWallScript : MonoBehaviour {

    public GameObject ItemToPlaceOnDesk;

    //Locations to Instantiate

    public enum Locations {
        POSITIVEBOUND,
        POSITIVEMIDDLE,
        MIDDLE,
        NEGATIVEMIDDLE,
        NEGATIVEBOUND
    }

    [SerializeField]
    private Locations surfaceXLocation;

    [SerializeField]
    private Locations surfaceYLocation;

    [SerializeField]
    private float scaleFactor;

    [Tooltip("Add a cube on a desk")]

    protected OVRSceneManager SceneManager { get; private set; }
    // Start is called before the first frame update
    void Start() {
        SceneManager = GetComponent<OVRSceneManager>();

        SceneManager.SceneModelLoadedSuccessfully += TargetWallSetup;
    }

    void TargetWallSetup() {
        OVRSceneAnchor[] sceneAnchors = FindObjectsOfType<OVRSceneAnchor>();
        //OVRSceneVolume Object ;

        //initialize game code

        for (int i = 0; i < sceneAnchors.Length; i++) {

            OVRSemanticClassification label = sceneAnchors[i].GetComponent<OVRSemanticClassification>();

            if (label != null) {
                if (label.Contains(OVRSceneManager.Classification.WallFace)) {
                    Debug.Log("this is a Wall");
                    GameObject spawn = Instantiate(ItemToPlaceOnDesk, sceneAnchors[i].transform, false);
                    //spawn.transform.position = new Vector3(spawn.transform.position.x + 5, spawn.transform.position.y, spawn.transform.position.z);
                        //transform.position = new Vector3(spawn.transform.position.x, spawn.transform.position.y v, spawn.transform.position.z);
                }
            }
        }
    }
}