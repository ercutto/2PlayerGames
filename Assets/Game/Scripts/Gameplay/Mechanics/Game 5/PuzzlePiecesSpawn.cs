using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using Photon.Realtime;
namespace TwoPlayersGame
{
    public class PuzzlePiecesSpawn : MonoBehaviourPun
    {
        public GameObject[] spawnPosArray;
 
        public bool[] isEmpty;
        public GameObject[] CollectableObjects;
        private PhotonView pv;
        private GameObject spawnedPart;
        public TogetherWinScore togetherWinScore;
        public float spawnRate = 50;
        public GameObject[] spawnPos;
        public GameObject PuzzleObject;
        private bool canSpawn,firstone;
        // Start is called before the first frame update
        public void Start()
        {
            pv = GetComponent<PhotonView>();
            if (PhotonNetwork.IsMasterClient)
            {
                firstone = false;
                if (togetherWinScore.begin)
                {
                    if (!firstone) {
                        int spawnFrom = Random.Range(0, spawnPos.Length);
                        SpawnObjects(spawnFrom, 5);
                    }
                    
                    


                    SpawnMethod();

                }


            }

        }

        void Update()
        {
            if (!PhotonNetwork.IsMasterClient) { return; }
            else { if (!pv.IsMine) { return; } else { pv.RPC("SpawnPuzzleParts", RpcTarget.All); } }

        }
        public void SetBackTofalse()
        {
            foreach (var spawnpos in spawnPosArray)
            {
                spawnpos.GetComponent<IsEmptyOrFull>().IsEmpty = true;
            }
        }
      


        public void SpawnMethod()
        {
            StartCoroutine(nameof(SpawnCount));
        }
        #region PuzzelPiecessSpawn
        [PunRPC]
        void SpawnPuzzleParts()
        {
            foreach (var spawnposition in spawnPosArray)
            {
                //bool isEmpty = spawnposition.GetComponent<IsEmptyOrFull>().IsEmpty;

                for (int i = 0; i < spawnPosArray.Length; i++)
                {
                    bool isEmpty = spawnPosArray[i].GetComponent<IsEmptyOrFull>().IsEmpty;
                    if (isEmpty)              
                    {
                        canSpawn = true;

                        //spawnPosArray[i].GetComponent<IsEmptyOrFull>().IsEmpty = false;
                        if (canSpawn)
                        {
                            spawnedPart = PhotonNetwork.Instantiate(CollectableObjects[i].name, spawnPosArray[i].transform.position, spawnPosArray[i].transform.rotation);
                            spawnedPart.GetComponent<CollectableParts>().objectCount = spawnPosArray[i].GetComponent<IsEmptyOrFull>().PuzzlesNumber;
                            //pv.RPC("ChangeMesh", RpcTarget.All, i);
                            canSpawn = false;
                            spawnPosArray[i].GetComponent<IsEmptyOrFull>().IsEmpty=false;
                        }
                        else
                        {
                            return;
                        }
                    }
                    
                }
               
            }
            
        }
        #endregion spawn
        //[PunRPC]
        //public void ChangeMesh(int whichMesh)
        //{
        //    spawnedPart.GetComponent<MeshFilter>().mesh = PuzzlesRealMeshes[whichMesh];
        //}
 
        IEnumerator SpawnCount()
        {
            while (togetherWinScore.begin)
            {
               
                yield return new WaitForSeconds(spawnRate);

                int SpawnFrom = Random.Range(0, spawnPos.Length);
                SpawnObjects(SpawnFrom, 5);
            }

        }
        void SpawnObjects(int Pos, float speed)
        {
            if (PhotonNetwork.IsMasterClient)
                PhotonNetwork.Instantiate(PuzzleObject.name, spawnPos[Pos].transform.position, spawnPos[Pos].transform.rotation);
                firstone = true;
        }
        //public void RestartGame()
        //{

        //    //togetherWinScore.begin = true;
        //    StartCoroutine(SpawnCount());
        //}


    }
}

