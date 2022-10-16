using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace TwoPlayersGame
{
    public class CollectableParts : MonoBehaviour
    {
        public Vector3 sizeOnTable = new Vector3(0.5f, 0.5f, 0.5f);
        public Vector3 sizeOnHand = new Vector3(0.5f, 0.5f, 0.5f);
        public Vector3 sizeOnAssembled = new Vector3(1.5f, 1.5f, 1.5f);
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
                transform.localScale = sizeOnTable;
            }
        }

        // Update is called once per frame
        void Update()
        {

            if (PhotonNetwork.IsMasterClient)
            {
                if (Pview.IsMine)
                {
                    if (!GameObject.Find("Score").GetComponent<TogetherWinScore>().begin)
                    {
                        PhotonNetwork.Destroy(gameObject);
                        return;
                    }
                    else
                    {
                        if (onHand)
                        {
                            if (player != null)
                            {  //transform.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z + 3);
                                transform.SetPositionAndRotation(player.transform.GetChild(1).transform.position, player.transform.GetChild(1).transform.rotation);
                                //transform.position = player.transform.GetChild(1).transform.position;
                                //transform.rotation = player.transform.GetChild(1).transform.rotation;
                                transform.localScale = sizeOnHand;
                            }
                            else
                            {
                                onHand = false;
                                transform.position = transform.position;
                                transform.localScale = sizeOnTable;
                            }
                           
                           
                           
                        }
                        else if (assembled)
                        {
                            //transform.position = AssemblePlace.position;
                            //transform.rotation = AssemblePlace.rotation;
                            transform.SetPositionAndRotation(AssemblePlace.position, AssemblePlace.rotation);
                            transform.localScale = sizeOnAssembled;
                        }
                        else
                        {
                            return;
                        }
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
                           
                           
                            if (player.GetComponent<PlayersBools>().isFull == false && player.GetComponent<PlayersBools>().interacting)
                            {
                                onHand = true;
                                //transform.root.networkView.RPC("CarController", RPCMode.AllBuffered, true, id);

                                player.GetComponent<PlayersBools>().isFull = true;
                                //OnOwnershipRequest(player);
                                CaryObject = other.gameObject.transform.GetChild(0).GetComponent<Transform>().transform;
                            
                            }
                            else
                            {
                                transform.position = transform.position;
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

        private void OnTriggerStay(Collider other)
        {
            if (PhotonNetwork.IsMasterClient)
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
                            if (player != null)
                            {

                                if (player.GetComponent<PlayersBools>().isFull == false && player.GetComponent<PlayersBools>().interacting)
                                {
                                    onHand = true;
                                    //transform.root.networkView.RPC("CarController", RPCMode.AllBuffered, true, id);

                                    player.GetComponent<PlayersBools>().isFull = true;
                                    //OnOwnershipRequest(player);
                                    CaryObject = other.gameObject.transform.GetChild(0).GetComponent<Transform>().transform;

                                }

                                else
                                {
                                    transform.position = transform.position;
                                }
                            }
                           
                        }

                    }
                    else if (other.gameObject.CompareTag("Assemble"))
                    {
                        if (player != null)
                        {
                            
                            assemble = other.gameObject;
                            if(assemble.GetComponent<PuzzelMove>().allbools[objectCount] == false)
                            {
                                onHand = false;
                                assembled = true;
                                player.GetComponent<PlayersBools>().isFull = false;
                                AssemblePlace = assemble.transform.GetChild(objectCount).GetComponent<Transform>().transform;
                            }
                            
                            //OnOwnershipRequest(assemble);
                        }
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
        


    }
}
