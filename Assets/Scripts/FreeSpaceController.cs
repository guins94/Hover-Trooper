using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class FreeSpaceController : MonoBehaviour
{
    [SerializeField] PhotonView photonView;
    private LocationPrinter printer = new LocationPrinter();
    public List<PlayerLocation> usedPlayerLocations = new List<PlayerLocation>();

    public bool CheckForPlayerLocation(PlayerLocation location)
    {
        for (int i = 0; i <= usedPlayerLocations.Count - 1; i++)
        {
            if (usedPlayerLocations[i] == location) return true;
        }
        return false;
    }

    public bool CheckPlayerRoad(PlayerRoad playerRoad)
    {
        for (int i = 0; i <= usedPlayerLocations.Count - 1; i++)
        {
            if (usedPlayerLocations[i].playerRoad == playerRoad) return true;
        }
        return false;
    }

    public PlayerLocation CanGoLeft(PlayerLocation location)
    {
        Debug.Log("location " + location.playerRoad);
        if(location.playerRoad == PlayerRoad.FirstRoad) return null;
        if(location.playerRoad == PlayerRoad.SecondRoad)
        {
            PlayerLocation bottomPlayerLocation = new PlayerLocation(FloorOrSky.Floor, PlayerRoad.FirstRoad);
            if(!CheckForPlayerLocation(bottomPlayerLocation))
            {
                PlayerJumpResolver(location, bottomPlayerLocation);
                return bottomPlayerLocation;
            } 
            else
            {
                bottomPlayerLocation.floorOrSky = FloorOrSky.Sky;
                if(!CheckForPlayerLocation(bottomPlayerLocation))
                {
                    PlayerJumpResolver(location, bottomPlayerLocation);
                    return bottomPlayerLocation;
                } 
                else return null;
            }
            
        }
        if(location.playerRoad == PlayerRoad.ThirdRoad)
        {
            PlayerLocation middlePlayerLocation = new PlayerLocation(FloorOrSky.Floor, PlayerRoad.SecondRoad);
            if(!CheckForPlayerLocation(middlePlayerLocation))
            {
                PlayerJumpResolver(location, middlePlayerLocation);
                return middlePlayerLocation;
            } 
            else
            {
                middlePlayerLocation.floorOrSky = FloorOrSky.Sky;
                if(!CheckForPlayerLocation(middlePlayerLocation))
                {
                    PlayerJumpResolver(location, middlePlayerLocation);
                    return middlePlayerLocation;
                } 
                else return null;
            }
        }
        return null;
    }

    public void PlayerJumpResolver(PlayerLocation currentLocation, PlayerLocation nextPlayerLocation)
    {
        FreeLocation(currentLocation);
        UseLocation(nextPlayerLocation);

    }

    public void UseLocation(PlayerLocation location)
    {
        if(CheckForPlayerLocation(location)) return;
        usedPlayerLocations.Add(location);
        photonView.RPC("SyncUsedPlayerLocations", RpcTarget.Others, location);
    }

    public void FreeLocation(PlayerLocation location)
    {
        if(!CheckForPlayerLocation(location)) return;
        usedPlayerLocations.Remove(location);
        photonView.RPC("RemoveLocationFromUsed", RpcTarget.Others, location);
    }

    // RPC to sync the added location with other players
    [PunRPC]
    private void SyncUsedPlayerLocations(PlayerLocation location)
    {
        if (!CheckForPlayerLocation(location))
        {
            usedPlayerLocations.Add(location);
        }
    }

    // RPC to sync the removal of a location with other players
    [PunRPC]
    private void RemoveLocationFromUsed(PlayerLocation location)
    {
        if (CheckForPlayerLocation(location))
        {
            usedPlayerLocations.Remove(location);
        }
    }
}
