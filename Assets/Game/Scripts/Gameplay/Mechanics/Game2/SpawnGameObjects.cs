using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace TwoPlayersGame
{
    public class SpawnGameObjects : MonoBehaviourPun
    {
        public bool begin;
        public GameObject obstacleOrCoin;
        public GameObject[] SpawnPos;
        private PhotonView spawnObjectPhotonView;
      
        // Start is called before the first frame update
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
            }
      
            
            
        }

        // Update is called once per frame
        void Update()
        {

        }
        IEnumerator SpawnCount()
        {
            while(begin)
            { yield return new WaitForSeconds(1);
                
                int SpawnFrom = Random.Range(0,SpawnPos.Length);
                SpawnObjects(SpawnFrom, 5);
            }
            
        }
        void SpawnObjects(int Pos, float speed)
        {
            if(PhotonNetwork.IsMasterClient)
            PhotonNetwork.Instantiate(obstacleOrCoin.name, SpawnPos[Pos].transform.position, SpawnPos[Pos].transform.rotation);
        }
    }
}

