using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestControllerLVL1 : MonoBehaviour
{
    //Номер в диалоговой системе
    public int[] questPoints;
    public Transform[] allStudents;

    public string[] texts;
    private TMP_Text Task;
    private int taskPointer = 0;
    private int questPointer = 0;
    private int currentMaxPointer = 0;
    void Start()
    {
        EventManagerLevel1.onSemicolon += QuestCheck;

        Task = transform.GetChild(0).GetComponent<TMP_Text>();
        Task.text = texts[taskPointer];
        currentMaxPointer = questPoints[questPointer];
    }

    private void OnDestroy()
    {
        EventManagerLevel1.onSemicolon -= QuestCheck;
    }

    public void forwardButtonClicked()
    {
        taskPointer = (taskPointer < texts.Length - 1) && (taskPointer < currentMaxPointer) ? taskPointer + 1 : taskPointer;
        Task.text = texts[taskPointer].Replace("\\n","\n");
        if(taskPointer == 8)
        {
            Debug.Log("send slytherin win");
            EventManagerLevel1.sendSlytherinWin();
        }
        if (taskPointer == 21)
        {
            EventManagerLevel1.sendGryffindorWin();
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

    void QuestCheck()
    {
        //Квест 1. Проверка на Слизерин.
        if (questPointer == 0)
        {
            StartCoroutine(QuestCheck1());
        }
        //Квест 2. Проверка на Гермиону.
        else if (questPointer == 1)
        {
            StartCoroutine(QuestCheck2());
        }
        //Квест 3. Проверка на Рона.
        else if (questPointer == 2)
        {
            StartCoroutine(QuestCheck3());
        }
        //Квест 4. Проверка на Гарри.
        else if (questPointer == 3)
        {
            StartCoroutine(QuestCheck4());
        }
        //Квест 5. Проверка на Невилла.
        else if (questPointer == 4)
        {
            StartCoroutine(QuestCheck5());
        }
    }

    IEnumerator QuestCheck1()
    {
        yield return new WaitForSeconds(0.1f);
        bool completed = true;
        foreach (var student in allStudents)
        {
            //Если не слизерин и выделен или слизерин и не выделен
            if (student.GetComponent<FacultyController>().currentFaculty != FacultyController.Faculty.Slytherin &&
                (student.GetComponent<StudentController>().selectedCurrent || student.GetComponent<StudentController>().isShowNameCurrent) ||
                student.GetComponent<FacultyController>().currentFaculty == FacultyController.Faculty.Slytherin &&
                !(student.GetComponent<StudentController>().selectedCurrent || student.GetComponent<StudentController>().isShowNameCurrent))
            {
                completed = false;
            }
        }
        if (completed)
        {
            QuestCompleted();
        }
    }

    IEnumerator QuestCheck2()
    {
        yield return new WaitForSeconds(0.1f);
        bool completed = true;
        foreach (var student in allStudents)
        {
            //Если Гермиона не выделена или не Гермиона выделена
            if (student.GetComponent<StudentController>().engFullName != "Hermione Granger" &&
                (student.GetComponent<StudentController>().selectedCurrent || student.GetComponent<StudentController>().isShowNameCurrent) ||
                student.GetComponent<StudentController>().engFullName == "Hermione Granger" &&
                !(student.GetComponent<StudentController>().selectedCurrent || student.GetComponent<StudentController>().isShowNameCurrent))
            {
                completed = false;
            }
        }
        if (completed)
        {
            QuestCompleted();
        }
    }

    IEnumerator QuestCheck3()
    {
        yield return new WaitForSeconds(0.1f);
        bool completed = true;
        foreach (var student in allStudents)
        {
            //Если Гермиона не выделена или не Гермиона выделена
            if (student.GetComponent<StudentController>().engFullName != "Ronald Weasley" &&
                (student.GetComponent<StudentController>().selectedCurrent || student.GetComponent<StudentController>().isShowNameCurrent) || 
                student.GetComponent<StudentController>().engFullName == "Ronald Weasley" &&
                !(student.GetComponent<StudentController>().selectedCurrent || student.GetComponent<StudentController>().isShowNameCurrent))
            {
                completed = false;
            }
        }
        if (completed)
        {
            QuestCompleted();
        }
    }

    IEnumerator QuestCheck4()
    {
        yield return new WaitForSeconds(0.1f);
        bool completed = true;
        foreach (var student in allStudents)
        {
            //Если Гермиона не выделена или не Гермиона выделена
            if (student.GetComponent<StudentController>().engFullName != "Harry Potter" &&
                (student.GetComponent<StudentController>().selectedCurrent || student.GetComponent<StudentController>().isShowNameCurrent) ||
                student.GetComponent<StudentController>().engFullName == "Harry Potter" &&
                !(student.GetComponent<StudentController>().selectedCurrent || student.GetComponent<StudentController>().isShowNameCurrent))
            {
                completed = false;
            }
        }
        if (completed)
        {
            QuestCompleted();
        }
    }

    IEnumerator QuestCheck5()
    {
        yield return new WaitForSeconds(0.1f);
        bool completed = true;
        foreach (var student in allStudents)
        {
            //Если Гермиона не выделена или не Гермиона выделена
            if (student.GetComponent<StudentController>().engFullName != "Neville Longbottom" &&
                (student.GetComponent<StudentController>().selectedCurrent || student.GetComponent<StudentController>().isShowNameCurrent) ||
                student.GetComponent<StudentController>().engFullName == "Neville Longbottom" &&
                !(student.GetComponent<StudentController>().selectedCurrent || student.GetComponent<StudentController>().isShowNameCurrent))
            {
                completed = false;
            }
        }
        if (completed)
        {
            QuestCompleted();
        }
    }

    IEnumerator EndOfLevel()
    {
        yield return new WaitForSeconds(1);
        var canvas = transform.parent.parent.parent;
        canvas.GetChild(2).gameObject.SetActive(true);
        canvas.GetChild(1).gameObject.SetActive(false);
        canvas.GetChild(0).gameObject.SetActive(false);
    }

    void QuestCompleted()
    {
        questPointer++;
        Debug.Log("Task " + questPointer + " completed");
        currentMaxPointer = questPointer < questPoints.Length ? questPoints[questPointer] : texts.Length - 1;
    }
}
