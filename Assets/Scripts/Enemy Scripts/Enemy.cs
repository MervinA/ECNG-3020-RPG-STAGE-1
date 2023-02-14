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
    public GameObject deathEffect; 
    public Vector2 StartPosition; 

private void Awake()
{
        health = maxHealth.initialValue; 
        StartPosition = transform.position; 
}

    public void OnEnable()
{
    health = maxHealth.initialValue; 
    transform.position = StartPosition; 
    currentState = EnemyState.idle; 
    //anim.SetBool("moving", true);
} 


public void TakeDamage(Rigidbody2D myRigidbody, float knockTime, float damage){
    health -= damage; 
    if(health > 0)
    {
    StartCoroutine(KnockCo(myRigidbody, knockTime));
    }else
    {
        DeathEffect();
        this.gameObject.SetActive(false);
    }
    //TakeDamage(damage);
}

private void DeathEffect()
{
    if(deathEffect != null) 
    {
        GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(effect, 1f);
    }
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
