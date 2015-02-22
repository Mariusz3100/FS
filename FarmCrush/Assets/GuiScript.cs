using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Parse;
using System;
using Facebook;
using Facebook.MiniJSON;
using System.Linq;

public class GuiScript : MonoBehaviour
{
	public GUISkin menuSkin;

	Rect loginButtonRect;
	Rect logoutButtonRect;
	int highscore=0;

	Dictionary<string, string> userProfile;

	private static GuiScript instance;

	public GUIText score;
		// Use this for initialization
		void Start ()
		{
		score.text = "0";
			if(instance!=null)
				throw new UnityException("More than one instance of Siatka");
			instance = this;

		float loginButtonWidth =180.0f;
		float loginButtonHeight =  38.0f;
		float loginButtonX = (Screen.width - loginButtonWidth) / 2.0f;
		loginButtonRect = new Rect (loginButtonX,10,loginButtonWidth,loginButtonHeight);
		
		float logoutButtonWidth = 90.0f;
		float logoutButtonHeight = 38.0f;
		float logoutButtonX = (Screen.width - logoutButtonWidth) / 2.0f;
		logoutButtonRect = new Rect (logoutButtonX,10,logoutButtonWidth,logoutButtonHeight);
	
	
	}
	
		// Update is called once per frame
		void Update ()
		{
	
		}

	void Awake()
	{
		enabled = false;
		FB.Init(SetInit, OnHideUnity);
	}

	private void OnHideUnity(bool isGameShown) {

	}
	
	private void SetInit()
	{
		enabled = true;


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

	void OnGUI() {


	
		GUI.skin = menuSkin;
//		FBLogin();
		// Check if no Parse user or not logged into Facebook
		// if ((ParseUser.CurrentUser == null) || !FB.IsLoggedIn) {

//		GUI.Button (loginButtonRect, "", "xxx");
	//	GUI.Button (loginButtonRect, "xxxx", "loginButton");
		if (!FB.IsLoggedIn) {
			if (GUI.Button(loginButtonRect, "fblogin", "loginButton"))
			{
				FBLogin();

			}
		} else {
			if (GUI.Button(logoutButtonRect, "", "logoutButton"))
			{
				ParseFBLogout();
			}
			IEnumerable<ParseObject> results=null;

			if (userProfile!=null) {
				Debug.Log("FBid for Q:"+userProfile["facebookId"]);

				var query = ParseObject.GetQuery("GameScore")
					.WhereEqualTo("playerId", userProfile["facebookId"]);
				query.FindAsync().ContinueWith(t =>
				                               {
					results = t.Result;
				});
				
			}
			Debug.Log("Query results:"+results);
			saveScore() ;
			if(userProfile==null)
				Debug.Log("null!!");
			else{
				Debug.Log("FBid:"+userProfile["facebookId"]);
				Debug.Log(userProfile["name"]);
			}
		}
	}

	private IEnumerator saveUserProfile(Dictionary<string, string> profile) {
		var user = ParseUser.CurrentUser;
		user["profile"] = profile;
		// Save if there have been any updates
		if (user.IsKeyDirty("profile")) {
			var saveTask = user.SaveAsync();
			while (!saveTask.IsCompleted) yield return null;
			UpdateProfile();
		}
	}

	private IEnumerator ParseLogin() {
		if (FB.IsLoggedIn) {
			// Logging in with Parse
			System.Threading.Tasks.Task<ParseUser> loginTask = ParseFacebookUtils.LogInAsync(FB.UserId,FB.AccessToken, FB.AccessTokenExpiresAt);
			while (!loginTask.IsCompleted) yield return null;
			// Login completed, check results
			if (loginTask.IsFaulted || loginTask.IsCanceled) {
				// There was an error logging in to Parse
				foreach(var e in loginTask.Exception.InnerExceptions) {
					ParseException parseException = (ParseException) e;
					Debug.Log("ParseLogin: error message " + parseException.Message);
					Debug.Log("ParseLogin: error code: " + parseException.Code);
				}
			} else {
				// Log in to Parse successful
				// Get user info
				FB.API("/me", HttpMethod.GET, FBAPICallback);
				// Display current profile info
				UpdateProfile();
			}
		}
	}

	private void FBLogin() {
		// Logging in with Facebook
		FB.Login("user_about_me, user_relationships, user_birthday, user_location", FBLoginCallback);
	}

	private void UpdateProfile() {
		}

	private void saveScore() {

		if (int.Parse (score.text) > highscore) 
			if(userProfile!=null)
		{
		
			ParseObject gameScore = new ParseObject ("GameScore");
			gameScore ["score"] = int.Parse (score.text);
			gameScore ["playerName"] = userProfile["name"];
			gameScore ["playerID"]=userProfile["facebookId"];
			System.Threading.Tasks.Task saveTask = gameScore.SaveAsync ();
			highscore=int.Parse (score.text);
		}
	}
	
	

	private void FBLoginCallback(FBResult result) {
		// Login callback
		if(FB.IsLoggedIn) {
//			showLoggedIn();
			StartCoroutine("ParseLogin");
		} else {
			Debug.Log ("FBLoginCallback: User canceled login");
		}
	}
	
	private void ParseFBLogout() {
		FB.Logout();
		ParseUser.LogOut();
//		showLoggedOut();
	}
	
	private void FBAPICallback(FBResult result)
	{
		if (!String.IsNullOrEmpty(result.Error)) {
			Debug.Log ("FBAPICallback: Error getting user info: + "+ result.Error);
			// Log the user out, the error could be due to an OAuth exception
			ParseFBLogout();
		} else {
			// Got user profile info
			var resultObject = Json.Deserialize(result.Text) as Dictionary<string, object>;
			userProfile = new Dictionary<string, string> ();
			
			userProfile["facebookId"] = getDataValueForKey(resultObject, "id");
			userProfile["name"] = getDataValueForKey(resultObject, "name");
			object location;
			if (resultObject.TryGetValue("location", out location)) {
				userProfile["location"] = (string)(((Dictionary<string, object>)location)["name"]);
			}
			userProfile["gender"] = getDataValueForKey(resultObject, "gender");
			userProfile["birthday"] = getDataValueForKey(resultObject, "birthday");
			userProfile["relationship"] = getDataValueForKey(resultObject, "relationship_status");
			if (userProfile["facebookId"] != "") {
				userProfile["pictureURL"] = "https://graph.facebook.com/" + userProfile["facebookId"] + "/picture?type=large&return_ssl_resources=1";
			}
			
			var emptyValueKeys = userProfile
				.Where(pair => String.IsNullOrEmpty(pair.Value))
					.Select(pair => pair.Key).ToList();
			foreach (var key in emptyValueKeys) {
				userProfile.Remove(key);
			}
			
			StartCoroutine("saveUserProfile", userProfile);
		}
	}

	private string getDataValueForKey(Dictionary<string, object> dict, string key) {
		object objectForKey;
		if (dict.TryGetValue(key, out objectForKey)) {
			return (string)objectForKey;
		} else {
			return "";
		}
	}
}

