using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EnemyState{
    idle,
    walk,
    attack,
    stagger
}

public class Enemy : MonoBehaviour
{

    public EnemyState currentState; 
    public int health; 
    public string enemyName; 
    public int baseAttack; 
    public float moveSpeed; 

public void Knock(Rigidbody2D myRigidbody, float knockTime){
    StartCoroutine(KnockCo(myRigidbody, knockTime));
}
    // Start is called before the first frame update
private IEnumerator KnockCo(Rigidbody2D myRigidbody, float knocktime){

    if(myRigidbody != null ){
        yield return new WaitForSeconds(knocktime);
        myRigidbody.velocity = Vector2.zero; 
        currentState= EnemyState.idle;
        myRigidbody.velocity = Vector2.zero;
    }
}

}
