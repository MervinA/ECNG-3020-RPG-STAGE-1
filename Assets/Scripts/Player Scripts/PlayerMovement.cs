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
    public FloatValue currentHealth; 
    public SignalSender PlayerHealthSignal; 
    public VectorValue startingPosition; 
    public Inventory playerInventory;
    public SpriteRenderer receivedItemSprite; 




    // Start is called before the first frame update
    void Start()
    {
        currentState = PlayerState.walk;
        animator = GetComponent<Animator>(); 
        myRigidbody = GetComponent<Rigidbody2D>(); 
        animator.SetFloat("MoveX", 0);
        animator.SetFloat("MoveY", -1);
        Application.targetFrameRate = 60;
        transform.position = startingPosition.initialValue; 
    }

   /* public void Update(){


      if(Input.GetButtonDown("attack") && currentState != PlayerState.attack){
        // Debug.Log ("attack");
          StartCoroutine(AttackCo());
        }

    } */

    // Update is called once per frame
    void Update() // can be changed to Update()
    {
        //is the player in an interaction
        if(currentState == PlayerState.interact)
        {
          return;   
        }
        change = Vector3.zero; 
        change.x = Input.GetAxisRaw("Horizontal"); 
        change.y = Input.GetAxisRaw("Vertical");
        if(Input.GetButtonDown("attack") && currentState != PlayerState.attack && currentState != PlayerState.stagger && this.gameObject.CompareTag("Player")){
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
        if(currentState != PlayerState.interact)
        {
          currentState = PlayerState.walk;   
        }
        
    }

    public void RaisedItem()
    {
        if (playerInventory.currentItem != null)
        {
           if (currentState != PlayerState.interact)
             {
              animator.SetBool("Receive Item", true); 
              currentState = PlayerState.interact; 
              receivedItemSprite.sprite = playerInventory.currentItem.itemSprite;
            }else
             {
              animator.SetBool("Receive Item", false);  
              currentState = PlayerState.idle; 
              receivedItemSprite.sprite = null; 
              playerInventory.currentItem = null; 
            }

        }
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

    public void Knock(float knockTime, float damage)
    {
        currentHealth.RuntimeValue -= damage;
        PlayerHealthSignal.Raise(); 
        if(currentHealth.RuntimeValue> 0 )
        {
          //  PlayerHealthSignal.Raise(); // take off to show zero hearts
             StartCoroutine(KnockCo(knockTime));
        }else{
            this.gameObject.SetActive(false);
        }
        
    }

    
    private IEnumerator KnockCo( float knockTime){

    if(myRigidbody != null){
        yield return new WaitForSeconds(knockTime);
        myRigidbody.velocity = Vector2.zero; 
        currentState = PlayerState.idle;
        myRigidbody.velocity = Vector2.zero;
    }
}
    
}
