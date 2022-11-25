using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float speed; 
    private Rigidbody2D myRigidbody; 
    private Vector3 change; 
    private Animator animator; 

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>(); 
        myRigidbody = GetComponent<Rigidbody2D>(); 

    }

    // Update is called once per frame
    void FixedUpdate() // can be changed to Update()
    {
        change = Vector3.zero; 
        change.x = Input.GetAxisRaw("Horizontal"); 
        change.y = Input.GetAxisRaw("Vertical");
       // Debug.Log(change);
       UpdateAnimationAndMove();
    }

    void UpdateAnimationAndMove(){
        if(change != Vector3.zero){
            MoveCharacter();
            animator.SetFloat("MoveX", change.x);
            animator.SetFloat("MoveY", change.y);
            animator.SetBool("Moving", true);
        }else{
            animator.SetBool("Moving", false);
        }
    } 
    void MoveCharacter(){

        myRigidbody.MovePosition(
            transform.position + change * speed * Time.fixedDeltaTime  //can be changed to deltaTime
            
        );
    }
}
