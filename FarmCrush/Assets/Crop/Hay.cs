using UnityEngine;
using System.Collections;

public class Hay : Crop
{
	int hayPoints=50;



		public override void remove(){
			
			base.remove ();
		}

		// Use this for initialization
		void Start ()
		{
			base.Points = hayPoints;
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}



}

