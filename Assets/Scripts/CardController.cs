using TMPro;
using UnityEngine;

public class CardController : MonoBehaviour
{
    public enum CardType
    {
        Action,
        Table,
        Element,
        Condition,
        Deactivated
    }
    public CardType currentCardType;

    public string engName;
    public string rusName;

    public TMP_Text TMP_Text_EngName;
    public TMP_Text TMP_Text_RusName;
    public MeshRenderer mesh;

    public Material actionMaterial;
    public Material tableMaterial;
    public Material elementMaterial;
    public Material conditionMaterial;
    public Material deactivatedMaterial;

    public CardData cardData;

    // Start is called before the first frame update
    void Start()
    {
        TMP_Text_EngName.text = engName;
        TMP_Text_RusName.text = rusName;
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
        else if (currentCardType.Equals(CardType.Condition))
        {
            mesh.material = conditionMaterial;
        }
        else if (currentCardType.Equals(CardType.Deactivated))
        {
            mesh.material = deactivatedMaterial;
        }
        else
        {
            Debug.LogError("Not find currentCardType");
        }
    }
}
