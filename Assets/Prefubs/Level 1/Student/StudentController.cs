using TMPro;
using UnityEngine;

//Изменяет объект или его поведение исходя из установок level controller
public class StudentController : MonoBehaviour
{
    [SerializeField] private string fullName;
    public string engFullName;
    public bool isShowNameCurrent = false;
    private bool isShowNameLastFrame = false;

    public bool selectedCurrent = false;
    private bool selectedLastFrame = false;

    public enum Anim
    {
        Relaxing,
        Eating,
        Happy
    }

    void Start()
    {
        fillRandomName();
    }

    private void fillRandomName()
    {
        string[] firstNames = new string[] { "Гарри", "Оливер", "Джек", "Чарли", "Томас", "Амелия", "Оливия", "Джессика", "Эмили", "Лили" };
        string[] secondNames = new string[] { "Андерсон", "Блэк", "Бредшоу", "Честертон", "Дикинсон", "Эванс", "Фостер", "Гилберт", "Кэндал", "МакАдам" };
        if (fullName == "")
        {
            int fName = Random.Range(0, firstNames.Length);
            int sName = Random.Range(0, secondNames.Length);
            fullName = firstNames[fName] + " " + secondNames[sName];
        }
        TMP_Text nameField = transform.Find("Canvas").Find("Name").GetComponent<TMP_Text>();
        nameField.text = fullName.Replace(' ', '\n');
    }


    void Update()
    {
        ShowNameControll();
        OutlineControll();
    }

    private void ShowNameControll()
    {
        if (isShowNameCurrent != isShowNameLastFrame)
        {
            transform.Find("Canvas").gameObject.SetActive(isShowNameCurrent);
        }
        isShowNameLastFrame = isShowNameCurrent;
    }

    private void OutlineControll()
    {
        if (selectedCurrent != selectedLastFrame)
        {
            if (selectedCurrent)
            {
                transform.GetComponent<Outline>().OutlineWidth = 5;
            }
            else
            {
                transform.GetComponent<Outline>().OutlineWidth = 0;
            }
        }
        selectedLastFrame = selectedCurrent;
    }
}
