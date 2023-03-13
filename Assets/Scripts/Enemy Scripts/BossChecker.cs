using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossChecker : MonoBehaviour
{
    [SerializeField] private BoolValue bosschecker;
    [SerializeField] private GameObject boss;  
    // Start is called before the first frame update
    void Awake()
    {
        if(bosschecker.RuntimeValue ==true)
        {
            boss.SetActive(false);
        }
    }

    
}
