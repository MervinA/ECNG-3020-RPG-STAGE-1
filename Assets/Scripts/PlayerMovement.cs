using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState {
        walk, 
        attack,
        interact

}

public class PlayerMovement : MonoBehaviour
{
    public PlayerState currentState;
    public float speed; 
    private Rigidbody2D myRigidbody; 
    private Vector3 change; 
    private Animator animator; 

    // Start is called before the first frame update
    void Start()
    {
        currentState = PlayerState.walk;
        animator = GetComponent<Animator>(); 
        myRigidbody = GetComponent<Rigidbody2D>(); 
        animator.SetFloat("MoveX", 0);
        animator.SetFloat("MoveY", -1);
    }

    public void Update(){

      if(Input.GetButtonDown("attack") && currentState != PlayerState.attack){
        // Debug.Log ("attack");
          StartCoroutine(AttackCo());
        }

    }

    // Update is called once per frame
    void FixedUpdate() // can be changed to Update()
    {
        change = Vector3.zero; 
        change.x = Input.GetAxisRaw("Horizontal"); 
        change.y = Input.GetAxisRaw("Vertical");
        /*if(Input.GetButtonDown("attack") && currentState != PlayerState.attack){
          StartCoroutine(AttackCo());
        }*/

     if(currentState == PlayerState.walk){
          UpdateAnimationAndMove();
        }
    }


    private IEnumerator AttackCo(){

        animator.SetBool("Attacking",true);
        currentState = PlayerState.attack;
        yield return null; 
        animator.SetBool("Attacking", false);
        yield return new WaitForSeconds(.3f);
        currentState = PlayerState.walk; 
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

        change.Normalize();
        myRigidbody.MovePosition(
            transform.position + change * speed * Time.fixedDeltaTime  //can be changed to deltaTime
            
        );
    }
}
