using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurfaceAnchorScript : MonoBehaviour {

    public GameObject ItemToPlaceOnDesk;

    //Locations to Instantiate


    [SerializeField]
    private float surfaceXLocation;

    [SerializeField]
    private float surfaceYLocation;

    [SerializeField]
    private float surfaceZLocation;


    [Tooltip("Add a cube on a desk")]

    protected OVRSceneManager SceneManager { get; private set; }
    // Start is called before the first frame update
    void Start() {
        SceneManager = GetComponent<OVRSceneManager>();

        SceneManager.SceneModelLoadedSuccessfully += TableTopFindandRelocate;
    }

    void TableTopFindandRelocate() {
        OVRSceneAnchor[] sceneAnchors = FindObjectsOfType<OVRSceneAnchor>();
        OVRSceneVolume Object ;

        //initialize game code

        for (int i = 0; i < sceneAnchors.Length; i++) {

            OVRSemanticClassification label = sceneAnchors[i].GetComponent<OVRSemanticClassification>();

            if (label != null) {
                if (label.Contains(OVRSceneManager.Classification.Desk)) {
                    Debug.Log("this is a desk");
                    GameObject spawn = Instantiate(ItemToPlaceOnDesk, sceneAnchors[i].transform, false);
                    spawn.transform.position = new Vector3(spawn.transform.position.x + surfaceXLocation, spawn.transform.position.y + surfaceYLocation, spawn.transform.position.z + surfaceZLocation);
                        //transform.position = new Vector3(spawn.transform.position.x, spawn.transform.position.y v, spawn.transform.position.z);
                }
            }   
        }
    }
}