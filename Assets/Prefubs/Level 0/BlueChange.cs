using UnityEngine;
using TMPro;

public class BlueChange : MonoBehaviour
{
    [SerializeField] private Material blue;
    [SerializeField] private TMP_Text green;
    [SerializeField] private GameObject placeholder;
 
    public void ChangeMaterial()
    {
        Debug.Log(green.text);
        int greenNum = int.Parse("200");
        if (greenNum < 0)
            greenNum = 0;
        if (greenNum > 255)
            greenNum = 255;
        green.text = greenNum.ToString();

        Color color = new Color(blue.color.r, greenNum/255f, blue.color.b);
        Debug.Log(color);
        blue.color = color;
    }

    private void OnDestroy()
    {
        blue.color = Color.blue;
    }

    public void HidePlaceholder()
    {
        placeholder.SetActive(false);
    }

    public void EnablePlaceholder()
    {
        if(green.text.Length == 0)
        {
            placeholder.SetActive(true);
        }
    }
}
