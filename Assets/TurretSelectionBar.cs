using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class TurretSelectionBar : MonoBehaviour
{
    [Header("Params")]
    [SerializeField] Vector2 barBaseSize;
    [SerializeField] float barMaxHorizontalSize = 600f;
    [Header("Open/Close Action")]
    [SerializeField] float barSizeOffsetX = 100f;
    [SerializeField] float slideBarDuration = 1f;
    [SerializeField] Ease slideBarEaseType= Ease.Unset;

    [Header("References")]
    [SerializeField] Image buttonArrow = null;
    [SerializeField] ChoosingTowerButton towerButtonPrefab = null;
    [SerializeField] RectTransform buttonContent = null;
    [SerializeField] TowerData data;
    [Header("")]


    RectTransform rectTransform;
    List<ChoosingTowerButton> choosingTowerButtons;
    bool isOpening = false;

    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        SetUpBar();
    }

    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.A))
        {
            AddChoosingTowerButton(data);
        }
    }
    
    public void OpenBar()
    {
        rectTransform.DOAnchorPosX(0f, slideBarDuration, false).SetEase(slideBarEaseType);
        buttonArrow.rectTransform.Rotate(new Vector3(0f, 180f, 0f), Space.World);
        isOpening = true;
    }
    public void CloseBar()
    {
        rectTransform.DOAnchorPosX(-(rectTransform.rect.width * rectTransform.localScale.x - barSizeOffsetX), slideBarDuration, false)
            .SetEase(slideBarEaseType);
        buttonArrow.rectTransform.Rotate(new Vector3(0f, 180f, 0f), Space.World);
        isOpening = false;
    }
    public void OnClickOpenCloseButton()
    {
        if (isOpening)
        {
            CloseBar();
        }
        else
        {
            OpenBar();
        }
    }

    private void SetUpBar()
    {
        rectTransform.sizeDelta = barBaseSize;
        rectTransform.anchoredPosition = new Vector3(-(rectTransform.rect.width * rectTransform.localScale.x - barSizeOffsetX), 0f, 0f);
    }
    private void AddChoosingTowerButton(TowerData tower)
    {
        var go = Instantiate(towerButtonPrefab, buttonContent);
        go.GetComponent<ChoosingTowerButton>().SetUpButton(tower);
        ExpandBarSize(go.GetComponent<RectTransform>().rect.width);
    }
    private void ExpandBarSize(float range)
    {
        if(rectTransform.rect.width +range >= barMaxHorizontalSize) { return; }
        rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x + range, rectTransform.sizeDelta.y);
    }
}
