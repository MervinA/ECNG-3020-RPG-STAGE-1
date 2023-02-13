using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol_Echo_turotial : Echo_tutorial
{
    public Transform[] path; 
    public int currentPoint; 
    public Transform currentGoal; 
    public float roundingDistance; 
    // Update is called once per frame
    


    
    public override void CheckDistance(){
        

        if (Vector3.Distance(target.position,
                            transform.position) <= chaseRadius
           && Vector3.Distance(target.position,
                               transform.position) > attackRadius)
       {
                if((currentState == EnemyState.idle || currentState == EnemyState.walk)
                   && currentState != EnemyState.stagger)
                   {
             Vector3 temp = Vector3.MoveTowards(transform.position,
                                                         target.position,
                                                         moveSpeed * Time.deltaTime);
            changeAnim(temp-transform.position);
            myRigidbody.MovePosition(temp);
            ChangeState(EnemyState.walk);
            anim.SetBool("moving", true);
                     }

                    /*else if (Vector3.Distance(target.position, transform.position) > chaseRadius)
        {
            anim.SetBool("wakeUp", false);
        }*/
       }
       else if (Vector3.Distance(target.position, transform.position) > chaseRadius)
       {
            if(Vector3.Distance(transform.position, path[currentPoint].position)>roundingDistance)
            {
            Vector3 temp = Vector3.MoveTowards(transform.position, path[currentPoint].position, moveSpeed*Time.deltaTime);
            changeAnim(temp-transform.position);
            myRigidbody.MovePosition(temp);
            ChangeState(EnemyState.walk);
            anim.SetBool("moving",true);
            }else{
                ChangeGoal(); 
            }
       }

        else if(Vector3.Distance(target.position, transform.position) < attackRadius){ //can remove second check if needed to move enemy even after entering attack radius 
            anim.SetBool("moving",false);
       }
    
    }

    private void ChangeGoal()
    {
        if(currentPoint == path.Length - 1)
        {
            currentPoint = 0; 
            currentGoal = path[0]; 
        }
        else 
        {
            currentPoint++; 
            currentGoal = path[currentPoint]; 
        }
    }
}
