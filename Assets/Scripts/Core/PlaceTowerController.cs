using System;
using UnityEngine;
using UnityEngine.EventSystems;
public class PlaceTowerController : MonoBehaviour, ITowerEvent
{
    public enum PLACE_TOWER_FAIL_REASON { NOT_ENOUGH_MONEY, UNPLACEABLE_POINT }

    public delegate void OnSuccessPlacingTower(TowerData tower, GameObject placedTower);
    public event OnSuccessPlacingTower SuccessPlacingTowerEvent;

    public delegate void OnFailedPlacingTower(TowerData tower, PLACE_TOWER_FAIL_REASON reason);
    public event OnFailedPlacingTower FailedPlacingTowerEvent;

    public event Action<Tower> OnSellingTowerEvent;

    private TowerData choosingTowerData = null;
    private TowerPlacePoint choosingSpawpoint = null;
    PlayerHQ playerHQ;

    public TowerData ChoosingTowerData { get => choosingTowerData; }

    private void Start()
    {
        playerHQ = FindObjectOfType<PlayerHQ>();
        EventSystemListener.main.AddListener(gameObject);
    }

    public void CheckTowerPlaceable(TowerPlacePoint placePoint)
    {
        if (placePoint != null)
        {
            if (placePoint.IsPlaceable)
            {
                if (placePoint != choosingSpawpoint)
                {
                    if (choosingSpawpoint != null)
                    {
                        choosingSpawpoint.HidePreviewTower();
                    }
                    choosingSpawpoint = placePoint;
                    choosingSpawpoint.ShowPreviewTower(choosingTowerData.towerPreviewPrefab);
                }
            }
        }
    }

    private void PlaceTower(TowerPlacePoint placePoint, TowerData towerData)
    {
        if (playerHQ.Money < towerData.price)
        {
            Debug.LogWarning("Not enough money");
            FailedPlacingTowerEvent?.Invoke(towerData, PLACE_TOWER_FAIL_REASON.NOT_ENOUGH_MONEY);
            return;
        }
        if(!placePoint.IsPlaceable)
        {
            Debug.LogWarning("This point is unplaceable");
            FailedPlacingTowerEvent?.Invoke(towerData, PLACE_TOWER_FAIL_REASON.UNPLACEABLE_POINT);
            return;
        }
        var placedTower = placePoint.BuildTower(towerData.towerPrefab);
        SuccessPlacingTowerEvent?.Invoke(towerData, placedTower);
    }

    public void ChooseTowerToPlace(TowerData towerData)
    {
        choosingTowerData = towerData;
    }

    public void UnchooseTowerToPlace()
    {
        choosingTowerData = null;
    }

    public void CeasePlacingTower()
    {
        if (choosingSpawpoint != null)
        {
            choosingSpawpoint.HidePreviewTower();
        }
        choosingSpawpoint = null;
    }

    public void PlaceChoosingTower()
    {
        if (choosingSpawpoint != null && choosingTowerData != null)
        {
            PlaceTower(choosingSpawpoint, choosingTowerData);
        }
    }

    public void OnSellingTower(Tower tower)
    {
        OnSellingTowerEvent?.Invoke(tower);
    }
}
