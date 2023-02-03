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
    public FloatValue maxHealth;
    public float health; 
    public string enemyName; 
    public int baseAttack; 
    public float moveSpeed; 

    private void Awake(){
        health = maxHealth.initialValue; 
    }


public void Knock(Rigidbody2D myRigidbody, float knockTime, float damage){
    health -= damage; 
    if(health > 0)
    {
    StartCoroutine(KnockCo(myRigidbody, knockTime));
    }else
    {
        this.gameObject.SetActive(false);
    }
    //TakeDamage(damage);
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
