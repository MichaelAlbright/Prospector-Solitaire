using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum FSState
{
	idle,
	pre,
	active,
	post
}

public class FloatingScore : MonoBehaviour {
	public FSState state = FSState.idle;
	[SerializeField]
	private int _score = 0;
	public string scoreString;

	public int score
	{
		get {
			return(_score);
		}
		set {
			_score = value;
			scoreString = Utils.AddCommasToNumber (_score);
			GetComponent<GUIText> ().text = scoreString;
		}
	}

	public List<Vector3> bezierPts;
	public List<float> fontSizes;
	public float timeStart = -1f;
	public float timeDuration = 1f;
	public string easingCuve = Easing.InOut;

	public GameObject reportFinishTo = null;

	public void Init(List<Vector3> ePts, float eTimeS = 0, float eTimeD = 1)
	{
		bezierPts = new List<Vector3> (ePts);

		if (ePts.Count == 1) {
			transform.position = ePts [0];
			return;
		}

		if (eTimeS == 0)
			eTimeS = Time.time;
		timeStart = eTimeS;
		timeDuration = eTimeD;

		state = FSState.pre;
	}

	public void FSCallback(FloatingScore fs)
	{

	}
}
