using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace TwoPlayersGame
{
    public class SpawnPlayers : MonoBehaviour
    {
        public GameObject PlayerPrefab;
        public float xPos,yPos;
        // Start is called before the first frame update
        void Start()
        {
            Vector3 randPos = new Vector3(Random.Range(-xPos, xPos), 0.0f, Random.Range(-yPos, yPos));
            PhotonNetwork.Instantiate(PlayerPrefab.name, randPos, Quaternion.identity);
        }

      
    }
}

