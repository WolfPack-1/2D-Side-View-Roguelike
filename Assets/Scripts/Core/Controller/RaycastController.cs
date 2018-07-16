using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class RaycastController : MonoBehaviour
{
    protected const float skinWidth = 0.015f;
    const float distanceBetweenRays = 0.1f;
    protected int horizontalRayCount;
    protected int verticalRayCount;
    
    protected float horizontalRaySpacing;
    protected float verticalRaySpacing;

    protected new BoxCollider2D collider;
    protected RaycastOrigins raycastOrigins;
    
    
    public struct RaycastOrigins
    {
        public Vector2 topLeft, topRight;
        public Vector2 bottomLeft, bottomRight;
    }

    protected virtual void Awake()
    {
        collider = GetComponent<BoxCollider2D>();
        UpdateRaycastOrigins();
    }
    
    protected virtual void Start()
    {
        CalculateRaySpacing();
    }

    protected virtual void Update()
    {
        
    }
    
    protected void UpdateRaycastOrigins()
    {
        Bounds bounds = collider.bounds;
        bounds.Expand(skinWidth * -2);

        raycastOrigins.bottomLeft = new Vector2(bounds.min.x, bounds.min.y);
        raycastOrigins.bottomRight = new Vector2(bounds.max.x, bounds.min.y);
        raycastOrigins.topLeft = new Vector2(bounds.min.x, bounds.max.y);
        raycastOrigins.topRight = new Vector2(bounds.max.x, bounds.max.y);
    }

    protected void CalculateRaySpacing()
    {
        Bounds bounds = collider.bounds;
        bounds.Expand(skinWidth * -2);

        float boundsWidth = bounds.size.x;
        float boundsHeight = bounds.size.y;

        horizontalRayCount = Mathf.Max(Mathf.RoundToInt(boundsHeight / distanceBetweenRays),2);
        verticalRayCount = Mathf.Max(Mathf.RoundToInt(boundsWidth / distanceBetweenRays),2);

        horizontalRaySpacing = bounds.size.y / (horizontalRayCount - 1);
        verticalRaySpacing = bounds.size.x / (verticalRayCount - 1);
    }
}
