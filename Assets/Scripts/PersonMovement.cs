using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonMovement : MonoBehaviour
{
    public EnemyMovement script;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
      if(script.isMoving) {
        anim.SetBool("isWalking", true);
      } else {
        Invoke("setIdle", 1);
      }
    }

    void setIdle()
    {
      anim.SetBool("isWalking", false);
    }
}
