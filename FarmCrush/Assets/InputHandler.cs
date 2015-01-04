using UnityEngine;
using System.Collections;

public class InputHandler : MonoBehaviour
{
	static bool inputBlocked=false;
	Vector2 selectedField=new Vector2(-1,-1);
	private Siatka siatka;
	private Vector3 start;
	private Vector3 end;
	private Vector3 offScreenPosition;
	bool shouldInputBeBlocked;

	public bool ShouldInputBeBlocked {
		get {
			return shouldInputBeBlocked;
		}
		set {
			shouldInputBeBlocked = value;
		}
	}

	public static bool InputBlocked {
		get {
			return inputBlocked;
		}
		set {
			inputBlocked = value;
		}
	}

		// Use this for initialization
		void Start ()
		{
		siatka = Siatka.getSingleton ();
		offScreenPosition = transform.position;
		start = siatka.fields [1] [0].transform.position - new Vector3 (siatka.PieceSizeX, -siatka.PieceSizeY, 0) / 2;
		end = siatka.fields [siatka.fields.Length - 1] [siatka.fields [0].Length - 1].transform.position + new Vector3 (siatka.PieceSizeX, -siatka.PieceSizeY, 0) / 2;
		InputBlocked = true;
		}
	
		// Update is called once per frame
		void Update ()
		{
				

			Vector3 mPos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			


			if (!inputBlocked) {
						{
								Vector2[] matches = siatka.checkForMatches ();
								
							while(matches.Length>0){
									siatka.executeMatches (matches);
									matches = siatka.checkForMatches ();
								}
								
								if (Input.GetMouseButtonDown (0)) 
								{
					



							
										if (mPos.x > start.x && mPos.y < start.y && mPos.x < end.x && mPos.y > end.y) {
					
												if (selectedField.x < 0) {


											
														assignSelectedField (mPos);
						
							
												} else {

														if (mPos.x > start.x && mPos.y < start.y && mPos.x < end.x && mPos.y > end.y) {
																Vector2 clickedField = new Vector2 ();
																clickedField.x = Mathf.Abs (mPos.x - start.x) / siatka.PieceSizeX;
																clickedField.y = Mathf.Abs (mPos.y - start.y) / siatka.PieceSizeY + 1;
													
																if (checkIfAdjacent (clickedField)) {
																		switchCrops (clickedField);
							
																		putOutOfScreen ();
			//															Vector2[] result = siatka.checkForMatches ();
														
			//															siatka.executeMatches (result);
																} else {
																		assignSelectedField (mPos);
																}
				
														}
												}
										}
								}

						}

				}

		inputBlocked = false;
		}

	void switchCrops (Vector2 clickedField)
	{
		Crop temp = siatka.fields [(int)clickedField.y] [(int)clickedField.x].CurrentCrop;
		siatka.fields [(int)clickedField.y] [(int)clickedField.x].CurrentCrop = siatka.fields [(int)selectedField.y] [(int)selectedField.x].CurrentCrop;
		siatka.fields [(int)selectedField.y] [(int)selectedField.x].CurrentCrop = temp;
		siatka.fields [(int)selectedField.y] [(int)selectedField.x].IsFilling = true;
		siatka.fields [(int)clickedField.y] [(int)clickedField.x].IsFilling = true;
	}

	public bool checkIfAdjacent(Vector2 clickedField){

		int xDiff = Mathf.Abs ((int)clickedField.x - (int)selectedField.x);
		int yDiff = Mathf.Abs ((int)clickedField.y - (int)selectedField.y);


		return xDiff+yDiff==1;
	
	
	}


	public void putOutOfScreen()
	{
		transform.position = offScreenPosition;
		selectedField.x = -1;
		selectedField.y = -1;
	}

	public void assignSelectedField (Vector3 mPos)
	{

		selectedField.x = Mathf.Abs (mPos.x - start.x) / siatka.PieceSizeX;
		selectedField.y = Mathf.Abs (mPos.y - start.y) / siatka.PieceSizeY + 1;
		Vector3 newPos = siatka.fields [(int)selectedField.y] [(int)selectedField.x].transform.position;
		newPos.z = transform.transform.position.z;
		transform.transform.position = newPos;
	}

}

