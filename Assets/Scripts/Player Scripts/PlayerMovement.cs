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

public enum PlayerAttackType{
    unhanded, 
    sword, 
    axe, 
    bow, 
    spear
}
public class PlayerMovement : MonoBehaviour
{
    [Header("Player States")]
    public PlayerState currentState;
    public PlayerAttackType currentAttackState; 
    
    [Header("Player Physics Characteristics")]
     public float speed; 
    public static string spawnPointName; 
    private Rigidbody2D myRigidbody; 
    private Animator animator;
    private Vector3 change; 
    
    
    [Header("Player Health Characteristics")]
    public FloatValue currentHealth; 
    public SignalSender PlayerHealthSignal; 

    [Header("Player Inventory Characteristics")]
    public Inventory playerInventory;
    public SpriteRenderer receivedItemSprite; 
    
    [Header("Player Weapons Characteristics")]
    public GameObject projectile; 

    // Start is called before the first frame update
    void Start()
    {
        currentState = PlayerState.walk;
        currentAttackState = PlayerAttackType.unhanded; 
        animator = GetComponent<Animator>(); 
        myRigidbody = GetComponent<Rigidbody2D>(); 
        animator.SetFloat("MoveX", 0);
        animator.SetFloat("MoveY", -1);
        Application.targetFrameRate = 60;
    }

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

        if(Input.GetButton("sword") && currentState != PlayerState.attack && currentState != PlayerState.stagger && this.gameObject.CompareTag("Player"))
        {
            currentAttackState = PlayerAttackType.sword; 
        }
        else if (Input.GetButton("axe") && currentState != PlayerState.attack && currentState != PlayerState.stagger && this.gameObject.CompareTag("Player"))
        {
            currentAttackState = PlayerAttackType.axe;
        }
        else if (Input.GetButton("bow") && currentState != PlayerState.attack && currentState != PlayerState.stagger && this.gameObject.CompareTag("Player"))
        {
            currentAttackState = PlayerAttackType.bow;
        }
        else if (Input.GetButton("spear") && currentState != PlayerState.attack && currentState != PlayerState.stagger && this.gameObject.CompareTag("Player"))
        {
            currentAttackState = PlayerAttackType.spear;
        }



        if(Input.GetButtonDown("attack") && currentState != PlayerState.attack && 
                currentState != PlayerState.stagger && currentAttackState == PlayerAttackType.sword && 
                this.gameObject.CompareTag("Player"))
        {
          StartCoroutine(SwordAttackCo());
        }
        else if(Input.GetButtonDown("attack") && currentState != PlayerState.attack && 
                currentState != PlayerState.stagger && currentAttackState == PlayerAttackType.bow && 
                this.gameObject.CompareTag("Player"))
        {
          StartCoroutine(bowAttackCo());
        }

        else if(currentState == PlayerState.walk|| currentState == PlayerState.idle){
            UpdateAnimationAndMove();
            }
    }


    private IEnumerator SwordAttackCo(){

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

     private IEnumerator bowAttackCo(){

        //animator.SetBool("Attacking",true);
        currentState = PlayerState.attack;
        yield return null; 
        MakeArrow();
        //animator.SetBool("Attacking", false);
        yield return new WaitForSeconds(.3f);
        if(currentState != PlayerState.interact)
        {
          currentState = PlayerState.walk;   
        }
        
    }

    private void MakeArrow()
    {
        Vector2 temp = new Vector2(animator.GetFloat("MoveX"), animator.GetFloat("MoveY"));
        Arrow arrow = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Arrow>();
        arrow.Setup(temp, ChooseArrorDirection());
    }

    Vector3 ChooseArrorDirection()
    {
        float temp = Mathf.Atan2(animator.GetFloat("MoveY"), animator.GetFloat("MoveX"))*Mathf.Rad2Deg;
        return new Vector3(0,0,temp); 
    
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
            change.x = Mathf.Round(change.x);
            change.y = Mathf.Round(change.y);
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
