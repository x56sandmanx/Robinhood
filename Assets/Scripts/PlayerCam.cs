using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    [SerializeField] private float sensX;
    [SerializeField] private float sensY;
    [SerializeField] private Transform orientation;

    public float xRotation;
    public float yRotation;
    Transform playerTransform;
    // Start is called before the first frame update
    void Start()
    {
        //Locking cursor and hiding while player is in first person
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        //Getting Unity inputs
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        //Setting y and x rotation based on mouse input
        yRotation += mouseX;
        xRotation -= mouseY;

        //Setting player Rotation to the yRotation for shooting
        playerTransform.eulerAngles = new Vector3(0, yRotation, 0);

        //Clamping rotation to 90 and -90 so camera cannot rotate after looking straight up/down
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }
}
