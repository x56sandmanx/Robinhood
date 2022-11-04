using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private Transform shootPos;
    [SerializeField] private PlayerCam playerCam;
    [SerializeField] private float force;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            //Instantiating object, setting it to the shoot pos, and rotating based off of player look pos, then adding force to shoot forward
            GameObject arrow = (GameObject)Instantiate(arrowPrefab, shootPos.position, Quaternion.Euler(playerCam.xRotation + 90f, playerCam.yRotation, 0));
            Rigidbody rb = arrow.GetComponent<Rigidbody>();
            rb.AddForce(arrow.transform.up * force, ForceMode.Impulse);
        }
    }
}
