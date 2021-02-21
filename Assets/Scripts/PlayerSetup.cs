using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class PlayerSetup : MonoBehaviourPun
{
	#region Public / Serialized Fields

	[SerializeField] GameObject uiPrefab;
	[SerializeField] GameObject diePrefab;
	[SerializeField] GameObject chatManagerPrefab;
	[SerializeField] Texture2D _cursor;

	[SerializeField] Vector2 _hotSpot;

	#endregion

	#region Private Fields / References



	#endregion

	#region Properties


	#endregion

	#region MonoBehaviour Methods

	void Awake()
	{
		if (photonView.IsMine)
		{
			//instantiate the Player's UI
			GameObject myUI = Instantiate(uiPrefab);
			myUI.transform.localScale = Vector3.one;
			myUI.transform.SetParent(transform);
			//instantiate the animated Die
			GameObject die = Instantiate(diePrefab);
			GameManager.Instance.myDiceRoll = die.GetComponentInChildren<MyDiceRoll>();
			GameManager.Instance.myFarmer = gameObject;
			//_hotSpot = new Vector2(40f, 41f);
			Cursor.SetCursor(_cursor, new Vector2(_hotSpot.x, _hotSpot.y), CursorMode.ForceSoftware);
			if(PhotonNetwork.PlayerList.Length > 1)
			{
				GameObject myChatManager = Instantiate(chatManagerPrefab);
				myChatManager.transform.localScale = Vector3.one;
				myChatManager.transform.SetParent(transform);
			}
		}
		else
		{
			//disable remote movement & GameTimer scripts
			GetComponent<PlayerMove>().enabled = false;
			GetComponent<GameTimer>().enabled = false;
		}
	}
	#endregion

	#region Public Methods


	#endregion

	#region Private Methods

	#endregion
}
