using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace TwoPlayersGame 
{
    public class Scrolltexture : MonoBehaviour
    {
        public float scroll = 2f;

        private Material mat;
        public bool xDir;


        // Start is called before the first frame update
        void Start()
        {
            mat = GetComponent<Renderer>().material;

        }

        // Update is called once per frame
        void Update()
        {
            float Offset = Time.deltaTime * scroll;
            ScrollTexture(Offset);




        }
        void ScrollTexture(float offset)
        {
            if (xDir)
            {
                mat.mainTextureOffset += new Vector2(0, offset);
            }
            else
            {
                mat.mainTextureOffset += new Vector2(offset, 0);
            }
        }


    }
}

