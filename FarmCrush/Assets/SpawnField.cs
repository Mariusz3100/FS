using UnityEngine;
using System.Collections;

public class SpawnField : Field
{

	public override void tryFill(){

		if (!IsFilling) {
			this.CurrentCrop = CropTypeData.Instance.getRandomNewCrop ();
			this.CurrentCrop.transform.position=transform.position;
			Empty=false;
		}
		
	}	

		
}

