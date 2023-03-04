using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BossState{
    Phase1, 
    Phase2
}
public class BossScript : Enemy
{
    [Header("Enemy States")]
    public BossState currPhase; 

    [Header("")]
    public Transform target; 
    public float chaseRadius; 
    public float attackRadius; 
    public float roundingDistance; 

    [Header("")]
    public Animator anim; 
    public Rigidbody2D myRigidbody;



        void Start()
        {
            currentState = EnemyState.idle; 
            currPhase = BossState.Phase1;
            anim = GetComponent<Animator>(); 
            myRigidbody = GetComponent<Rigidbody2D>(); 
            target = GameObject.FindWithTag("Player").transform; 
            
        }
        public void FixedUpdate()
        {

        }


    public virtual void CheckDistance(){

            if (Vector3.Distance(target.position,
                                transform.position)<=chaseRadius
                                && Vector3.Distance(target.position, 
                                transform.position)>attackRadius)
        {
                    if((currentState == EnemyState.idle || 
                        currentState == EnemyState.walk) 
                    && currentState != EnemyState.stagger){

                //if (currentState != EnemyState.stagger){
                Vector3 temp = Vector3.MoveTowards(transform.position, target.position, 
                            moveSpeed*Time.deltaTime);
            
                changeAnim(temp-transform.position);
                myRigidbody.MovePosition(temp);
                ChangeState(EnemyState.walk);
                anim.SetBool("moving", true);
                    }
        }
        else if ((Vector3.Distance(target.position, transform.position) <= chaseRadius) &&
            (Vector3.Distance(target.position, transform.position) <= attackRadius)){
            // anim.SetBool("moving",false);
                if((currentState == EnemyState.idle || 
                    currentState == EnemyState.walk) 
                    && currentState != EnemyState.stagger){
                        StartCoroutine(Phase1AttackCo());
                }
        }

        else if( (Vector3.Distance(target.position, transform.position) > chaseRadius)){
            /*anim.SetBool("moving", false);
            ChangeState(EnemyState.idle); */
            
                if(Vector3.Distance(transform.position,StartPosition) > roundingDistance)
                    {
                        Vector3 temp = Vector3.MoveTowards(transform.position, StartPosition, moveSpeed*Time.deltaTime);
                        changeAnim(temp-transform.position);
                        myRigidbody.MovePosition(temp);
                        ChangeState(EnemyState.walk);
                        anim.SetBool("moving",true);
                    }

                else if(Vector3.Distance(transform.position,StartPosition) <= roundingDistance)
                    {
                        anim.SetBool("moving", false);
                        SetAnimFloat(Vector2.down);
                        ChangeState(EnemyState.idle);  
                    }
            
        }
    }
    private IEnumerator BossAttackBehaviour()
    {
        if(PhaseStates() == BossState.Phase1)
        {
            StartCoroutine(Phase1AttackCo());
        }
        else if(PhaseStates() == BossState.Phase1)
        {
            StartCoroutine(Phase1AttackCo());
        }
        yield return null;
    }   
 private BossState PhaseStates()
        {
            BossState NowPhase = currPhase; 
            if(health > (health/1.5))
            {
                NowPhase = BossState.Phase2; 
            }
            else 
            {
                NowPhase = BossState.Phase1; 
            }
            return NowPhase; 
        }

    private IEnumerator Phase1AttackCo(){

            anim.SetBool("moving", false);
            currentState = EnemyState.attack;
            anim.SetBool("attacking", true);
            yield return null;
            anim.SetBool("attacking", false);
            yield return new WaitForSeconds(1f);
            currentState = EnemyState.walk;
            
        }

    private IEnumerator Phase2AttackCo(){

            anim.SetBool("moving", false);
            currentState = EnemyState.attack;
            anim.SetBool("attacking", true);
            yield return null;
            anim.SetBool("attacking", false);
            yield return new WaitForSeconds(1f);
            currentState = EnemyState.walk;
            
        } 
        

    
    public void SetAnimFloat(Vector2 setVector){
            anim.SetFloat("moveX", setVector.x);
            anim.SetFloat("moveY", setVector.y);
        }

//come back to change anim
    public void changeAnim(Vector2 direction){
            if(Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
            {
                if(direction.x > 0){
                    SetAnimFloat(Vector2.right);
                }else if (direction.x < 0)
                {
                    SetAnimFloat(Vector2.left);
                }
            }else if(Mathf.Abs(direction.x) < Mathf.Abs(direction.y)){
                if(direction.y > 0)
                {
                    SetAnimFloat(Vector2.up);
                }
                else if (direction.y < 0)
                {
                    SetAnimFloat(Vector2.down);
                }
            }
        }

        public void ChangeState(EnemyState newState)
        {
            if(currentState != newState){
                currentState = newState;
            }
        }

       
}
