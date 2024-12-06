using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerLocation
{
    public PlayerRoad playerRoad = PlayerRoad.SecondRoad;
    public FloorOrSky floorOrSky = FloorOrSky.Floor;

    public PlayerLocation()
    {
        this.floorOrSky = FloorOrSky.Floor;
        this.playerRoad = PlayerRoad.SecondRoad;
    }

    public PlayerLocation(FloorOrSky level, PlayerRoad road)
    {
        this.floorOrSky = level;
        this.playerRoad = road;
    }
/*
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // This is executed on the owner. Send data to others.
            stream.SendNext((int)floorOrSky);
            stream.SendNext((int)playerRoad);
        }
        else
        {
            // This is executed on remote clients. Receive data.
            floorOrSky = (FloorOrSky)(int)stream.ReceiveNext();
            playerRoad = (PlayerRoad)(int)stream.ReceiveNext();
        }
    }
*/
    public static bool CompareLocation(PlayerLocation otherPlayerLocation, PlayerLocation currentLocation)
    {
        if(otherPlayerLocation.playerRoad == currentLocation.playerRoad && otherPlayerLocation.floorOrSky == currentLocation.floorOrSky)  return true;
        else return false;
    }

    public bool CompareLocation(PlayerLocation otherPlayerLocation)
    {
        if(otherPlayerLocation.playerRoad == this.playerRoad && otherPlayerLocation.floorOrSky == this.floorOrSky)  return true;
        else return false;
    }

    public bool CompareRoad(PlayerLocation otherPlayerLocation)
    {
        if(otherPlayerLocation.playerRoad == this.playerRoad)  return true;
        else return false;
    }

    public bool CompareFloorOrSky(PlayerLocation otherPlayerLocation)
    {
        if(otherPlayerLocation.floorOrSky == this.floorOrSky)  return true;
        else return false;
    }

    public bool IsFlying()
    {
        if (floorOrSky == FloorOrSky.Sky) return true;
        else return false;
    }

    public bool IsGrounded()
    {
        if (floorOrSky == FloorOrSky.Floor) return true;
        else return false;
    }

    public bool IsAtNoneFloorOrSky()
    {
        if (floorOrSky == FloorOrSky.None) return true;
        else return false;
    }

    public bool IsAtFirstRoad()
    {
        if (playerRoad == PlayerRoad.FirstRoad) return true;
        else return false;
    }

    public bool IsAtSecondRoad()
    {
        if (playerRoad == PlayerRoad.SecondRoad) return true;
        else return false;
    }

    public bool IsAtThirdRoad()
    {
        if (playerRoad == PlayerRoad.ThirdRoad) return true;
        else return false;
    }

    public bool IsAtNoneRoad()
    {
        if (playerRoad == PlayerRoad.None) return true;
        else return false;
    }

    public bool IsNone()
    {
        if (playerRoad == PlayerRoad.None && floorOrSky == FloorOrSky.None) return true;
        else return false;
    }
}
