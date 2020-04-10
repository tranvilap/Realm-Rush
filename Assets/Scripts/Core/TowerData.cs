using UnityEngine;

[CreateAssetMenu(fileName = "Tower", menuName ="Tower Data")]
public class TowerData : ScriptableObject
{
    public int price;
    public Sprite towerImage = null;
    public GameObject towerPreviewPrefab = null; 
    public GameObject towerPrefab=null; 
}
