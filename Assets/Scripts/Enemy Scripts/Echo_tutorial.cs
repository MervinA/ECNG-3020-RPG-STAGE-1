using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Echo_tutorial : Enemy
{
    public Rigidbody2D myRigidbody; 
    public Transform target; 
    public float chaseRadius; 
    public float attackRadius; 
    public Transform homePosition; 
    public Animator anim; 
    // Start is called before the first frame update
    void Start()
    {
        currentState = EnemyState.idle; 
        anim = GetComponent<Animator>(); 
        myRigidbody = GetComponent<Rigidbody2D>(); 
        target = GameObject.FindWithTag("Player").transform; 
        anim.SetBool("moving", true);
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CheckDistance();
    }

    public virtual void CheckDistance(){

        if (Vector3.Distance(target.position,
                             transform.position)<=chaseRadius
                             && Vector3.Distance(target.position, 
                                                    transform.position)>attackRadius)
       {
                if((currentState == EnemyState.idle || currentState == EnemyState.walk) 
                   && currentState != EnemyState.stagger){

            //if (currentState != EnemyState.stagger){
            Vector3 temp = Vector3.MoveTowards(transform.position, target.position, moveSpeed*Time.deltaTime);
            changeAnim(temp-transform.position);
            myRigidbody.MovePosition(temp);
          
            ChangeState(EnemyState.walk);
            anim.SetBool("moving", true);
                }
       }else if ((Vector3.Distance(target.position, transform.position) > chaseRadius) ||
        (Vector3.Distance(target.position, transform.position) < attackRadius)){
            anim.SetBool("moving",false);
       }

    }

    private void SetAnimFloat(Vector2 setVector){
        anim.SetFloat("moveX", setVector.x);
        anim.SetFloat("moveY", setVector.y);
    }

    public void changeAnim(Vector2 direction){
        if(Mathf.Abs(direction.x)>Mathf.Abs(direction.y)){
            if(direction.x>0){
                SetAnimFloat(Vector2.right);
            }else if(direction.x<0){
                SetAnimFloat(Vector2.left);
            }
        }else if(Mathf.Abs(direction.x)<Mathf.Abs(direction.y)){
            if(direction.y>0){
               SetAnimFloat(Vector2.up); 
            }else if(direction.y<0){
              SetAnimFloat(Vector2.down);  
            }
        }
    }
    private void ChangeState(EnemyState newState){

        if(currentState != newState){
            currentState = newState;
        }

    }
}
