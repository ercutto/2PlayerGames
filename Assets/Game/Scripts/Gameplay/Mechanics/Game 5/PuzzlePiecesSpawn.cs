using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
namespace TwoPlayersGame
{
    public class PuzzlePiecesSpawn : MonoBehaviour
    {
        public GameObject[] spawnPosArray;
        public bool[] isEmpty;
        public GameObject CollectableObjects;
        
        // Start is called before the first frame update
        void Start()
        {
            //if (PhotonNetwork.IsMasterClient)
            //{
            //    SpawnPuzzleParts();
            //}
        }

        void Update()
        {
            if (PhotonNetwork.IsMasterClient)
            {
                SpawnPuzzleParts();
            }

        }
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
                        GameObject spawnedPart = PhotonNetwork.Instantiate(CollectableObjects.name, spawnPosArray[i].transform.position, spawnPosArray[i].transform.rotation);
                        spawnedPart.GetComponent<CollectableParts>().objectCount = spawnPosArray[i].GetComponent<IsEmptyOrFull>().PuzzlesNumber;
                        spawnPosArray[i].GetComponent<IsEmptyOrFull>().IsEmpty = false;
                       
                    }
                }
               
            }
            
        }
        
        
    }
}

