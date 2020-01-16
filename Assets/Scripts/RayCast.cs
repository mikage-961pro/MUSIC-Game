using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCast : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.touchCount > 0){
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit = new RaycastHit();

//			if (Physics.Raycast(ray, out hit)) {
//				Debug.Log (hit.transform.name);
//			}
		}

		for (int num = 0; Input.touchCount > num; num++) {
		}
	}
}
