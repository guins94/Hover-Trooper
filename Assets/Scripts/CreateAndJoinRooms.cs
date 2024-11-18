using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class CreateAndJoinRooms : MonoBehaviourPunCallbacks
{
    public InputField creaturInput;
    public InputField joinInput;

    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom("test");
    }

    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom("test");
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("SCENE_City");
    }
}
