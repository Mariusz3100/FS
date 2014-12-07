using UnityEngine;
using System.Collections;

public class CropTypeData : MonoBehaviour
{
	int cropTypes=0;
	public string[] namesList;
	public Crop[] CropList;

	private static CropTypeData instance;

	public static CropTypeData Instance {
		get {
			return instance;
		}
	}

	// Use this for initialization
	void Start ()
	{
		if(instance!=null)
			throw new UnityException("More than one instance of CropTypeData");
		instance = this;

		if(CropList.Length!=namesList.Length)
			throw new UnityException("Inconsistent number of crop names/objects");
		cropTypes=CropList.Length;

		for (int i=0; i<this.CropList.Length; i++) 
		{
			this.CropList[i].Name=namesList[i];
			this.CropList[i].Type=i;

		}
	}
	
	public Crop getRandomNewCrop()
	{
		int randInt=(int)(Random.Range (0, CropList.Length));
		if(randInt==CropList.Length)
			randInt=(int)(Random.Range (0, CropList.Length));

		Crop newCrop= (Crop)Instantiate(CropList[randInt]);
		return newCrop;

	}


}

