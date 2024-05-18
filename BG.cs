using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BG : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.GetComponent<Renderer>().material.SetTexture("_Main_Tex", Resources.Load<Texture2D>("背景"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
