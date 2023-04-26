using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetWallScript : MonoBehaviour {

    public GameObject ItemToPlaceOnWall;

    //Locations to Instantiate


    [SerializeField]
    private float surfaceXLocation;

    [SerializeField]
    private float surfaceYLocation;

    [SerializeField]
    private float surfaceZLocation;



    [Tooltip("Add a cube on a wall")]

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
                    GameObject spawn = Instantiate(ItemToPlaceOnWall, sceneAnchors[i].transform, false);
                    spawn.transform.position = new Vector3(spawn.transform.position.x + surfaceXLocation, spawn.transform.position.y + surfaceYLocation, spawn.transform.position.z + surfaceZLocation);
                    //transform.position = new Vector3(spawn.transform.position.x, spawn.transform.position.y v, spawn.transform.position.z);
                }
            }
        }
    }
}