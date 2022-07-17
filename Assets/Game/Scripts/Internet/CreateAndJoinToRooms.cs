using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Photon.Realtime;
namespace TwoPlayersGame
{
    
    public class CreateAndJoinToRooms : MonoBehaviourPunCallbacks
    {
        public TMP_InputField creatingRoomInput,joinInput;

        //public void CreateRoom()
        //{  
        //    PhotonNetwork.CreateRoom(creatingRoomInput.text,default);
        
        //}
        //public void JoinRoom()
        //{
        //    PhotonNetwork.JoinRoom(joinInput.text);
        //}
        //public override void OnJoinedRoom()
        //{
        //    PhotonNetwork.LoadLevel("Game");
        //}
        //public override void OnCreateRoomFailed(short returnCode, string message)
        //{
        //    print("CreateRoom Failed" + returnCode + " mesagge: " + message);
        //}
    }
}

