using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelControllerLevel0 : MonoBehaviour
{
    //Номер в диалоговой системе
    public int[] questPoints;
    public Transform[] figures;

    public string[] texts;
    private TMP_Text Task;
    private int taskPointer = 0;
    private int questPointer = 0;
    private int currentMaxPointer = 0;

    private bool[] eventHappens;

    [SerializeField] private Transform backPanel;
    [SerializeField] private GameObject deleteButton;
    [SerializeField] private GameObject tableColors;
    [SerializeField] private GameObject arrow;
    [SerializeField] private Material blue;

    void Start()
    {
        //EventManager.onFigureChoosen += QuestCheck;

        Task = transform.GetChild(0).GetComponent<TMP_Text>();
        Task.text = texts[taskPointer];
        currentMaxPointer = questPoints[questPointer];

        eventHappens = new bool[5];
        for(int i = 0; i < eventHappens.Length; i++)
        {
            eventHappens[i] = false;
        }
    }

    private void OnDestroy()
    {
        //EventManager.onFigureChoosen -= QuestCheck;
    }

    public void forwardButtonClicked()
    {
        taskPointer = (taskPointer < texts.Length - 1) && (taskPointer < currentMaxPointer) ? taskPointer + 1 : taskPointer;
        Task.text = texts[taskPointer].Replace("\\n", "\n");
        if(taskPointer == 1 && !eventHappens[0])
        {
            eventHappens[0] = true;
            StartCoroutine(FigureBlink());
        }
        if(taskPointer == 3 && !eventHappens[1])
        {
            eventHappens[1] = true;
            StartCoroutine(BackPanelBlinking());
        }
        if(taskPointer == 17 && !eventHappens[2])
        {
            eventHappens[2] = true;
            StartCoroutine(EnableDeleteButton());
        }
        if (taskPointer == 18 && !eventHappens[3])
        {
            eventHappens[3] = true;
            StartCoroutine(EnableTableColors());
        }
        if (taskPointer == 19 && !eventHappens[4])
        {
            eventHappens[4] = true;
            StartCoroutine(BlinkArrow());
        }
        if (taskPointer == texts.Length - 1)
        {
            StartCoroutine(EndOfLevel());
        }
    }

    public void backwardButtonClicked()
    {
        taskPointer = taskPointer > 0 ? taskPointer - 1 : taskPointer;
        Task.text = texts[taskPointer].Replace("\\n", "\n");
    }

    public void QuestCheck()
    {
        //Квест 1. Проверка на выбор синего куба.
        if (questPointer == 0)
        {
            QuestCheck1();
        }
        //Квест 2. Проверка на изменённый красный конус.
        else if (questPointer == 1)
        {
            QuestCheck2();
        }
        //Квест 3. Проверка на добавленный розовый куб.
        else if (questPointer == 2)
        {
            QuestCheck3();
        }
        //Квест 4. Проверка на удаление.
        else if (questPointer == 3)
        {
            QuestCheck4();
        }
        //Квест 4. Проверка на добавление зелёного в синий.
        else if (questPointer == 4)
        {
            QuestCheck5();
        }
    }

    void QuestCheck1()
    {
        bool isComplete = true;
        foreach(var figure in figures)
        {
            if(figure.name == "Cube" && figure.GetComponent<Outline>().OutlineWidth == 0)
                isComplete = false;
            if(figure.name != "Cube" && figure.GetComponent<Outline>().OutlineWidth > 0)
                isComplete = false;
        }
        if (isComplete)
        {
            QuestCompleted();
            EventManager.sendPlayerCompleteTask1();
        }
    }

    void QuestCheck2()
    {
        bool isComplete = true;

        foreach (var figure in figures)
        {
            if (figure.name == "Cone (1)" && figure.GetComponent<MeshRenderer>().material.name.Replace(" (Instance)","") != "Blue")
            {
                isComplete = false;
            }
        }

        if (isComplete)
        {
            QuestCompleted();
            EventManager.sendPlayerCompleteTask2();
        }
    }

    void QuestCheck3()
    {
        bool isComplete = true;

        foreach (var figure in figures)
        {
            if (figure.name == "Cube (2)" && !figure.gameObject.activeSelf)
            {
                isComplete = false;
            }
        }

        if (isComplete)
        {
            QuestCompleted();
            EventManager.sendPlayerCompleteTask3();
        }
    }

    void QuestCheck4()
    {
        bool isComplete = true;

        foreach (var figure in figures)
        {
            if (figure.name == "Cube (2)" && figure.gameObject.activeSelf)
            {
                isComplete = false;
            }
        }

        if (isComplete)
        {
            QuestCompleted();
            EventManager.sendPlayerCompleteTask4();
        }
    }

    void QuestCheck5()
    {
        bool isComplete = true;

        if (blue.color.g == 0)
        {
            isComplete = false;
        }

        if (isComplete)
        {
            QuestCompleted();
            EventManager.sendPlayerCompleteTask4();
        }
    }

    void QuestCompleted()
    {
        questPointer++;
        Debug.Log("Task " + questPointer + " completed");
        currentMaxPointer = questPointer < questPoints.Length ? questPoints[questPointer] : texts.Length - 1;
    }

    IEnumerator FigureBlink()
    {
        float waitTime = 0.25f;
        int outlineWidth = 6;
        for(int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(waitTime);
            OutlineBlink(outlineWidth);
            yield return new WaitForSeconds(waitTime);
            OutlineBlink(0);
        }
        EventManager.sendFigureEndBlinking();
    }

    IEnumerator BackPanelBlinking()
    {
        float waitTime = 0.25f;
        Color color = Color.yellow;
        for (int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(waitTime);
            PanelBlink(color);
            yield return new WaitForSeconds(waitTime);
            PanelBlink(Color.white);
        }
    }

    IEnumerator EnableDeleteButton()
    {
        yield return new WaitForEndOfFrame();
        deleteButton.SetActive(true);
    }

    IEnumerator EnableTableColors()
    {
        yield return new WaitForEndOfFrame();
        tableColors.SetActive(true);
    }

    IEnumerator BlinkArrow()
    {
        for (int i = 0; i < 3; i++)
        {
            arrow.SetActive(true);
            yield return new WaitForSeconds(0.35f);
            arrow.SetActive(false);
            yield return new WaitForSeconds(0.35f);
        }
    }

    IEnumerator EndOfLevel()
    {
        yield return new WaitForSeconds(2);
        var canvas = transform.parent.parent.parent;
        canvas.GetChild(2).gameObject.SetActive(true);
        canvas.GetChild(1).gameObject.SetActive(false);
        canvas.GetChild(0).gameObject.SetActive(false);
        canvas.GetChild(3).gameObject.SetActive(false);
    }

    private void OutlineBlink(int width)
    {
        foreach (var figure in figures)
        {
            figure.GetComponent<Outline>().OutlineWidth = width;
        }
    }

    private void PanelBlink(Color color)
    {
        backPanel.GetComponent<Image>().color = color;
    }
}
