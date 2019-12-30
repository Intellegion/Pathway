using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayMode : MonoBehaviour
{
    [SerializeField]
    private CinemachineVirtualCamera vcam;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void Target()
    {
        if(GameObject.FindGameObjectWithTag("Player"))
            vcam.Follow = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
