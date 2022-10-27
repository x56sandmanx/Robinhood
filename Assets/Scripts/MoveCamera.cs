using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    [SerializeField] private Transform cameraPos;

    // Update is called once per frame
    void Update()
    {
        //Have camera follow the camera pos for first person
        transform.position = cameraPos.position;
    }
}
