using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

[CustomEditor(typeof(Grid))]
public class GridEditor : Editor
{

     Transform targetTransform;
     
     void OnEnable()
     {
          targetTransform = ((Grid) target).transform;
          foreach (Transform childTransform in targetTransform)
          {
               TilemapRenderer tilemapRenderer = childTransform.GetComponent<TilemapRenderer>();
               if (tilemapRenderer == null)
               {
                    Debug.LogError("Grid의 자식은 무조건 tilemap 이어야 합니다.");
                    continue;
               }

               tilemapRenderer.sortingOrder = childTransform.GetSiblingIndex();
          }
     }
}
