using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FlieTester : MonoBehaviour
{
	#region Fields & Properties

	string path;

	#endregion

	#region Getters


	#endregion

	#region Unity Methods

	void Start() 
	{
		path = Application.persistentDataPath;
	}
	
	void Update() 
	{
		
	}
	#endregion

	#region Public Methods

	public void OnReadFileButtonClicked()
	{
		string file = path + "/MyTest.txt";
		if (!File.Exists(file))
		{
			//create the file...
			using (StreamWriter sw = File.CreateText(file))
			{
				sw.WriteLine("Hello");
				sw.WriteLine("And");
				sw.WriteLine("Welcome");
			}
		}

		//Open the file to read from...
		using (StreamReader sr = File.OpenText(file))
		{
			string s;
			while ((s = sr.ReadLine()) != null)
			{
				Debug.Log(s);
			}
		}
	}

	public void OnWriteFileButtonClicked()
	{

	}
	#endregion

	#region Private Methods


	#endregion
}
