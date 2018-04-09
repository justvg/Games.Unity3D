using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShurikenScr : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
		
	void Update () {
        transform.Rotate(0, 0, Time.deltaTime * -300);
	}
}