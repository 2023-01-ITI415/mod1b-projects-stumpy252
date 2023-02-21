using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Collectible : MonoBehaviour
{
    
   void OnTriggerEnter(){
        this.gameObject.SetActive(false);
      Prototype.SHOT_HIT();
      Prototype.SCORE();
   }
 
    
}
