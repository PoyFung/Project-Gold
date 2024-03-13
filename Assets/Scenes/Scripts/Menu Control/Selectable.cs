using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Selectable : MonoBehaviour
{
    public GameObject raceScreen;

    private Transform highlight;
    private Transform selection;
    private RaycastHit mouseInfo;
    private bool isSelectable = false;

    // Update is called once per frame
    void Update()
    {
        if (highlight!=null && !isSelectable)
        {

            if (highlight.gameObject.GetComponent<Outline>() != null)
            {
                highlight.gameObject.GetComponent<Outline>().enabled = false;
            }
            highlight = null;
        }

        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (!EventSystem.current.IsPointerOverGameObject() && Physics.Raycast(mouseRay, out mouseInfo))
        {
            highlight = mouseInfo.transform;
            if (highlight.CompareTag("Selectable"))
            {
                isSelectable = true;
                if (highlight.gameObject.GetComponent<Outline>() != null)
                {
                    highlight.gameObject.GetComponent<Outline>().enabled = true;
                }

                else
                {
                    Outline outline = highlight.gameObject.AddComponent<Outline>();
                    outline.enabled = true;
                }

                if (Input.GetMouseButtonDown(0))
                {
                    Debug.Log("Clicked!");
                    raceScreen.SetActive(true);
                    Time.timeScale = 0;
                }
            }
        }
        isSelectable = false;
    }
}
