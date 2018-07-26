using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{

    [SerializeField] Portal connectedPortal;

    Player currentPlayer;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && currentPlayer != null)
            currentPlayer.transform.position = connectedPortal.transform.position;
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.GetComponent<Player>();
        if (!player)
            return;

        currentPlayer = player;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        currentPlayer = null;
    }
}
