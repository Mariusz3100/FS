using UnityEngine;
using System.Collections;

public class Crop : MonoBehaviour {
	private int type=0;
	private string cropName="";

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
}
