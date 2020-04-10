using UnityEngine;
using UnityEngine.EventSystems;
public class PlaceTowerController : MonoBehaviour
{
    public enum PLACE_TOWER_FAIL_REASON { NOT_ENOUGH_MONEY}

    public delegate void OnSuccessPlacingTower(TowerData tower);
    public event OnSuccessPlacingTower SuccessPlacingTowerEvent;

    public delegate void OnFailedPlacingTower(TowerData tower, PLACE_TOWER_FAIL_REASON reason);
    public event OnFailedPlacingTower FailedPlacingTowerEvent;

    private TowerData choosingTowerData = null;
    private PlaceTowerPoint choosingSpawpoint = null;
    PlayerHQ playerHQ;

    public TowerData ChoosingTowerData { get => choosingTowerData;}

    private void Start()
    {
        playerHQ = FindObjectOfType<PlayerHQ>();
    }

    public void CheckTowerPlaceable(PlaceTowerPoint spawnPoint)
    {
        if (spawnPoint != null)
        {
            if (spawnPoint.IsPlacable)
            {
                if (spawnPoint != choosingSpawpoint)
                {
                    if (choosingSpawpoint != null)
                    {
                        choosingSpawpoint.HidePreviewTower();
                    }
                    choosingSpawpoint = spawnPoint;
                    choosingSpawpoint.ShowPreviewTower(choosingTowerData.towerPreviewPrefab);
                }
                if (Input.GetMouseButton(0))
                {
                    PlaceTower(spawnPoint, choosingTowerData);
                }
            }
        }
        else
        {
            if (choosingSpawpoint != null)
            {
                choosingSpawpoint.HidePreviewTower();
            }
        }
    }

    private void PlaceTower(PlaceTowerPoint spawnPoint, TowerData towerData)
    {
        if(playerHQ.Money < towerData.price)
        {
            Debug.LogWarning("Not enough money");
            FailedPlacingTowerEvent?.Invoke(towerData, PLACE_TOWER_FAIL_REASON.NOT_ENOUGH_MONEY);
            return;
        }
        spawnPoint.PlaceTower(towerData.towerPrefab);
        SuccessPlacingTowerEvent?.Invoke(towerData);
    }

    public void ChooseTowerToPlace(TowerData towerData)
    {
        choosingTowerData = towerData;
    }

    public void UnchooseTowerToPlace()
    {
        choosingTowerData = null;
    }
}
