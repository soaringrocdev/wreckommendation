using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public GameObject uiholder;

public class UIHandler : MonoBehaviour
{

public class ClickAction : MonoBehaviour, IPointerClickHandler {
        public void OnPointerClick(PointerEventData eventData) {
            if (this.transform.name = "Winning1") {
                uiholder = gameObject.Find("Winning2");
                this.gameObject.SetActive(false);
                uiholder.setActive(true);
            } else {
                this.gameObject.SetActive(false);
            }
        }
    }
}
