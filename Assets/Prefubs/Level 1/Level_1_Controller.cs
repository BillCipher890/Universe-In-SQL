using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_1_Controller : MonoBehaviour
{
    [SerializeField] private LevelData levelData;
    public string cardUsed;
    private string lastCardUsed;

    private bool studentShowName = false;
    private bool studentOutlineBody = false;
    private bool teacherShowName = false;
    private bool teacherOutlineBody = false;

    private List<Condition> conditions = new List<Condition>();
    private List<Connector> connectors = new List<Connector>();

    private enum NextWillBe
    {
        element,
        studentName,
        faculty,
        connector
    }
    private NextWillBe nextWillBe = NextWillBe.element;

    void Update()
    {
        cardUsed = levelData.usedCardName;

        if(cardUsed != lastCardUsed)
        {
            switch (cardUsed)
            {
                case "SELECT":
                    ClearData();
                    nextWillBe = NextWillBe.element;
                    break;

                case "student.name":
                    studentShowName = true;
                    break;

                case "student.body":
                    studentOutlineBody = true;
                    break;

                case "teacher.name":
                    teacherShowName = true;
                    break;

                case "teacher.body":
                    teacherOutlineBody = true;
                    break;

                case "name = ":
                    nextWillBe = NextWillBe.studentName;
                    break;

                case "faculty = ":
                    nextWillBe = NextWillBe.faculty;
                    break;

                case ";":
                    influence();
                    break;

                default:
                    if(nextWillBe == NextWillBe.studentName)
                    {
                        conditions.Add(new Condition(cardUsed.Trim('"'), null));
                        nextWillBe = NextWillBe.connector;
                    }
                    else if (nextWillBe == NextWillBe.faculty)
                    {
                        conditions.Add(new Condition(null, cardUsed.Trim('"')));
                        nextWillBe = NextWillBe.connector;
                    }
                    else if(nextWillBe == NextWillBe.connector)
                    {
                        Enum.TryParse(cardUsed, out Connector connector);
                        connectors.Add(connector);
                        nextWillBe = NextWillBe.element;
                    }
                    break;
            }
        }

        lastCardUsed = cardUsed;
    }

    void ClearData()
    {
        conditions = new List<Condition>();
        connectors = new List<Connector>();
        //find right students
        GameObject[] gameObjectsOnScene = gameObject.scene.GetRootGameObjects();
        GameObject studentsArray = new GameObject();
        for (int i = 0; i < gameObjectsOnScene.Length; i++)
        {
            if (gameObjectsOnScene[i].name == "StudentsArray")
            {
                studentsArray = gameObjectsOnScene[i];
            }
        }
        for (int i = 0; i < studentsArray.transform.childCount; i++)
        {
            Transform student = studentsArray.transform.GetChild(i);
            student.GetComponent<StudentController>().isShowNameCurrent = false;
            student.GetComponent<StudentController>().selectedCurrent = false;
        }
    }

    void influence()
    {
        //find right students
        GameObject[] gameObjectsOnScene = gameObject.scene.GetRootGameObjects();
        GameObject studentsArray = new GameObject();
        for (int i = 0; i < gameObjectsOnScene.Length; i++)
        {
            if (gameObjectsOnScene[i].name == "StudentsArray")
            {
                studentsArray = gameObjectsOnScene[i];
            }
        }
        for (int i = 0; i < studentsArray.transform.childCount; i++)
        {
            Transform student = studentsArray.transform.GetChild(i);
            bool lastMayBeFit = true;
            bool maybeFit = true;
            bool fit = false;

            lastMayBeFit = conditionCheck(0, student);
            for(int j = 0; j < connectors.Count; j++)
            {
                if (connectors[j] == Connector.or)
                {
                    if (lastMayBeFit && maybeFit)
                    {
                        fit = true;
                        break;
                    }
                    else
                    {
                        lastMayBeFit = true;
                    }
                }
                else if (connectors[j] == Connector.and)
                {
                    lastMayBeFit = lastMayBeFit && maybeFit;
                }
                maybeFit = conditionCheck(j + 1, student);

            }
            if (!fit)
            {
                if (connectors.Count == 0)
                {
                    fit = lastMayBeFit;
                }
                else
                {
                    fit = lastMayBeFit && maybeFit;
                }
            }
            //influence
            if (fit)
            {
                if (studentShowName)
                {
                    student.GetComponent<StudentController>().isShowNameCurrent = true;
                }
                if (studentOutlineBody)
                {
                    student.GetComponent<StudentController>().selectedCurrent = true;
                }
            }
        }

        //find all teachers
        /*GameObject teacherArray = new GameObject();
        for (int i = 0; i < gameObjectsOnScene.Length; i++)
        {
            if (gameObjectsOnScene[i].name == "TeachersArray")
            {
                teacherArray = gameObjectsOnScene[i];
            }
        }
        for (int i = 0; i < teacherArray.transform.childCount; i++)
        {
            Transform teacher = teacherArray.transform.GetChild(i);
            bool lastMayBeFit = true;
            bool maybeFit = true;
            bool fit = false;

            lastMayBeFit = conditionCheck(0, teacher);
            for (int j = 0; j < connectors.Count; j++)
            {
                if (connectors[j] == Connector.or)
                {
                    if (lastMayBeFit && maybeFit)
                    {
                        fit = true;
                        break;
                    }
                    else
                    {
                        lastMayBeFit = true;
                    }
                }
                else if (connectors[j] == Connector.and)
                {
                    lastMayBeFit = lastMayBeFit && maybeFit;
                }

                maybeFit = conditionCheck(j + 1, teacher);
            }
            if (!fit)
            {
                if (connectors.Count == 0)
                {
                    fit = lastMayBeFit;
                }
                else
                {
                    fit = lastMayBeFit && maybeFit;
                }
            }

            Debug.Log(teacher.GetComponent<StudentController>().engFullName + " = " + fit);

            //influence
            if (fit)
            {
                if (teacherShowName)
                {
                    teacher.GetComponent<StudentController>().isShowNameCurrent = true;
                }
                if (teacherOutlineBody)
                {
                    teacher.GetComponent<StudentController>().selectedCurrent = true;
                }
            }
        }*/
    }

    private bool conditionCheck(int j, Transform student)
    {
        bool maybeFit = false;
        if (conditions[j].faculty == null)
        {
            if (student.GetComponent<StudentController>().engFullName == conditions[j].studentName)
            {
                maybeFit = true;
            }
            else
            {
                maybeFit = false;
            }
        }
        else if (conditions[j].studentName == null)
        {
            Enum.TryParse(conditions[j].faculty, out FacultyController.Faculty facultyFromConditions);
            if (student.GetComponent<FacultyController>().currentFaculty == facultyFromConditions)
            {
                maybeFit = true;
            }
            else
            {
                maybeFit = false;
            }
        }
        return maybeFit;
    }

    private class Condition
    {
        public string studentName;
        public string faculty;

        public Condition(string studentName, string faculty)
        {
            this.studentName = studentName;
            this.faculty = faculty;
        }
    }

    private enum Connector
    {
        and,
        or
    }
}
