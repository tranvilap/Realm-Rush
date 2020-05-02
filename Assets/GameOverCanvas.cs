using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
public class GameOverCanvas : MonoBehaviour
{
    [SerializeField] RectTransform menuPanel = null;
    [SerializeField] float introDuration = 1f;
    [SerializeField] Ease introEaseType = Ease.Unset;

    public void ActiveCanvas()
    {
        gameObject.SetActive(true);
    }

    private void OnEnable()
    {
        Time.timeScale = 0f;
        menuPanel.anchoredPosition = new Vector2(0f, GetComponent<RectTransform>().rect.height);
        Sequence panelIntroSequence = DOTween.Sequence();
        menuPanel.DOAnchorPosY(0f, introDuration).SetEase(introEaseType).SetUpdate(true);
    }
    private void OnDisable()
    {
        Time.timeScale = 1f;
        menuPanel.anchoredPosition = new Vector2(0f, GetComponent<RectTransform>().rect.height);
    }
}
