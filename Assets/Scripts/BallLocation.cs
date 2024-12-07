using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallLocation : MonoBehaviour
{
    private PlayerLocation playerLocation = null;

    private void Awake()
    {
        playerLocation = new PlayerLocation();
    }

    public PlayerLocation GetBallLocation()
    {
        return playerLocation;
    }

    public void SetBallLocation(PlayerLocation location)
    {
        this.playerLocation.floorOrSky = location.floorOrSky;
        this.playerLocation.playerRoad = location.playerRoad;
    }

    public void DestroyBall()
    {
        Destroy(gameObject);
    }
}
