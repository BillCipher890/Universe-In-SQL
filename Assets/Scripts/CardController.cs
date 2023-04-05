using TMPro;
using UnityEngine;

public class CardController : MonoBehaviour
{
    public enum CardType
    {
        Action,
        Table,
        Element
    }
    public CardType currentCardType;

    public string text;

    public TMP_Text TMP_Text;
    public MeshRenderer mesh;

    public Material actionMaterial;
    public Material tableMaterial;
    public Material elementMaterial;

    // Start is called before the first frame update
    void Start()
    {
        TMP_Text.text = text;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentCardType.Equals(CardType.Action))
        {
            mesh.material = actionMaterial;
        }
        else if (currentCardType.Equals(CardType.Table))
        {
            mesh.material = tableMaterial;
        }
        else if (currentCardType.Equals(CardType.Element))
        {
            mesh.material = elementMaterial;
        }
        else
        {
            Debug.LogError("Not find currentCardType");
        }
    }
}
