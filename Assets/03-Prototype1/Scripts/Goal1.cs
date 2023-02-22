using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent (typeof(Renderer))]

public class Goal1 : MonoBehaviour
{
	static public bool 	goalMet = false;

	void OnTriggerEnter(Collider other) {
		// when the trigger is hit by something
		// check to see if it's a Projectile 
		Projectile proj = other.GetComponent<Projectile>();
		if(proj != null){
			// if so, set goalMet = true
			Goal.goalMet = true;

			// also set the alpha of the color of higher opacity
			Material mat = GetComponent<Renderer>().material;
			Color c = mat.color;
			c.a = 0.75f;
			mat.color = c;
		}
		
	}
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
