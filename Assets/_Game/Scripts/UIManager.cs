using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Image timeIndicatorImage;

    private Tweener scaleTween;

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
        if (scaleTween == null)
        {
            timeIndicatorImage.rectTransform.localScale = Vector3.one;

            scaleTween = timeIndicatorImage.rectTransform.DOScaleX(0f, 2f)
                .SetEase(Ease.Linear);
        }
        else
        {
            if (!scaleTween.IsPlaying())
            {
                timeIndicatorImage.rectTransform.localScale = Vector3.one;

                scaleTween = timeIndicatorImage.rectTransform.DOScaleX(0f, 2f)
                    .SetEase(Ease.Linear);
            }
        }
    }
}
