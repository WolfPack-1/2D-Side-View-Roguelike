using UnityEngine;

public class Controller2D : RaycastController
{
    public struct CollisionInfo
    {
        public bool above, below;
        public bool left, right;

        public bool climbingSlope, descendingSlope;
        public float slopeAngle, slopeAngleOld;
        public Vector2 moveAmountOld;
        public int faceDir;
        public bool fallingThrough;
        
        public void Reset()
        {
            above = below = false;
            left = right = false;
            climbingSlope = descendingSlope = false;
            slopeAngleOld = slopeAngle;
            slopeAngle = 0;
        }
    }
    
    [SerializeField] protected LayerMask groundMask;
    [SerializeField] float maxClimbAngle;
    [SerializeField] float maxDescendAngle;

    Vector2 playerInput;

    
    protected CollisionInfo collisions;

    protected override void Start()
    {
        base.Start();
        collisions.faceDir = 1;
    }

    protected override void Update()
    {

    }

    public void Move(Vector2 moveAmount, bool standingOnPlatform = false)
    {
        Move(moveAmount, Vector2.zero, standingOnPlatform);
    }

    public void Move(Vector2 moveAmount, Vector2 input, bool standingOnPlatform = false)
    {
        UpdateRaycastOrigins();
        collisions.Reset();
        collisions.moveAmountOld = moveAmount;
        playerInput = input;
        
        if (moveAmount.x != 0)
        {
            collisions.faceDir = (int)Mathf.Sign(moveAmount.x);
        }

        if (moveAmount.y < 0)
        {
            DescendSlope(ref moveAmount);
        }

        if (moveAmount.x != 0)
        {
            HorizontalCollisions(ref moveAmount);
        }

        if (moveAmount.y != 0)
        {
            VerticalCollisions(ref moveAmount);
        }
        
        transform.Translate(moveAmount);

        if (standingOnPlatform)
        {
            collisions.below = true;
        }

        Flip(moveAmount);
    }
    
