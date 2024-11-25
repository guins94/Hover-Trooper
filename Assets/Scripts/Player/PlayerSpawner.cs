using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] Player playerOnePrefab;
    [SerializeField] Player playerTwoPrefab;
    [SerializeField] Vector2[] spawnLocations = null;

    // Cached Components
    public Vector2[] SpawnLocations => spawnLocations;
    

    // Start is called before the first frame update
    public void CreateOnlinePlayer()
    {
        GameObject player = null;

        player = PhotonNetwork.Instantiate(playerOnePrefab.name, new Vector2(0, 0), Quaternion.identity);
        Player PlayerOne = player.GetComponent<Player>();
        GameManager.gameManagerInstance.playerOne = PlayerOne;
        PlayerOne.transform.position = ResolveLocation(PlayerOne.playerLocation);
        
    }

    public Vector2 ResolveLocation(PlayerLocation playerLocation)
    {
        if (playerLocation.floorOrSky == FloorOrSky.Floor)
        {
            if(playerLocation.IsAtFirstRoad()) return spawnLocations[0];
            if((int)playerLocation.playerRoad == (int)PlayerRoad.SecondRoad) return spawnLocations[1];
            if((int)playerLocation.playerRoad == (int)PlayerRoad.ThirdRoad) return spawnLocations[2];
        }
        else
        {
            if(playerLocation.playerRoad == PlayerRoad.FirstRoad) return spawnLocations[3];
            if(playerLocation.playerRoad == PlayerRoad.SecondRoad) return spawnLocations[4];
            if(playerLocation.playerRoad == PlayerRoad.ThirdRoad) return spawnLocations[5];
        }
        return Vector2.zero;
    }
}
