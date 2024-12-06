using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] PhotonView playerView;
    public PlayerLocation playerLocation = null;
    public PlayerLocation nextPlayerLocation = new PlayerLocation(FloorOrSky.None, PlayerRoad.None);

    //Cached Components
    private PlayerOneController playerControls;
    private bool changingLocation = false;
    public PhotonView PlayerView => playerView;

    public void Awake()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            playerLocation = new PlayerLocation();
            GameManager.gameManagerInstance.freeSpaceController.UseLocation(playerLocation);
        }
        else
        {
            playerLocation = new PlayerLocation(FloorOrSky.Sky, PlayerRoad.SecondRoad);
            //GameManager.gameManagerInstance.freeSpaceController.AskSyncWithMaster(playerLocation.floorOrSky, playerLocation.playerRoad);
            //GameManager.gameManagerInstance.freeSpaceController.UseLocation(playerLocation);
        }
        
    
        playerControls = new PlayerOneController();
        playerControls.Enable();
    }

    private void OnEnable()
    {

    }

    private void OnDisable()
    {

    }

    // Start is called before the first frame update
    public void Start()
    {
        if(playerView.IsMine)
        {
            StartCoroutine(CheckForPlayerChangeLocation());
        }
    }

    private IEnumerator CheckForPlayerChangeLocation()
    {
        PlayerLocation initialLocation = nextPlayerLocation;
        yield return new WaitUntil(() => nextPlayerLocation != initialLocation);

        changingLocation = true;
        yield return new WaitUntil(() => ChangePlayerLocation());
        yield return new WaitForSeconds(2f);
        changingLocation = false;

        bool ChangePlayerLocation()
        {
            Vector3 newPosition = GameManager.gameManagerInstance.PlayerSpawner.ResolveLocation(nextPlayerLocation);
            gameObject.transform.position = new Vector2(newPosition.x, newPosition.y);
            playerLocation = nextPlayerLocation;
            nextPlayerLocation = null;
            
            StartCoroutine(CheckForPlayerChangeLocation());
            return true;
        }
    }

    

    // Update is called once per frame
    public void Update()
    {
        if(playerView.IsMine && !changingLocation)
        {
            Move();
        }

        //SendMyLocation();
    }

    public void Move()
    {
        Vector2 move = Vector2.zero;
        if (playerControls != null) move = playerControls.PlayerOne.Move.ReadValue<Vector2>();

        PlayerLocation shouldChangeLocation = null;
        if (move.x < 0) shouldChangeLocation = GameManager.gameManagerInstance.freeSpaceController.CanGoLeft(playerLocation);
        if (move.x > 0) shouldChangeLocation = GameManager.gameManagerInstance.freeSpaceController.CanGoRight(playerLocation);
        if(shouldChangeLocation == null) return;
        nextPlayerLocation = shouldChangeLocation;
    }
}
