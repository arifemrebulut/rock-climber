using System.Collections;
using UnityEngine;

public class Grip : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        EventManager.CallGripEvent(gameObject);
    }
}
