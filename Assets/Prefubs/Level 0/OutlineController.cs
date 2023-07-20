using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OutlineController : MonoBehaviour
{
    public bool outlineBlocked = true;
    [SerializeField] private Transform rowDataConteiner;
    private bool isChoosen = false;

    void Start()
    {
        EventManager.onFigureEndBlinking += outlineBlocketSetFalse;
    }

    private void OnDestroy()
    {
        EventManager.onFigureEndBlinking -= outlineBlocketSetFalse;
    }

    private void OnMouseDown()
    {
        if (!outlineBlocked)
        {
            isChoosen = !isChoosen;
            if (isChoosen)
            {
                GetComponent<Outline>().OutlineWidth = 7;
                for (int i = 0; i < rowDataConteiner.childCount - 1; i++)
                {
                    rowDataConteiner.GetChild(i).GetComponent<Image>().color = Color.yellow;
                }
                rowDataConteiner.GetChild(rowDataConteiner.childCount - 1).GetChild(0).GetComponent<Image>().color = Color.yellow;
                EventManager.sendFigureChoosen();
            }
            else
            {
                GetComponent<Outline>().OutlineWidth = 0;
                for (int i = 0; i < rowDataConteiner.childCount - 1; i++)
                {
                    rowDataConteiner.GetChild(i).GetComponent<Image>().color = Color.white;
                }
                rowDataConteiner.GetChild(rowDataConteiner.childCount - 1).GetChild(0).GetComponent<Image>().color = Color.white;
            }
        }
    }

    private void outlineBlocketSetFalse()
    {
        EventManager.onFigureEndBlinking -= outlineBlocketSetFalse;
        outlineBlocked = false;
    }
}
