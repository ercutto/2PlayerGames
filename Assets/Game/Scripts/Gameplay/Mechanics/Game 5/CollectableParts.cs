using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace TwoPlayersGame
{
    public class CollectableParts : MonoBehaviour
    {
        private PhotonView Pview;
        private Rigidbody rb;
        public int objectCount;
        private bool onHand,assembled;
        private Vector3 syncPos;
        private Quaternion syncRot;
        private Transform CaryObject,AssemblePlace,playerTransform;
        GameObject player, assemble;
      

        // Start is called before the first frame update
        void Start()
        {
            Pview = GetComponent<PhotonView>();
            if (Pview.IsMine)
            {
                rb = GetComponent<Rigidbody>();
                onHand = false;
                assembled = false;
               
            }
        }

        // Update is called once per frame
        void Update()
        {

            if (PhotonNetwork.IsMasterClient)
            {
                if (Pview.IsMine)
                {
                    if (onHand)
                    {

                        //transform.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z + 3);
                        transform.SetPositionAndRotation(player.transform.GetChild(1).transform.position, player.transform.GetChild(1).transform.rotation);
                        //transform.position = player.transform.GetChild(1).transform.position;
                        //transform.rotation = player.transform.GetChild(1).transform.rotation;
                        transform.localScale = new Vector3(1f, 1f, 1f);

                    }
                    else if (assembled)
                    {
                        //transform.position = AssemblePlace.position;
                        //transform.rotation = AssemblePlace.rotation;
                        transform.SetPositionAndRotation(AssemblePlace.position, AssemblePlace.rotation);
                        transform.localScale = new Vector3(3f, 3f, 3f);
                    }
                    else
                    {
                        return;
                    }
                }

            }
        }
        private void OnTriggerEnter(Collider other)
        {
            if(PhotonNetwork.IsMasterClient)
                if (Pview.IsMine)
                {
                    if (other.gameObject.CompareTag("EndObject"))
                    {
                        PhotonNetwork.Destroy(gameObject);
                    }
                    else if (!onHand && !assembled)
                    {
                        if (other.gameObject.CompareTag("Player"))
                        {
                            player = other.gameObject;
                            if(player.GetComponent<PlayersBools>().isFull == false) {
                                onHand = true;
                                //transform.root.networkView.RPC("CarController", RPCMode.AllBuffered, true, id);

                                player.GetComponent<PlayersBools>().isFull = true;
                                //OnOwnershipRequest(player);
                                CaryObject = other.gameObject.transform.GetChild(0).GetComponent<Transform>().transform;
                            }

                        }

                    }
                    else if (other.gameObject.CompareTag("Assemble"))
                    {
                        assemble = other.gameObject;
                        onHand = false;
                        assembled = true;
                        player.GetComponent<PlayersBools>().isFull = false ;
                        AssemblePlace = assemble.transform.GetChild(objectCount).GetComponent<Transform>().transform;
                        //OnOwnershipRequest(assemble);
                    }
                    else { return; }
                   

                    
                }
                else
                {
                    return;
                }

            

        }
 

        //public void OnOwnershipRequest(GameObject viewAndPlayer)
        //{

        //    Pview.TransferOwnership(viewAndPlayer.GetComponent<PhotonView>().ViewID);

        //    //if (Pview.TransferOwnership)
        //    //{
        //    //    view.TransferOwnership(requestingPlayer.ID);
        //    //}
        //}


    }
}
