using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
namespace TwoPlayersGame
{
    public class PlayerManager : MonoBehaviourPunCallbacks,IPunObservable
    {
        public static GameObject localPlayerInstance;
        public  Color myColor;
        public Text Indicator;
        private int playerLayer=15;
        private int ignoreLayer=20;
        public int FirstOrSecond;
        private Rigidbody rb;
        public GameObject[] myGarphics;
        public GameObject[] ToSetActiveOrFalse;

        // Start is called before the first frame update
        public void Awake()
        {
            rb = GetComponent<Rigidbody>();
            rb.useGravity = true;
            if (photonView.IsMine)
            {
               
                PlayerManager.localPlayerInstance = this.gameObject;
               // gameObject.GetComponentInChildren<Renderer>().material.color = myColor;
                foreach (var item in myGarphics)
                {
                    item.GetComponent<Renderer>().material.color = myColor;
                }
                foreach (var obj in ToSetActiveOrFalse)
                {
                    obj.SetActive(false);
                }
            }
            DontDestroyOnLoad(this.gameObject);
        }
        void Start()
        {
           
#if UNITY_5_4_OR_NEWER
            // Unity 5.4 has a new scene management. register a method to call CalledOnLevelWasLoaded.
            UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
#endif
        }

        // Update is called once per frame
        void Update()
        {
            if (photonView.IsMine && PhotonNetwork.IsMasterClient)
            {
                FirstOrSecond = 1;
                
            }else if (photonView.IsMine && !PhotonNetwork.IsMasterClient)
            {
                FirstOrSecond = 2;
            }
            Indicator.text = FirstOrSecond.ToString();
        }
        
#if !UNITY_5_4_OR_NEWER
        <summary>See CalledOnLevelWasLoaded. Outdated in Unity 5.4.</summary>
        void OnLevelWasLoaded(int level)
        {
            this.CalledOnLevelWasLoaded(level);
        }
#endif
#if UNITY_5_4_OR_NEWER
        void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode loadingMode)
        {
            this.CalledOnLevelWasLoaded(scene.buildIndex);
        }
#endif
        void CalledOnLevelWasLoaded(int level)
        {
            // check if we are outside the Arena and if it's the case, spawn around the center of the arena in a safe zone
            //float randx = Random.Range(0, 3);
            //float randy = Random.Range(0, 3);
            //transform.position = new Vector3(randx, 0, randy);
            SwitchPosForGame(level);
        }

#if UNITY_5_4_OR_NEWER
        public override void OnDisable()
        {
            // Always call the base to remove callbacks
            base.OnDisable();
            UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnSceneLoaded;
        }
        void SwitchPosForGame(int SceneNumber)
        {
            switch (SceneNumber)
            {
                case 1:
                    gameObject.layer = playerLayer;
                    ToSetActiveOrFalse[0].SetActive(false);
                    ToSetActiveOrFalse[1].SetActive(false);
                    ToSetActiveOrFalse[2].SetActive(false);
                    rb.useGravity = true;
                    if (PhotonNetwork.IsMasterClient) PosTransform(2, 0, 0);
                    else PosTransform(-2, 0, 0);
                    break;
                case 5:
                    gameObject.layer = playerLayer;
                    ToSetActiveOrFalse[0].SetActive(false);
                    ToSetActiveOrFalse[1].SetActive(false);
                    ToSetActiveOrFalse[2].SetActive(false);
                    rb.useGravity = true;
                    if (PhotonNetwork.IsMasterClient) PosTransform(2, 0, 0);
                    else PosTransform(-2, 0, 0);
                    break;
                case 6:
                    ToSetActiveOrFalse[1].SetActive(false);
                    ToSetActiveOrFalse[2].SetActive(false);
                    rb.useGravity = true;
                    if (PhotonNetwork.IsMasterClient)
                    {
                        PosTransform(2, 0, 0); transform.eulerAngles = new Vector3(0, 0, 0).normalized; gameObject.layer = ignoreLayer;
                        ToSetActiveOrFalse[0].SetActive(true);
                    }
                    else { PosTransform(-2, 0, 0); transform.eulerAngles = new Vector3(0, 0, 0).normalized; gameObject.layer = ignoreLayer; ToSetActiveOrFalse[0].SetActive(true); }
                    break;
                case 8:
                    rb.useGravity = true;
                    ToSetActiveOrFalse[0].SetActive(false);
                    ToSetActiveOrFalse[1].SetActive(false);
                    ToSetActiveOrFalse[2].SetActive(true);
                    if (PhotonNetwork.IsMasterClient)
                    {
                        PosTransform(2, 0, 0); transform.eulerAngles = new Vector3(0, 0, 0).normalized; gameObject.layer = ignoreLayer;
                      
                    }
                    else { PosTransform(-2, 0, 0); transform.eulerAngles = new Vector3(0, 0, 0).normalized; gameObject.layer = ignoreLayer;
                       
                    }
                    break;
                case 10:
                    rb.useGravity = false;
                    ToSetActiveOrFalse[1].SetActive(false);
                    ToSetActiveOrFalse[2].SetActive(false);
                    if (PhotonNetwork.IsMasterClient)
                    {   
                        PosTransform(0, 2, 0); transform.eulerAngles = new Vector3(0, 0, 0).normalized; gameObject.layer = ignoreLayer;
                        ToSetActiveOrFalse[1].SetActive(true);
                    }
                    else { PosTransform(0, 0, 0); transform.eulerAngles = new Vector3(0, 0, 0).normalized; gameObject.layer = ignoreLayer; ToSetActiveOrFalse[1].SetActive(true);  }
                    break;
                default:
                    break;

            }
        }
        private void PosTransform(float xT, float yT,float zT)
        {
            transform.position = new Vector3(xT, yT, zT);
        }

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                stream.SendNext(FirstOrSecond);
                foreach (GameObject obj in ToSetActiveOrFalse)
                {
                    stream.SendNext(obj.activeSelf);
                }
            }
            else
            {
                FirstOrSecond = (int)stream.ReceiveNext();
                foreach (GameObject obj in ToSetActiveOrFalse)
                {
                    obj.SetActive((bool)stream.ReceiveNext());
                }
            }
        }


#endif
    }
   



}

