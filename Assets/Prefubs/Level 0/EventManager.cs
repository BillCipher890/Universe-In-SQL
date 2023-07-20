using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static event Action onFigureChoosen;

    public static event Action onFigureEndBlinking;

    public static event Action onPlayerCompleteTask1;

    public static event Action onPlayerCompleteTask2;

    public static event Action onPlayerCompleteTask3;

    public static event Action onPlayerCompleteTask4;

    public static void sendFigureChoosen()
    {
        onFigureChoosen?.Invoke();
    }

    public static void sendFigureEndBlinking()
    {
        onFigureEndBlinking?.Invoke();
    }

    public static void sendPlayerCompleteTask1()
    {
        onPlayerCompleteTask1?.Invoke();
    }

    public static void sendPlayerCompleteTask2()
    {
        onPlayerCompleteTask2?.Invoke();
    }

    public static void sendPlayerCompleteTask3()
    {
        onPlayerCompleteTask3?.Invoke();
    }

    public static void sendPlayerCompleteTask4()
    {
        onPlayerCompleteTask4?.Invoke();
    }
}
