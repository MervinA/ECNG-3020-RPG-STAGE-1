using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState {
        walk, 
        idle,
        attack,
        interact,
        stagger
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
        if(Input.GetButtonDown("attack") && currentState != PlayerState.attack && currentState != PlayerState.stagger){
          StartCoroutine(AttackCo());
        }

    else if(currentState == PlayerState.walk|| currentState == PlayerState.idle){
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

    public void Knock(float knockTime){
        StartCoroutine(KnockCo(knockTime));
    }

    
    private IEnumerator KnockCo( float knockTime){

    if(myRigidbody != null ){
        yield return new WaitForSeconds(knockTime);
        myRigidbody.velocity = Vector2.zero; 
        currentState = PlayerState.idle;
        myRigidbody.velocity = Vector2.zero;
    }
}
    
}
