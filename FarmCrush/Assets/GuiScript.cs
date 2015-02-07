using UnityEngine;
using System.Collections;

public class GuiScript : MonoBehaviour
{

	private static GuiScript instance;

	public GUIText score;
		// Use this for initialization
		void Start ()
		{
		score.text = "0";
			if(instance!=null)
				throw new UnityException("More than one instance of Siatka");
			instance = this;
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}


	public static GuiScript getSingleton()
	{
		return instance;
	}

	public void addPoints(int newPoints)
	{
		int pointsBefore=int.Parse (score.text);
		pointsBefore += newPoints;
		score.text = pointsBefore.ToString();
	}

}

