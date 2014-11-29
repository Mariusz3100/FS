using UnityEngine;
using System.Collections;

public class CropTypeData : MonoBehaviour
{
	int cropTypes=0;
	public CropType[] typesList;

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	public class CropType
	{
		string name;
		Texture2D tex;
	}
}

