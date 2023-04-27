using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIHandler : MonoBehaviour
{

public class ClickAction : MonoBehaviour, IPointerClickHandler {
    public void OnPointerClick(PointerEventData eventData) {
            Destroy(this);
    }
}
}
