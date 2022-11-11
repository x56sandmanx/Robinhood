using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private Transform shootPos;
    [SerializeField] private PlayerCam playerCam;
    [SerializeField] private float force;
    [SerializeField] private GameManager gameManager;
    public AudioSource[] sounds;
    private AudioSource arrowSound;
    
    // Start is called before the first frame update
    void Start()
    {
		sounds = GetComponents<AudioSource>();
        arrowSound = sounds[0];
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            //Instantiating object, setting it to the shoot pos, and rotating based off of player look pos, then adding force to shoot forward
            GameObject arrow = (GameObject)Instantiate(arrowPrefab, shootPos.position, Quaternion.Euler(playerCam.xRotation + 90f, playerCam.yRotation,3));
            arrow.transform.LookAt(shootPos);
            Rigidbody rb = arrow.GetComponent<Rigidbody>();
            rb.AddForce(arrow.transform.up * force, ForceMode.Impulse);
            arrowSound.Play();
        }
    }
}
