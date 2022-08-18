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
                if (spawnposition.GetComponent<IsEmptyOrFull>().IsEmpty==false)
                {
                    return;
                   
                }
                else
                {
                    GameObject spawnedPart = PhotonNetwork.Instantiate(CollectableObjects.name, spawnposition.transform.position, spawnposition.transform.rotation);
                    spawnedPart.GetComponent<CollectableParts>().objectCount = spawnposition.GetComponent<IsEmptyOrFull>().PuzzlesNumber;
                    spawnposition.GetComponent<IsEmptyOrFull>().IsEmpty = false;
                }
            }
        }
        
        
    }
}

