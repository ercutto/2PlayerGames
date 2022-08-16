using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
namespace TwoPlayersGame
{
    public class PuzzlePiecesSpawn : MonoBehaviour
    {
        public Transform[] spawnPosArray;
        public bool[] isEmpty;
        public GameObject CollectableObjects;
        
        // Start is called before the first frame update
        void Start()
        {
            if(PhotonNetwork.IsMasterClient)
            foreach (var spawnposition in spawnPosArray)
            {
               bool isEmpty= spawnposition.GetComponent<IsEmptyOrFull>().IsEmpty;
                if (isEmpty)
                {
                   GameObject spawnedPart= PhotonNetwork.Instantiate(CollectableObjects.name, spawnposition.position, Quaternion.identity);
                    spawnedPart.GetComponent<CollectableParts>().objectCount = spawnposition.GetComponent<IsEmptyOrFull>().PuzzlesNumber;
                    isEmpty = false;
                }
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}

