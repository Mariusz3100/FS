using UnityEngine;
using System.Collections;

public class TexShift : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	float scrollSpeed = 0.5f;

		void Update() {
			float scaleX = Mathf.Cos(Time.time) * 2F + 1;
			float scaleY = Mathf.Sin(Time.time) * 2F + 1;
			renderer.material.SetTextureScale("_MainTex", new Vector2(scaleX, scaleY));
		}

}
