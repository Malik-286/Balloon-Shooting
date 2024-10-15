using UnityEngine;
using System.Collections;


public class SingletonManager : MonoBehaviour
{
	public GameObject[] values;

	void Awake ()
	{
		InstantiateValues ();
	}


	private void InstantiateValues ()
	{
		if (values == null) {
			return;
		}

		foreach (GameObject value in values) {
			if (GameObject.Find (value.name) == null) {
				GameObject temp = Instantiate (value, Vector3.zero, Quaternion.identity) as GameObject;
				temp.transform.eulerAngles = value.transform.eulerAngles;
				temp.name = value.name;
			}
		}
	}
}
