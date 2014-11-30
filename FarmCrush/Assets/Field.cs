using UnityEngine;
using System.Collections;

public class Field :MonoBehaviour {
	public static float speed=0.01f;

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


	// Use this for initialization
	void Start () {
		empty = true;
		isFilling = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Empty) {
			tryFill ();
		} else {
			moveCropToThisField();
		
		}

	}

	 

	public virtual void tryFill(){
		if (fieldAbove.isFilling)
						return;
		currentCrop=FieldAbove.currentCrop;
		fieldAbove.currentCrop = null;
		fieldAbove.Empty = true;
		isFilling = true;
		Empty = false;

	}

	private void moveCropToThisField(){
		Vector3 diff = currentCrop.transform.position - transform.position;
		if (diff.sqrMagnitude > 1)
						currentCrop.transform.position -= speed*diff;
				else
						IsFilling = false;

	}


}
