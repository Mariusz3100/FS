using UnityEngine;
using System.Collections;

public class SpawnField : Field
{

	public override void tryFill(){

		if (!IsFilling) {
			IsFilling = true;
			this.CurrentCrop = CropTypeData.Instance.getRandomNewCrop ();
			Empty=false;
		}
		
	}	

		
}

