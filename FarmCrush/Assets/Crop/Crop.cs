using UnityEngine;
using System.Collections;

public abstract class Crop : MonoBehaviour {
	private int type=0;
	private string cropName="";
	public static float speed=0.03f;
	public static int blankType=-1;
	private int points = 0;

	public int Points {
		get {
			return points;
		}
		set {
			points = value;
		}
	}





	public string Name {
		get {
			return name;
		}
		set {
			name = value;
		}
	}

	public int Type {
		get {
			return type;
		}
		set {
			type = value;
		}
	}

	int bonusGold=0;

	void Start () {
	
	}
	
	void Update () {
	
	}


	public virtual void remove(){

		transform.position=new Vector3 (-12, 10, this.transform.position.z);
//		Destroy (this);
		this.Type = Crop.blankType;
		Destroy (this.gameObject);



	}
}
