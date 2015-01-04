using UnityEngine;
using System.Collections;

public class Potatoes : Crop
{
	int potatoesPoints=80;

	public override void remove(){

		base.remove ();
	}

		// Use this for initialization
		void Start ()
		{
			Points = potatoesPoints;
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}
}

