using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSky : MonoBehaviour
{
    private GameObject sky;
    private float time;
    private void Awake()
    {
        sky = GameObject.FindGameObjectWithTag("sky");
    }

    // Update is called once per frame
    void Update()
    {
        //float time;
        //Material mat = RenderSettings.skybox;
        time = Time.deltaTime * 1f;
        //mat.SetFloat("_Rotation", time);
        
        sky.transform.Rotate(0,time,0);
    }
}
