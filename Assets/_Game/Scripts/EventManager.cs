using System;
using UnityEngine;

public static class EventManager
{
    public static Action<GameObject> GripEvent;

    public static void CallGripEvent(GameObject grip) => GripEvent?.Invoke(grip);
}