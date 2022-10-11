using DG.Tweening;
using UnityEngine;

public class SawBlade : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;

    void Start()
    {
        SawAnimationTween();
    }

    private void SawAnimationTween()
    {
        Sequence sequence = DOTween.Sequence();

        sequence.Append(transform.DOLocalMoveX(transform.localPosition.x * -1, 1.5f)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.Linear));

        sequence.Join(transform.DOLocalRotate(new Vector3(0f, 0f, 90f), rotationSpeed, RotateMode.LocalAxisAdd)
            .SetLoops(-1)
            .SetEase(Ease.Linear));
    }
}