using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace TwoPlayersGame
{
    public class SpawnGameObjects : MonoBehaviourPun
    {
        public bool begin;
        public int spawnRate = 1;
        public GameObject obstacleOrCoin;
        public GameObject[] SpawnPos;
        private PhotonView spawnObjectPhotonView;
        //for restart
        public GameObject restartButton,leaveGameModbutton;
        public GameObject scoreBoardBlue,scoreBoardRed;

        void Start()
        {
            spawnObjectPhotonView = GetComponent<PhotonView>();
            begin = true;

            if (PhotonNetwork.IsMasterClient)
            {
                if (begin)
                    {
                        StartCoroutine(SpawnCount());
                    }

                leaveGameModbutton.SetActive(true);

            }
            else
            {
                leaveGameModbutton.SetActive(false);
            }

            restartButton.SetActive(false);

        }


        void Update()
        {
           
        }
        IEnumerator SpawnCount()
        {
            while(begin)
            { yield return new WaitForSeconds(spawnRate);
                
                int SpawnFrom = Random.Range(0,SpawnPos.Length);
                SpawnObjects(SpawnFrom, 5);
            }
            
        }
        void SpawnObjects(int Pos, float speed)
        {
            if(PhotonNetwork.IsMasterClient)
             PhotonNetwork.Instantiate(obstacleOrCoin.name, SpawnPos[Pos].transform.position, SpawnPos[Pos].transform.rotation);
        }
        public void RestartGame()
        {
      
            begin = true;
            StartCoroutine(SpawnCount());
        }
       
    }
}

