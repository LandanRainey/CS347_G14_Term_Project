using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PopUp : MonoBehaviour
{

    public GameObject upgradeNode; 

    private void Awake()
    {
        EventSystem.current.SetSelectedGameObject(upgradeNode);
    }

    private void OnMouseUpAsButton()
    {
        if(upgradeNode.activeInHierarchy == false)
        {
            upgradeNode.SetActive(true);
        }
        else
        {
            upgradeNode.SetActive(false);
        }
    }

    public void OnDeselect(BaseEventData eventData)
    {
        upgradeNode.SetActive(false);
    }

        

}
