using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
namespace TwoPlayersGame
{
    
    public class CreateAndJoinToRooms : MonoBehaviourPunCallbacks
    {
        public TMP_InputField creatingRoomInput,joinInput;

        public void CreateRoom()
        {
            PhotonNetwork.CreateRoom(creatingRoomInput.text);
        }
        public void JoinRoom()
        {
            PhotonNetwork.JoinRoom(joinInput.text);
        }
        public override void OnJoinedRoom()
        {
            PhotonNetwork.LoadLevel("Game");
        }
    }
}

