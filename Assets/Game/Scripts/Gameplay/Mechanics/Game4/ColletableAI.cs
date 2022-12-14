using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Photon.Pun;
namespace TwoPlayersGame
{
    public class ColletableAI : MonoBehaviour
    {
        private NavMeshAgent nav;
        public GameObject[] wayPoints;
        private PhotonView pv;
        private BoxCollider coll;

        
        int pointIndex = 0;
        Vector3 nextwayPoint;
        

        
        // Start is called before the first frame update
        void Start()
        {
            pv = GetComponent<PhotonView>();
            if (pv.IsMine)
            {
                FindNextPoint();
                nav = GetComponent<NavMeshAgent>();
                coll = GetComponent<BoxCollider>();
                SetDes();
            }
           
        }

        // Update is called once per frame
        void Update()
        {
            if (pv.IsMine)
            {
                if (Vector3.Distance(transform.position, nextwayPoint) < 1f)
                {
                    FindNextPoint();
                    SetDes();
                }
            }
               
            
            
        }
        void SetDes()
        {
            if (pv.IsMine)
            {
                nextwayPoint = wayPoints[pointIndex].transform.position;
                nav.SetDestination(nextwayPoint);

            }

        }
        void FindNextPoint()
        {
            if(pv.IsMine)pointIndex = Random.Range(1, wayPoints.Length);
           
        }
        public void OnTriggerEnter(Collider other)
        {
            if (pv.IsMine)
            {
                if (other.gameObject.CompareTag("Player"))
                {
                    coll.enabled = false;
                    gameObject.GetComponent<MeshRenderer>().material.color = Color.magenta;
                    StartCoroutine(nameof(CollBack));

                }

            }
            
        }
        IEnumerator CollBack()
        {
            yield return new WaitForSeconds(3);
            coll.enabled = true;
            gameObject.GetComponent<MeshRenderer>().material.color = Color.blue;



        }
    }

}