    void HorizontalCollisions(ref Vector2 moveAmount)
    {
        float directionX = collisions.faceDir;
        float rayLength = Mathf.Abs(moveAmount.x) + skinWidth;
        
        
        for (int i = 0; i < horizontalRayCount; i++)
        {
            Vector2 rayOrigin = (directionX == -1) ? raycastOrigins.bottomLeft : raycastOrigins.bottomRight;
            rayOrigin += Vector2.up * (horizontalRaySpacing * i);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLength, groundMask);
            
            Debug.DrawRay(rayOrigin, Vector2.right * directionX, Color.red);

            if (hit)
            {
                if (hit.distance == 0)
                    continue;
                
                float slopeAngle = Vector2.Angle(hit.normal, Vector2.up);
                if (i == 0 && slopeAngle <= maxClimbAngle)
                {
                    if (collisions.descendingSlope)
                    {
                        collisions.descendingSlope = false;
                        moveAmount = collisions.moveAmountOld;
                    }
                    float distanceToSlopeStart = 0;
                    if (slopeAngle != collisions.slopeAngleOld)
                    {
                        distanceToSlopeStart = hit.distance - skinWidth;
                        moveAmount.x -= distanceToSlopeStart * directionX;
                    }
                    ClimbSlope(ref moveAmount, slopeAngle);
                    moveAmount.x += distanceToSlopeStart * directionX;
                }

                if (!collisions.climbingSlope || slopeAngle > maxClimbAngle)
                {
                    moveAmount.x = (hit.distance - skinWidth) * directionX;
                    rayLength = hit.distance;

                    if (collisions.climbingSlope)
                    {
                        moveAmount.y = Mathf.Tan(collisions.slopeAngle * Mathf.Deg2Rad) * Mathf.Abs(moveAmount.x);
                    }

                    collisions.left = directionX == -1;
                    collisions.right = directionX == 1;   
                }
            }
        }  
    }

    void VerticalCollisions(ref Vector2 moveAmount)
    {
        float directionY = Mathf.Sign(moveAmount.y);
        float rayLength = Mathf.Abs(moveAmount.y) + skinWidth;
        
        
        for (int i = 0; i < verticalRayCount; i++)
        {
            Vector2 rayOrigin = (directionY == -1) ? raycastOrigins.bottomLeft : raycastOrigins.topLeft;
            rayOrigin += Vector2.right * (verticalRaySpacing * i + moveAmount.x);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up * directionY, rayLength, groundMask);
            
            Debug.DrawRay(rayOrigin, Vector2.up * directionY, Color.red);
            
            if (hit)
            {
                if (hit.collider.tag == "Through")
                {
                    if (directionY == 1 || hit.distance == 0)
                        continue;

                    if (collisions.fallingThrough)
                        continue;

                    if (playerInput.y == -1)
                    {
                        collisions.fallingThrough = true;
                        Invoke("ResetFallingThroughPlatform", 0.5f);
                        continue;
                    }
                }
                
                moveAmount.y = (hit.distance - skinWidth) * directionY;
                rayLength = hit.distance;

                if (collisions.climbingSlope)
                {
                    moveAmount.x = moveAmount.y / Mathf.Tan(collisions.slopeAngle * Mathf.Deg2Rad) * Mathf.Sign(moveAmount.x);
                }
                
                collisions.below = directionY == -1;
                collisions.above = directionY == 1;
            }
        }

        if (collisions.climbingSlope)
        {
            float directionX = Mathf.Sign(moveAmount.x);
            rayLength = Mathf.Abs(moveAmount.x) + skinWidth;
            Vector2 rayOrigin = ((directionX == -1) ? raycastOrigins.bottomLeft : raycastOrigins.bottomRight) + Vector2.up * moveAmount.y;
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLength, groundMask);

            if (hit)
            {
                float slopeAngle = Vector2.Angle(hit.normal, Vector2.up);
                if (slopeAngle != collisions.slopeAngle)
                {
                    moveAmount.x = (hit.distance - skinWidth) * directionX;
                    collisions.slopeAngle = slopeAngle;
                }
            }
        }
    }
    
    void ClimbSlope(ref Vector2 moveAmount, float slopeAngle)
    {
        float moveDistance = Mathf.Abs(moveAmount.x);
        float climbmoveAmountY = Mathf.Sin(slopeAngle * Mathf.Deg2Rad) * moveDistance;

        if (moveAmount.y <= climbmoveAmountY)
        {
            moveAmount.y = climbmoveAmountY;
            moveAmount.x = Mathf.Cos(slopeAngle * Mathf.Deg2Rad) * moveDistance * Mathf.Sign(moveAmount.x);
            collisions.below = true;
            collisions.climbingSlope = true;
            collisions.slopeAngle = slopeAngle;
        }
    }

    void DescendSlope(ref Vector2 moveAmount)
    {
        float directionX = Mathf.Sign(moveAmount.x);
        Vector2 rayOrigin = (directionX == -1) ? raycastOrigins.bottomRight : raycastOrigins.bottomLeft;
        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.down, Mathf.Infinity, groundMask);

        if (hit)
        {
            float slopeAngle = Vector2.Angle(hit.normal, Vector2.up);
            if (slopeAngle != 0 && slopeAngle <= maxDescendAngle)
            {
                if (Mathf.Sign(hit.normal.x) == directionX)
                {
                    if(hit.distance - skinWidth <= Mathf.Tan(slopeAngle * Mathf.Deg2Rad) * Mathf.Abs(moveAmount.x))
                    {
                        float moveDistance = Mathf.Abs(moveAmount.x);
                        float descendmoveAmountY = Mathf.Sin(slopeAngle * Mathf.Deg2Rad) * moveDistance;
                        moveAmount.x = Mathf.Cos(slopeAngle * Mathf.Deg2Rad) * moveDistance * Mathf.Sign(moveAmount.x);
                        moveAmount.y -= descendmoveAmountY;

                        collisions.slopeAngle = slopeAngle;
                        collisions.descendingSlope = true;
                        collisions.below = true;
                    }
                }
            }
        }
    }

    void ResetFallingThroughPlatform()
    {
        collisions.fallingThrough = false;
    }

    void Flip(Vector2 moveAmount)
    {
        if (moveAmount.x == 0)
            return;
        
        Vector3 scale = transform.localScale;
        scale.x =  Mathf.Sign(-moveAmount.x);
        transform.localScale = scale;
    }

}
