using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Plane : MonoBehaviour
{
    [SerializeField] Rigidbody2D rigidbody2D;
    [SerializeField] PhotonView playerView;
    [SerializeField] SpriteRenderer planeSpriteRenderer;
    
    [SerializeField] private float moveSpeed = 5f; // Speed of movement

    //Cached Components
    public float planeVelocity => moveSpeed;


    public float planeXSize => planeSpriteRenderer.sprite.bounds.size.x * transform.localScale.x;
    //public float planeMinX => -planeSpriteRenderer.sprite.bounds.size.x * transform.localScale.x;

    public void PlaneMovement(Vector2 movement)
    {
        Vector2 playerVelocity = new Vector2(movement.x * moveSpeed * Time.deltaTime, 0);
        rigidbody2D.velocity =  playerVelocity;
    }

    public void StopMovement()
    {
        Debug.Log("STOP");
        rigidbody2D.velocity = new Vector2(0, 0);
    }

    public void MovePlaneLeft()
    {
        // Logic to move the plane to the left
        Debug.Log("Moving plane left.");
        Vector2 playerVelocity = new Vector2(-1 * moveSpeed * Time.deltaTime, 0);
        rigidbody2D.velocity = playerVelocity;
    }

    public void MovePlaneRight()
    {
        // Logic to move the plane to the right
        Debug.Log("Moving plane right.");
        Vector2 playerVelocity = new Vector2(1 * moveSpeed * Time.deltaTime, 0);
        rigidbody2D.velocity =  playerVelocity;
    }
}
    

