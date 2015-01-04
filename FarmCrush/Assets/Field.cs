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


	void Start () {
		empty = true;
		isFilling = false;
	}

	public void Update () {
		updateThisField ();
		}


	public void updateThisField () {
		if (Empty) {
			InputHandler.InputBlocked=true;
			tryFill ();
		} else {

			moveCropToThisField();
			if(isFilling)
				InputHandler.InputBlocked=true;

		//		InputHandler.InputBlocked=false;
			else{

			}
		}

	}

	 

	public virtual void tryFill(){
		if (fieldAbove.isFilling||fieldAbove.Empty)
						return;
		currentCrop=FieldAbove.currentCrop;
		fieldAbove.currentCrop = null;
		fieldAbove.Empty = true;
		isFilling = true;
		Empty = false;

	}

	public void moveCropToThisField(){
		if (currentCrop == null) {
						//			throw new UnityException("Crop shouldn't be null here");
				}else {
						Vector3 diff = currentCrop.transform.position - transform.position;
						if(diff.y<0||diff.sqrMagnitude > 0.005)
								currentCrop.transform.position -= Crop.speed * diff.normalized;
						else
								IsFilling = false;
				}
	}

	public  void emptyThisField(){
		Empty = true;
		currentCrop.remove ();
	}


}
