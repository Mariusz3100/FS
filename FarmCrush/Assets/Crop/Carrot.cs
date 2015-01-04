using UnityEngine;
using System.Collections;

public class Carrot : Crop
{
	int carrotPoints=100;


		public override void remove(){
			
			base.remove ();
		}

		// Use this for initialization
		void Start ()
		{
			Points = carrotPoints;
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}
}

