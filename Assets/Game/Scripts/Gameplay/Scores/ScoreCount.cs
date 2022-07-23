using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace TwoPlayersGame
{
    public class ScoreCount : MonoBehaviour// IPunObservable
    {
        public Text ScoreBoard;
        private int score;
        private PhotonView photonView;
        private BoxCollider boxCollider;
        public Vector3 ballStartPos;
        public bool Goal;
        public bool canGoal;
        private float count;
        

        // Start is called before the first frame update
        void Start()
        {
            boxCollider = GetComponent<BoxCollider>();
            Goal = false;
            score = 0;
            photonView = GetComponent<PhotonView>();
            ballStartPos = new Vector3(0, 0.5f, 0);
            
        }

        // Update is called once per frame
        void Update()
        {
            count += Time.deltaTime;
            if (count > 2)
            {
                canGoal = true;
            }
            else
            {
                canGoal = false;
            }
            
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Ball"))
            {
                //boxCollider.enabled = false;
               
                //photonView.RPC("ScoreChange", RpcTarget.All);
                ScoreChange();
            }
        }

       

        //[PunRPC]
        void ScoreChange()
        {
            if (canGoal == false)
            {
                return;
            }
            score++;
            ScoreBoard.text = score.ToString();
            count = 0;
        }

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {

                stream.SendNext(score);
                //stream.SendNext(ballStartPos);
                stream.SendNext(canGoal);
                
            }
            else
            {
                score = (int)stream.ReceiveNext();
                //ballStartPos = (Vector3)stream.ReceiveNext();
                //Goal = (bool)stream.ReceiveNext();
                canGoal=(bool)stream.ReceiveNext(); 
            }
        }
    }
}
