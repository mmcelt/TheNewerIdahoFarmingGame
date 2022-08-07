using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Tester : MonoBehaviour
{
	#region Fields & Properties

	public TMP_Text _testText;

	[SerializeField] int _testNum, _doubler;

	#endregion

	#region Getters


	#endregion

	#region Unity Methods

	void Start() 
	{
		_testText.text = $"{_testNum *= (int)(Mathf.Pow(2,_doubler))}";
	}
	#endregion

	#region Public Methods


	#endregion

	#region Private Methods


	#endregion
}
