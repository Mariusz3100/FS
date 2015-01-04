using UnityEngine;
using System.Collections;

public class Nettle : Crop
{
	int nettlePoints=1;


	
	public override void remove(){

		base.remove ();
	}

		// Use this for initialization
		void Start ()
		{
			Points = nettlePoints;
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}
}

