using UnityEngine;
using UnityEngine.Tilemaps;
#if UNITY_EDITOR
using UnityEditor;
#endif


public class GimmickTile : TileBase
{

    [SerializeField] GimmickStruct gimmickStruct;
    [SerializeField] Sprite tileSprite;

    public void Action()
    {
        this.Log("Actoin");
    }
    
    #if UNITY_EDITOR
    [MenuItem("Assets/Create/Gimmick Tile")]
    public static void CreateCustomTile()
    {
        string path = EditorUtility.SaveFilePanelInProject("Save Gimmick Tile", "New Gimmick Tile", "Asset", "Save Gimmick Tile", "Assets");
        if (path == "")
            return;
        AssetDatabase.CreateAsset(CreateInstance<GimmickTile>(), path);
    }
    #endif
    
    
    
}