using UnityEngine;
using System.Collections;

public class Siatka : MonoBehaviour {
	float pieceSizeY;
	float pieceSizeX;
	Vector2 playgroundStart=new Vector2 (0, 0);
	Vector2 playgroundSize=new Vector2 (6,8);
	public Field[][] fields;
	private static Siatka instance;
	public Field fieldtemplate;
	public SpawnField spawnTemplate;
	public GameObject netBackground;
	public GameObject line;
//	public FieldRow[] rows;


	public static Siatka getSingleton()
	{
		return instance;
	}

	public float PieceSizeY {
		get {
			return pieceSizeY;
		}
		set {
			pieceSizeY = value;
		}
	}

	public float PieceSizeX {
		get {
			return pieceSizeX;
		}
		set {
			pieceSizeX = value;
		}
	}

	void Start () {
//		playgroundStart = ((BoxCollider2D)(this.collider2D)).bounds.min;
//		Debug.Log( netBackground.GetComponent<SpriteRenderer>().bounds);
//		Debug.Log (playgroundStart);
		if(instance!=null)
			throw new UnityException("More than one instance of Siatka");
		instance = this;

		initFields ();
		initLines ();
	}

	public void initLines()
	{	
		float constantY = (fields [1] [0].transform.position.y + fields [fields.Length-1] [0].transform.position.y) / 2;

		for (int i=0; i<fields[0].Length-1; i++) 
		{
			float changingX=(fields[0][i].transform.position.x+fields[0][i+1].transform.position.x)/2;
			GameObject newLine=(GameObject)Instantiate(this.line);
			Vector3 linePos=new Vector3(changingX,constantY,line.transform.position.z);
			newLine.transform.position=linePos;

		}

		float constantX = (fields [0] [0].transform.position.x + fields[0][fields[0].Length-1].transform.position.x) / 2;
		
		for (int i=1; i<fields.Length-1; i++) 
		{
			float changingY=(fields[i][0].transform.position.y+fields[i+1][0].transform.position.y)/2;
			GameObject newLine=(GameObject)Instantiate(this.line);
			Vector3 linePos=new Vector3(constantX,changingY,line.transform.position.z);
			newLine.transform.localScale-=new Vector3(0,(playgroundSize.y-0.7f-playgroundSize.x),0);
			newLine.transform.position=linePos;
			newLine.transform.Rotate(0f,0f,90f);		
		}
	}

	public void initFields()
	{
		pieceSizeX=((BoxCollider2D)(fieldtemplate.GetComponent<Collider2D>())).bounds.size.x;
		pieceSizeY=((BoxCollider2D)(fieldtemplate.GetComponent<Collider2D>())).bounds.size.y;

		fields=new Field[(int)(playgroundSize.y)][];

		fields [0] = new Field[(int)(playgroundSize.x)];
		for (int i=0; i<playgroundSize.x; i++) {
			//fields [0] [i] = /rows[0].fields[i];
			fields [0] [i] = (Field)Instantiate(spawnTemplate);
			Vector3 pos=fields [0] [i].transform.position;
			pos.x=spawnTemplate.transform.position.x+pieceSizeX*i;
			fields [0] [i].transform.position=pos;

		}

		Destroy (spawnTemplate.gameObject);

		spawnTemplate=(SpawnField)fields [0] [0];



		for (int j=1; j<playgroundSize.y; j++) {
			fields [j]=new Field[(int)(playgroundSize.x)];
			for (int i=0; i<playgroundSize.x; i++){
//				fields [j] [i] = rows[j].fields[i];
				Field newField=(Field)Instantiate(fieldtemplate);
				newField.transform.position=new Vector3(fieldtemplate.transform.position.x+pieceSizeX*i,
				                                        fieldtemplate.transform.position.y-pieceSizeY*(j-1),
				                                        fieldtemplate.transform.position.z);
				newField.FieldAbove=fields [j-1] [i];
				fields [j] [i]=newField;
			}
		}

		Destroy (fieldtemplate.gameObject);
		fieldtemplate=fields [1] [0];
		fieldtemplate.FieldAbove = fields [0] [0];


//		Debug.Log ("ready");

	}

	void Update () {
		updateFields ();
	}


	void updateFields()
	{



	}


	public void executeMatches(Vector2[] matches){
		GuiScript.getSingleton ().addPoints (
			fields[(int)matches[0].y][(int)matches[0].x].CurrentCrop.Points*matches.Length
			);


		foreach (Vector2 v in matches) {
			fields [(int)v.y] [(int)v.x].emptyThisField();
			
		}

	}

	public Vector2[] checkForMatches(){
				Vector2[] bestmatch = new Vector2[]{};

				for (int i=1; i<=fields.Length-3; i++) {
						for (int j=0; j<=fields[0].Length-1; j++) {

								if (fields [i] [j].CurrentCrop.Type == fields [i + 1] [j].CurrentCrop.Type
										&& fields [i] [j].CurrentCrop.Type != Crop.blankType)	
								if (fields [i] [j].CurrentCrop.Type == fields [i + 2] [j].CurrentCrop.Type) {
										if (bestmatch.Length < 3)
												bestmatch = new Vector2[]{new Vector2 (j, i),new Vector2 (j, i + 1),new Vector2 (j, i + 2)};

										if (fields.Length > i + 3 && fields [i] [j].CurrentCrop.Type == fields [i + 3] [j].CurrentCrop.Type) {
												if (bestmatch.Length == 3)
														bestmatch = new Vector2[] {
																new Vector2 (j, i),
																new Vector2 (j, i + 1),
																new Vector2 (j, i + 2),
																new Vector2 (j, i + 3)
														};
										}

								}
						}
				}


		for (int i=1; i<=fields.Length-1; i++) {
			for (int j=0; j<=fields[0].Length-3; j++) {
				
				if(fields[i][j].CurrentCrop.Type==fields[i][j+1].CurrentCrop.Type
				   &&fields[i][j].CurrentCrop.Type!=Crop.blankType)	
				if(fields[i][j].CurrentCrop.Type==fields[i][j+2].CurrentCrop.Type){
					if(bestmatch.Length<3)
					bestmatch=new Vector2[]{new Vector2(j,i),new Vector2(j+1,i),new Vector2(j+2,i)};
					
					if(fields[0].Length>j+3&&fields[i][j].CurrentCrop.Type==fields[i][j+3].CurrentCrop.Type){
						if(bestmatch.Length==3)
						bestmatch=new Vector2[]{new Vector2(j,i),new Vector2(j+1,i),new Vector2(j+2,i),new Vector2(j+3,i)};
					}
					
				}

				/*
				if(fields[i][j].CurrentCrop.Type==fields[i][j+1].CurrentCrop.Type)
				if(fields[i][j].CurrentCrop.Type==fields[i][j+2].CurrentCrop.Type){
					if(bestmatch==null)
					bestmatch=new Vector2[]{new Vector2(i,j),new Vector2(i,j+1),new Vector2(i,j+2)};
					
					if(fields[0].Length>j+3&&fields[i][j].CurrentCrop.Type==fields[i][j+3].CurrentCrop.Type){
						if(bestmatch.Length==3)
						bestmatch=new Vector2[]{new Vector2(i,j),new Vector2(i+1,j),new Vector2(i+2,j),new Vector2(i+3,j)}
					}
					*/
					
				}	
			}

		return bestmatch;

	}



}
