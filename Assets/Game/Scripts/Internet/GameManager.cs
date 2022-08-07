using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

using Photon.Pun;
using Photon.Realtime;

namespace TwoPlayersGame
{
    public class GameManager : MonoBehaviourPunCallbacks
    {
        [Tooltip("For Player prefab")]
        public static GameManager gameManagerInstance;
        public GameObject playerPrefab;
        
       
        public float xPos, yPos;
        // Start is called before the first frame update
        void Start()
        {
            gameManagerInstance = this;
            if (playerPrefab==null)
            {
                Debug.LogErrorFormat("<color=orange>GameManager: </color><color=red> {0} prefab is Missing!", playerPrefab.name);
            }
            else
            {
                if (PlayerManager.localPlayerInstance == null) {
                    Debug.LogFormat("We are instantiating local player form {0}", SceneManagerHelper.ActiveSceneName);
                    //PhotonNetwork.Instantiate(playerPrefab.name, new Vector3(0f, 0.5f, 0f), Quaternion.identity,0);
                    Vector3 randPos = new Vector3(Random.Range(-xPos, xPos), 0.0f, Random.Range(-yPos, yPos));
                    PhotonNetwork.Instantiate(playerPrefab.name, randPos, Quaternion.identity);
                    
                }
                else
                {
                    Debug.LogFormat("Ignoring scene load for {0}", SceneManagerHelper.ActiveSceneName);
                }
                
            }
        }

        // Update is called once per frame
        void Update()
        {
            
        }
        public override void OnLeftRoom()
        {
            SceneManager.LoadScene("StartScene");
        }
        public void LeaveRoom()
        {
            Debug.LogFormat("<color=orange>GameManager: </color><color=yellow>You are Leaving the Room: {0} </color>",PhotonNetwork.CurrentRoom.Name.ToString());
            PhotonNetwork.LeaveRoom();
            
        }
        void LoadArena()
        {
            if (!PhotonNetwork.IsMasterClient)
            {
                Debug.LogError("<color=orange> GameManager : </color> You cannot load a level unless you are a master client!");
            }
            Debug.LogFormat("<color=orange>GameManager : </color><color=green>loading Level: {0}</color>" , PhotonNetwork.CurrentRoom.PlayerCount);
            //PhotonNetwork.LoadLevel("Room for " + PhotonNetwork.CurrentRoom.PlayerCount);
            PhotonNetwork.LoadLevel("Room for 1");
        }
        public override void OnPlayerEnteredRoom(Player other)
        {
            Debug.LogFormat("OnPlayerEntered() {0}", other.NickName);
            if (PhotonNetwork.IsMasterClient)
            {
                Debug.LogFormat("<color=orange>GameManager: </color> <color=aqua>OnPlayerEneteredRoom IsMAsterClient {0}</color>", PhotonNetwork.IsMasterClient);
                
                LoadArena();
               
            }
            
        }
        public override void OnJoinedRoom()
        {
            if (PhotonNetwork.IsMasterClient)
            {  
                //LoadGameArena();

            }
            else
            {
                return;
            }
            
        }
        public void LoadGameArena(string GameArenaName)
        {
            if (!PhotonNetwork.IsMasterClient) return; else PhotonNetwork.LoadLevel(GameArenaName);
            
        }
       
    }
}

