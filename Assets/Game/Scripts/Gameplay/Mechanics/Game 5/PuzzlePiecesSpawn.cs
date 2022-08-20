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
        
        // Start is called before the first frame update
        void Start()
        {
            pv = GetComponent<PhotonView>();
            //if (PhotonNetwork.IsMasterClient)
            //{
            //    SpawnPuzzleParts();
            //}
        }

        void Update()
        {
            if (!PhotonNetwork.IsMasterClient)
            {
                return;
            }
            else
            {
                if (!pv.IsMine)
                {

                    return;
                }
                else
                {
                    pv.RPC("SpawnPuzzleParts", RpcTarget.All);

                    //SpawnPuzzleParts();
                }
            }

        }
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
                        spawnedPart = PhotonNetwork.Instantiate(CollectableObjects[i].name, spawnPosArray[i].transform.position, spawnPosArray[i].transform.rotation);
                        spawnedPart.GetComponent<CollectableParts>().objectCount = spawnPosArray[i].GetComponent<IsEmptyOrFull>().PuzzlesNumber;
                        //pv.RPC("ChangeMesh", RpcTarget.All, i);
                        spawnPosArray[i].GetComponent<IsEmptyOrFull>().IsEmpty = false;

                    }
                }
               
            }
            
        }
        //[PunRPC]
        //public void ChangeMesh(int whichMesh)
        //{
        //    spawnedPart.GetComponent<MeshFilter>().mesh = PuzzlesRealMeshes[whichMesh];
        //}



    }
}

