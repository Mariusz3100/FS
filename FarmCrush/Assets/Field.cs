using UnityEngine;
using System.Collections;

public class Field :MonoBehaviour {

	private Crop currentCrop;
	
	public Crop CurrentCrop {
		get {
			return currentCrop;
		}
		set {
			currentCrop = value;
		}
	}

	bool isFilling;

	public bool IsFilling {
		get {
			return isFilling;
		}
		set {
			isFilling = value;
		}
	}

	Field fieldAbove;

	public Field FieldAbove {
		get {
			return fieldAbove;
		}
		set {
			fieldAbove = value;
		}
	}

	bool empty;

	public bool Empty {
		get {
			return empty;
		}
		set {
			empty = value;
		}
	}

	public static float speed;

	// Use this for initialization
	void Start () {
		empty = true;
		isFilling = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Empty) {
			fill ();
		} else {
			moveCropToThisField();
		
		}

	}

	 

	public void fill(){
		currentCrop=FieldAbove.currentCrop;
		fieldAbove.currentCrop = null;
		fieldAbove.Empty = true;

	}

	private void moveCropToThisField(){

	}


}
