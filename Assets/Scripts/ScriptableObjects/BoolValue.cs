using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BoolValue : ScriptableObject, ISerializationCallbackReceiver
{
    // Start is called before the first frame update
    public bool initialValue; 
   [HideInInspector]
   public bool RuntimeValue;
    public void OnAfterDeserialize()
    {
        RuntimeValue = initialValue;
    }
   public void OnBeforeSerialize()
   {

   }
}
