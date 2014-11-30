using UnityEngine;
using System.Collections;

public class Siatka : MonoBehaviour {
	float pieceSize=0;
	Vector2 playgroundStart=new Vector2 (0, 0);
	Vector2 playgroundSize=new Vector2 (6,7);
	public Field[][] fields;
	public GameObject fieldtemplate;
	public GameObject netBackground;

	public FieldRow[] rows;

	void Start () {
		pieceSize=((BoxCollider2D)(fieldtemplate.collider2D)).bounds.size.x;
//		playgroundStart = ((BoxCollider2D)(this.collider2D)).bounds.min;
		Debug.Log( netBackground.GetComponent<SpriteRenderer>().bounds);
		Debug.Log (playgroundStart);
		initFields ();
	}

	public void initFields()
	{
		fields=new Field[(int)(playgroundSize.y)][];

		fields [0] = new Field[(int)(playgroundSize.x)];
		for (int i=0; i<playgroundSize.x; i++)
						fields [0] [i] = new SpawnField ();

		for (int j=1; j<playgroundSize.y; j++) {
			fields [j]=new Field[(int)(playgroundSize.x)];
			for (int i=0; i<playgroundSize.x; i++){
				fields [j] [i] = rows[j].fields[i];
				fields [j] [i].FieldAbove=fields [j-1] [i];
			}
		}

		Debug.Log ("ready");

	}

	void Update () {

	}


}
