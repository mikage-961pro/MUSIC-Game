using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterSystem : MonoBehaviour {

	[SerializeField]
	private float gameSpeed = 1;

	[SerializeField]
	private float noteHeight = 3;

	[SerializeField]
	private float waitTime = 2;

	private NoteData[] noteArray;

	private Vector2[] tapPos_A = { new Vector2(), new Vector2(), new Vector2(), new Vector2(), new Vector2() };

	private float time = 0;

	private Dictionary<string, Transform> noteTarget_D = new Dictionary<string, Transform>();

	void Start () {
		foreach (GameObject target in GameObject.FindGameObjectsWithTag("Target")) {
			noteTarget_D.Add (target.name, target.transform);
		}
		noteArray = GameObject.FindObjectsOfType<NoteData> ();
	}

	void Update () {
		NoteMove();
		if (Input.GetKeyDown (KeyCode.Space)) {
			Debug.Log (time);
		}
		TapTrigger();
	}

	void NoteMove () {
		foreach (NoteData note in noteArray) {

			float a = noteTarget_D [note.pos].position.y - noteHeight;
			float t = note.time;
			float w = waitTime;
			float s = gameSpeed;
			float p = noteHeight;

			note.gameObject.transform.position = new Vector3 (
				noteTarget_D [note.pos].transform.position.x * (NoteMove(note.time, note.time - (waitTime * 1.67f) / gameSpeed, time)),
				(noteTarget_D [note.pos].position.y - noteHeight) * Mathf.Pow(((time - note.time + (waitTime / gameSpeed)) * (gameSpeed / waitTime)), 2) + noteHeight,
				note.transform.position.z);
			
			note.gameObject.transform.localScale = new Vector3 (1.5f, 1.5f, 1) * (NoteMove(note.time, note.time - (waitTime * 1.67f) / gameSpeed, time));
		}
		time += Time.deltaTime;
	}

	void TapTrigger(){
		foreach (Touch t in Input.touches) {
			var id = t.fingerId;

			switch (t.phase) {
			case TouchPhase.Began:
				Ray ray = Camera.main.ScreenPointToRay (t.position);
				RaycastHit hit = new RaycastHit ();
				if (Physics.Raycast (ray, out hit, 100)) {
//					Debug.Log (hit.transform.name);
					JudgeNote (hit.transform.name);
					tapPos_A [id] = Camera.main.WorldToScreenPoint (hit.transform.position);;
				}
				break;
			case TouchPhase.Moved:
				float xx = t.position.x - tapPos_A [id].x;
				float yy = t.position.y - tapPos_A [id].y;

				Debug.LogFormat ("({0} , {1})", xx, yy);
//				if (Mathf.Abs (xx) > Mathf.Abs (yy)) {
//					if (50 < xx){
//						//右向きにフリック
//						Debug.Log("右フリック");
//					}else if (-50 > xx){
//						//左向きにフリック
//						Debug.Log("左フリック");
//					}
//				}else if (Mathf.Abs (yy) > Mathf.Abs (xx)) {
//					if (50 < yy){
//						//右向きにフリック
//						Debug.Log("上フリック");
//					}else if (-50 > yy){
//						//左向きにフリック
//						Debug.Log("下フリック");
//					}
//				}

				break;
			}
		}
	}

	public static float NoteMove(float max,float min,float ve){
		float range = max - min;
		float value = ve - min;

		if (ve <= min) {
			return 0;
		} else {
			return value / range;
		}
	}

	void JudgeNote(string targetName){
		NoteData nearNote = serchNearNote (targetName);
		if (nearNote != null) {

			if (Mathf.Abs (nearNote.time - time) <= .15f) {
				Debug.Log ("Good");

			} else if (Mathf.Abs (nearNote.time - time) <= .25f) {
				Debug.Log ("OK");

			} else {
				Debug.Log ("Bad");
			}
			nearNote.gameObject.SetActive (false);
		}
	}

	NoteData serchNearNote(string targetName){
		float tmpTime = 0;           //距離用一時変数
		float nearTime = 0;          //最も近いオブジェクトの距離
		NoteData targetObj = null; 		 //現在の判定を取るノーツオブジェクト
		bool firstJudge = true;		 //初回判定通過用bool

		//タグ指定されたオブジェクトを配列で取得する
		foreach (NoteData note in GameObject.FindObjectsOfType<NoteData> ()){
			if (targetName == note.pos && Mathf.Abs(note.time - time) <= 0.3f) {
				tmpTime = Mathf.Abs(time - note.time);
				//オブジェクトの距離が近いか、距離0であればオブジェクト名を取得
				//一時変数に距離を格納
				if (firstJudge || nearTime > tmpTime) {
					nearTime = tmpTime;
					targetObj = note;
					firstJudge = false;
				}
			}
		}
		return targetObj;
	}
}
