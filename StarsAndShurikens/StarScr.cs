using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarScr : MonoBehaviour {
	void Update () {
		
	}

   void OnTriggerEnter2D(Collider2D col) {
        if (col.CompareTag("Border")) {
            gameObject.SetActive(false);
        }
   }
}
