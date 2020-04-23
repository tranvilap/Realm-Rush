using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class IconAnimations : MonoBehaviour
{
    [SerializeField] RectTransform baseRect;
    [SerializeField] TextMeshProUGUI amountText = null;
    [SerializeField] RectTransform icon = null;

    [SerializeField] float incrementDistance = 200f;
    [SerializeField] float decrementDistance = -200f;

    [SerializeField] float movingDuration = 1f;
    [SerializeField] float iconRotateDuration = 0.2f;

    private void Start()
    {
        //rectTransform = GetComponent<RectTransform>();
    }

    public void PlayIncrementAnimation(int amount)
    {
        Vector3 originalPos = baseRect.position;
        amountText.text = "+" + amount.ToString();

        Sequence sequence = DOTween.Sequence();
        sequence.Append(baseRect.DOAnchorPosY(incrementDistance, movingDuration, false))
            .Join(icon.DORotate(new Vector3(0f, 360f, 0f), iconRotateDuration, RotateMode.WorldAxisAdd)
                .SetLoops((int)(movingDuration / iconRotateDuration)))
            .OnComplete(() => OnCompleteAnimation(originalPos));
    }

    public void PlayDecrementAnimation(int amount)
    {
        Vector3 originalPos = baseRect.position;
        amountText.text = "-" + amount.ToString();

        Sequence sequence = DOTween.Sequence();
        sequence.Append(baseRect.DOAnchorPosY(decrementDistance, movingDuration, false))
            .Join(icon.DORotate(new Vector3(0f, 360f, 0f), iconRotateDuration, RotateMode.WorldAxisAdd)
                .SetLoops((int)(movingDuration / iconRotateDuration)))
            .OnComplete(() => OnCompleteAnimation(originalPos));
    }

    private void OnCompleteAnimation(Vector3 originalPos)
    {
        baseRect.position = originalPos;
        baseRect.gameObject.SetActive(false);
    }
}
