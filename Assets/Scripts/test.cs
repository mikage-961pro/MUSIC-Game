using UnityEngine;
using System.Collections;

public class test: MonoBehaviour
{
	float[] time = {0,0};
	void Update()
	{
		foreach (Touch t in Input.touches)
		{
			var id = t.fingerId;

			switch(t.phase)
			{
			case TouchPhase.Began:
				Ray ray = Camera.main.ScreenPointToRay (t.position);
				RaycastHit hit = new RaycastHit ();
				if (Physics.Raycast (ray, out hit, 100)) {
					Debug.Log (hit.transform.name);
				}
				break;

			case TouchPhase.Moved:
				Debug.LogFormat("{0}:動いている", id);
				break;
//
//			case TouchPhase.Stationary:
//				time[id] += Time.deltaTime;
//				break;

//			case TouchPhase.Ended:
//				Debug.LogFormat("{0}:{1}", id,t.position);
//					Ray ray2 = Camera.main.ScreenPointToRay(t.position);
//					RaycastHit hit2 = new RaycastHit();
//				if(Physics.Raycast(ray2,out hit2,100)){
//						Debug.Log(hit2.transform.name);
//					}
//				Debug.Log(time[id]);
//				break;

//			case TouchPhase.Canceled:
//				Debug.LogFormat("{0}:いま離された", id);
//				break;
			}
		}
	}
}
