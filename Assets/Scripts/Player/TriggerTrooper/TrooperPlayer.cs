using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.InputSystem;

public class TrooperPlayer : MonoBehaviour
{
    [SerializeField] PhotonView playerView;
    [SerializeField] Rigidbody2D rigidBody2D;
    [SerializeField] Plane plane;
    [SerializeField] private float moveSpeed = 5f; // Speed of movement
    [SerializeField] SpriteRenderer playerSpriteRenderer;

    //Cached Components
    private PlayerOneController playerControls;
    public PhotonView PlayerView => playerView;
    public float playerXSize => playerSpriteRenderer.sprite.bounds.size.x * transform.localScale.x;
    public float playerXDistance => transform.position.x + playerXSize;
    public float playerMaxXDistance => plane.transform.position.x + plane.planeXSize;
    public float playerYDistance => transform.position.x - playerXSize;
    public float playerMaxYDistance => plane.transform.position.x - plane.planeXSize;

    // ReachedCorner Coroutine
    Coroutine leftWindCoroutine = null;
    Coroutine rightWindCoroutine = null;


    // Start is called before the first frame update
    void Start()
    {
        playerControls = new PlayerOneController();
        playerControls.Enable();
        playerControls.PlayerOne.Move.started += OnMoveStarted;
        playerControls.PlayerOne.Move.performed += OnMovePerformed;
        playerControls.PlayerOne.Move.canceled += OnMoveCanceled;
    }

    void OnDisable()
    {
        playerControls.PlayerOne.Move.started -= OnMoveStarted;
        playerControls.PlayerOne.Move.performed -= OnMovePerformed;
        playerControls.PlayerOne.Move.canceled -= OnMoveCanceled;
    }

    void OnMoveStarted(InputAction.CallbackContext context)
    {
        Debug.Log("Started");
    }

    void OnMovePerformed(InputAction.CallbackContext context)
    {
        Debug.Log("Performed");

        Vector2 move = Vector2.zero;
        if (playerControls != null) move = playerControls.PlayerOne.Move.ReadValue<Vector2>();

        if (move.x < 0)
        {
            if (playerYDistance <= playerMaxYDistance) 
            {
                plane.MovePlaneLeft();
                MovePlayerLeft(plane.planeVelocity);
            }
            else
            {
                MovePlayerLeft(moveSpeed);
                CheckIfReachedLeft();
            }
        }
        else if (move.x > 0)
        {
            if (playerXDistance >= playerMaxXDistance) 
            {
                plane.MovePlaneRight();
                MovePlayerRight(plane.planeVelocity);
            }
            else
            {
                MovePlayerRight(moveSpeed);
                CheckIfReachedRight();
            }
        }
    }

    private void CheckIfReachedRight()
    {
        if (rightWindCoroutine != null) StopCoroutine(rightWindCoroutine);
        rightWindCoroutine = StartCoroutine(PlayerReachedRight());

        IEnumerator PlayerReachedRight()
        {
            yield return new WaitUntil(() => (playerXDistance >= playerMaxXDistance));
            rigidBody2D.velocity = new Vector2(plane.planeVelocity * Time.deltaTime, rigidBody2D.velocity.y);
            plane.MovePlaneRight();
        }
    }

    private void CheckIfReachedLeft()
    {
        if (leftWindCoroutine != null) StopCoroutine(leftWindCoroutine);
        leftWindCoroutine = StartCoroutine(PlayerReachedLeft());

        IEnumerator PlayerReachedLeft()
        {
            yield return new WaitUntil(() => (playerYDistance <= playerMaxYDistance));
            rigidBody2D.velocity = new Vector2(-1 * plane.planeVelocity * Time.deltaTime, rigidBody2D.velocity.y);
            plane.MovePlaneLeft();
        }
    }

    void OnMoveCanceled(InputAction.CallbackContext context)
    {
        Debug.Log("Canceled");
        rigidBody2D.velocity = new Vector2(0, rigidBody2D.velocity.y);
        plane.StopMovement();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        // Read the movement input
        Vector2 moveInput = context.ReadValue<Vector2>();
        Debug.Log("" + moveInput);
    }

    private void MovePlayerLeft(float velocity)
    {
        // Logic to move the player to the left
        Debug.Log($"Moving player left with velocity {velocity}.");
        rigidBody2D.velocity = new Vector2(-1 * velocity * Time.deltaTime, rigidBody2D.velocity.y);
    }

    private void MovePlayerRight(float velocity)
    {
        // Logic to move the player to the right
        Debug.Log($"Moving player right with velocity {velocity}.");
        rigidBody2D.velocity = new Vector2(1 * velocity * Time.deltaTime, rigidBody2D.velocity.y);
    }
}