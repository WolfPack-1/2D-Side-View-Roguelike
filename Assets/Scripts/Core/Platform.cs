using System.Collections;
using UnityEngine;

[RequireComponent(typeof(PlatformEffector2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class Platform : MonoBehaviour
{
    BoxCollider2D col;
    
    void Awake()
    {
        gameObject.layer = LayerMask.NameToLayer("Ground");
        col = GetComponent<BoxCollider2D>();
        col.usedByEffector = true;
    }

    public void DownJump()
    {
        StopAllCoroutines();
        StartCoroutine("DownJumpUpdator");
    }

    IEnumerator DownJumpUpdator()
    {
        col.enabled = false;
        yield return new WaitForSeconds(0.5f);
        col.enabled = true;
    }
}