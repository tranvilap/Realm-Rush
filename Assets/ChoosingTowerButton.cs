using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ChoosingTowerButton : MonoBehaviour
{
    public ChoosingTowerButton[] otherButtons;
    public TowerData holdingTowerData = null;
    [SerializeField] Sprite isChoosingSprite = null;
    [SerializeField] Image towerImage = null;

    public bool isChoosing = false;
    Sprite originalButtonSprite;
    Image buttonImage;
    Button button;
    PlaceTowerController placeTowerController;
    private void Awake()
    {
        button = GetComponent<Button>();
        placeTowerController = FindObjectOfType<PlaceTowerController>();
        originalButtonSprite = button.GetComponent<Image>().sprite;
        Debug.Log(originalButtonSprite);
        buttonImage = GetComponent<Image>();
    }

    public void SetUpButton(TowerData towerData)
    {
        holdingTowerData = towerData;
        button.onClick.AddListener(() => OnClickButton());
        towerImage.sprite = holdingTowerData.towerImage;
    }

    public void OnClickButton()
    {
        ChoosingTowerButton ctb = null;
        foreach (Transform tf in transform.parent)
        {
            ctb = tf.GetComponent<ChoosingTowerButton>();
            if (ctb == null || ctb == this) { continue; }
            if (ctb.isChoosing)
            {
                ctb.UnchooseButton();
                break;
            }
        }
        if (isChoosing)
        {
            UnchooseButton();
        }
        else
        {
            ChooseButton();
        }
        
    }

    public void UnchooseButton()
    {
        placeTowerController.UnchooseTowerToPlace();
        buttonImage.sprite = originalButtonSprite;
        isChoosing = false;
    }

    public void ChooseButton()
    {
        placeTowerController.ChooseTowerToPlace(holdingTowerData);
        buttonImage.sprite = isChoosingSprite;
        isChoosing = true;
    }
}
