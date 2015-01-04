using UnityEngine;
using System.Collections;

public class Wheat : Crop
{
	int wheatPoints=10;


	
	public override void remove(){

		base.remove ();
	}

		// Use this for initialization
		void Start ()
		{
			Points = wheatPoints;
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}
}

