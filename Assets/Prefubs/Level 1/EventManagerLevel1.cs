using System;
using UnityEngine;

public class EventManagerLevel1 : MonoBehaviour
{
    public static event Action onSemicolon;

    public static event Action onSlytherinWin;
    public static event Action onGryffindorWin;

    public static void sendSemicolonUsed()
    {
        onSemicolon?.Invoke();
    }

    public static void sendSlytherinWin()
    {
        onSlytherinWin?.Invoke();
    }

    public static void sendGryffindorWin() => onGryffindorWin?.Invoke();
}
