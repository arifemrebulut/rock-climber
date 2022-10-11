using System;
using UnityEngine;

public static class EventManager
{
    public static Action<GameObject> GripEvent;
    public static Action KillPlayerEvent;

    public static void CallGripEvent(GameObject grip) => GripEvent?.Invoke(grip);
    public static void CallKillPlayerEvent() => KillPlayerEvent?.Invoke();
}