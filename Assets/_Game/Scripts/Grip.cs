using System.Collections;
using UnityEngine;

public class Grip : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        EventManager.CallGripEvent(gameObject);

        StartCoroutine(DestroyGrip());
    }

    private IEnumerator DestroyGrip()
    {
        yield return new WaitForSeconds(2f);

        Destroy(gameObject);
    }
}
