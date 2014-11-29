using UnityEngine;
using System.Collections;

public class Siatka : MonoBehaviour {
	float pieceSize=0;
	Vector2 playgroundStart=new Vector2 (0, 0);
	Vector2 playgroundSize=new Vector2 (6,7);
	public Field[][] fields;
	public MonoBehaviour[][] fields2;
	public GameObject fieldtemplate;
	public GameObject netBackground;


	void Start () {
		pieceSize=((BoxCollider2D)(fieldtemplate.collider2D)).bounds.size.x;
//		playgroundStart = ((BoxCollider2D)(this.collider2D)).bounds.min;
		Debug.Log( netBackground.GetComponent<SpriteRenderer>().bounds);
		Debug.Log (playgroundStart);
	}
	

	void Update () {

	}
}
