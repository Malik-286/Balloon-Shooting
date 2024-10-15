using UnityEditor;
using UnityEngine;

///Developed by Indie Studio
///https://assetstore.unity.com/publishers/9268
///www.indiestd.com
///info@indiestd.com

[InitializeOnLoad]
public class WelcomePopup : EditorWindow
{
	private static WelcomePopup window;
	private static bool initilized;
	private static bool dontShowWeclomeMessageAgain;
	private static Vector2 size = new Vector2 (600, 300);
	private static string strKey = "2DArcEditor_WelcomePopup";
	private static Sprite logo;

	static WelcomePopup ()
	{
		EditorApplication.update += Update;
	}

	[MenuItem ("Tools/2DArcheryToolkit/Welcome", false, 1)]
	static void ReadManual ()
	{
		initilized = false;
		PlayerPrefs.SetInt (strKey, CommonUtil.TrueFalseBoolToZeroOne (false));
		Init ();
	}

	[MenuItem ("Tools/2DArcheryToolkit/About Author", false, 2)]
	static void AboutMe ()
	{
		Application.OpenURL (Links.authorPath);
	}

	private static void Init ()
	{

		if (initilized) {
			return;
		}

		if (PlayerPrefs.HasKey (strKey)) {
			dontShowWeclomeMessageAgain = CommonUtil.ZeroOneToTrueFalseBool (PlayerPrefs.GetInt (strKey));
		}

		if (dontShowWeclomeMessageAgain) {
			return;
		}

		if (logo == null)
		{
			logo = Resources.Load("Author/logo", typeof(Sprite)) as Sprite;
		}

		window = (WelcomePopup)EditorWindow.GetWindow (typeof(WelcomePopup));
		window.titleContent.text = "Welcome";
		window.maxSize = size;
		window.maximized = true;
		window.position = new Rect ((Screen.currentResolution.width - size.x) / 2, (Screen.currentResolution.height - size.y) / 2, size.x, size.y);
		window.Show ();
		window.Focus ();

		initilized = true;

		PlayerPrefs.SetInt (strKey, CommonUtil.TrueFalseBoolToZeroOne (true));
	}

	static void Update ()
	{
		if (Application.isPlaying) {
			if (window != null) {
				window.Close ();
				window = null;
			}
			return;
		}

		if (window == null) {
			Init ();
		}
	}

	void OnGUI ()
	{if (window == null) {
			return;
		}

		EditorGUILayout.Separator ();
		EditorGUILayout.LabelField ("2DArcheryToolkit " + Links.versionCode, EditorStyles.boldLabel);
		EditorGUILayout.Separator ();

		EditorGUILayout.TextArea ("Thank you for buying/downloading 2DArcheryToolkit Package.\nIf you have any questions, suggestions, comments , feature requests or bug detected,\ndo not hesitate to Contact US.\n", GUI.skin.label);
		EditorGUILayout.Separator ();

		EditorGUILayout.TextArea ("We always strive to provide high quality assets. If you have enjoyed with 2DArcheryToolkit,\nwe would be happy if you would spend few minutes and write a review for us on the \n" + Links.storeName + ".\n", GUI.skin.label);
		EditorGUILayout.Separator ();

		EditorGUILayout.TextArea ("If one of the buttons below does not work , you will find its file under the package folder.\n", GUI.skin.label);

		EditorGUILayout.BeginHorizontal ();
		GUI.backgroundColor = Colors.yellowColor;
		if (GUILayout.Button ("Read the Manual", GUILayout.Width (120), GUILayout.Height (22))) {
			Application.OpenURL (Links.docPath);
		}
		GUI.backgroundColor = Colors.whiteColor;

		if (GUILayout.Button ("Version Changes", GUILayout.Width (120), GUILayout.Height (22))) {
			Application.OpenURL (Links.versionChangesPath);
		}

		if (GUILayout.Button ("More Assets", GUILayout.Width (100), GUILayout.Height (22))) {
			Application.OpenURL (Links.indieStudioStoreURL);
		}

		if (GUILayout.Button ("Contact US", GUILayout.Width (100), GUILayout.Height (22))) {
			Application.OpenURL (Links.indieStudioContactUsURL);
		}

		EditorGUILayout.EndHorizontal ();

		EditorGUILayout.Separator ();
		EditorGUILayout.Separator ();

		EditorGUILayout.BeginHorizontal();
		GUILayout.Space(size.x / 2.0f - 100);
		if (logo != null)
		{
			int logoSize = 160;
			GUI.DrawTexture(new Rect(size.x / 2 - logoSize / 2, size.y - logoSize / 1.3f, logoSize, logoSize), logo.texture, ScaleMode.ScaleToFit);
			GUILayout.Space(50);
		}
		EditorGUILayout.EndHorizontal();
	}

	void OnInspectorUpdate ()
	{
		Repaint ();
	}
}
