using UnityEngine;
using System.Collections;

public class CropTypeData : MonoBehaviour
{
	int cropTypes=0;
	public string[] namesList;
	public GameObject[] objectList;

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

		if(objectList.Length!=namesList.Length)
			throw new UnityException("Inconsistent number of crop names/objects");
	}
	
	// Update is called once per frame
	void Update ()
	{
	}


}

