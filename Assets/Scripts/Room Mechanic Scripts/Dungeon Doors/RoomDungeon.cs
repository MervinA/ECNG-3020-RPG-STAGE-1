using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomDungeon : MonoBehaviour
{
  public Enemy [] enemies; 
  public pot[] pots; 
/*public HealthUp[] healthRegen;
  public Coin[] coins; 
*/

  public virtual void OnTriggerEnter2D(Collider2D other)
  {
    if(other.CompareTag("Player") && !other.isTrigger)
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            ChangeActivation(enemies[i], true); 
        }
         for (int i = 0; i < pots.Length; i++)
        {
            ChangeActivation(pots[i], true); 
        }
      /*    for (int i = 0; i < healthRegen.Length; i++)
        {
            ChangeActivation(healthRegen[i], true); 
        }
         for (int i = 0; i < coins.Length; i++)
        {
            ChangeActivation(coins[i], true); 
        } */
    }
  }

  public virtual void OnTriggerExit2D(Collider2D other)
  {
     if(other.CompareTag("Player") && !other.isTrigger)
    {
         for (int i = 0; i < enemies.Length; i++)
        {
            ChangeActivation(enemies[i], false); 
        }
         for (int i = 0; i < pots.Length; i++)
        {
            ChangeActivation(pots[i], false); 
        }
      /*  for (int i = 0; i < healthRegen.Length; i++)
        {
            ChangeActivation(healthRegen[i], false); 
        }
        for (int i = 0; i < coins.Length; i++)
        {
            ChangeActivation(coins[i], false); 
        } */
    }
  }

  void ChangeActivation(Component component, bool activation)
  {
    component.gameObject.SetActive(activation);
  }
}
