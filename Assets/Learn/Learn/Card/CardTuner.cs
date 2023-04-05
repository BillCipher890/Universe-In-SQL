using TMPro;
using UnityEngine;

public class CardTuner : MonoBehaviour
{
    /// <summary>
    /// Card type. 1 = Action. 2 = Table. 3 = Element.
    /// </summary>
    public int cardType;

    public string text;

    public TMP_Text TMPtext;
    public MeshRenderer mesh;

    public Material actionMaterial;
    public Material elementMaterial;
    public Material tableMaterial;

    void Start()
    {
        TMPtext.text = text;
        if(cardType == 1)
        {
            mesh.material = actionMaterial;
        }
        else if(cardType == 2)
        {
            mesh.material = tableMaterial;
        }
        else if( cardType == 3)
        {
            mesh.material = elementMaterial;
        }
    }

    /*void setSelect()
    {
        mesh.material = actionMaterial;
        text.text = "SELECT\n¬€¡–¿“‹";
    }*/

    void setTable(string tableName)
    {
        mesh.material = tableMaterial;
        TMPtext.text = tableName;
    }

    void setElement(string elementName)
    {
        mesh.material = elementMaterial;
        TMPtext.text = elementName;
    }
}
