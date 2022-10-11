using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Image timeIndicatorImage;

    private void OnEnable()
    {
        EventManager.GripEvent += IndicatorAnimation;
    }

    private void OnDisable()
    {
        EventManager.GripEvent -= IndicatorAnimation;
    }

    private void IndicatorAnimation(GameObject grip)
    {

    }
}
