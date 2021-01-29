using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Linq;
using Photon.Pun;
using ExitGames.Client.Photon;
using Photon.Realtime;
using DG.Tweening;

public class WinnerList : MonoBehaviour
{
	#region Public / Serialized Fields

	[SerializeField] Transform _content;
	[SerializeField] GameObject _winnerEntryPerfab;
	public GameObject _winnersPanel;
	public Transform _ruContent;
	public GameObject _runnerUpEntryPrefab;
	public GameObject _runnersUpPanel;

	#endregion

	#region Private Fields / References

	UIManager _uiManager;
	Winners _winners = null;

	string _myDataLocation;

	#endregion

	#region Properties

	public static WinnerList Instance { get; private set; }

	#endregion

	#region MonoBehaviour Methods

	void OnEnable()
	{
		PhotonNetwork.NetworkingClient.EventReceived += OnUpdateWinnersListEventReceived;
	}

	void OnDisable()
	{
		PhotonNetwork.NetworkingClient.EventReceived -= OnUpdateWinnersListEventReceived;
	}

	void Awake()
	{
		if (Instance == null)
			Instance = this;
		else
			Destroy(gameObject);
	}

	void Start()
	{
		//_myDataLocation = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData) + "/IFG";
		//_myDataLocation = Path.GetFullPath(".");
		_myDataLocation = Application.persistentDataPath;
		//Debug.Log(_myDataLocation);

		_uiManager = GameManager.Instance.uiManager;

		if (GameManager.Instance._numberOfPlayers > 1)
			SendListToOthers();
	}

	#endregion

	#region Public Methods

	public void AddWinnerListEntry(string pName, string fName, int nw, int endGameNW, int nop, string[] runnersUpNames, string[] runnerUpFarmers, int[] runnerUpNetworkths)
	{
		//create WinnerListEntryReadAllText
		WinnerEntry winnerListEntry = new WinnerEntry
		{
			date = System.DateTime.Now.ToShortDateString(),
			//date = testDate,
			playerName = pName,
			farmerName = fName,
			networth = nw,
			endGameNetworth = endGameNW,
			numberOfPlayers = nop,
			rPlayerNames = runnersUpNames,
			rFarmerNames = runnerUpFarmers,
			rNetworths = runnerUpNetworkths
		};
		//load saved data...
		if (File.Exists(_myDataLocation + "/WinnerList.json"))
		{
			string jsonString = File.ReadAllText(_myDataLocation + "/WinnerList.json");
			_winners = JsonUtility.FromJson<Winners>(jsonString);

			////open the file to read...
			//using (StreamReader sr = File.OpenText(_myDataLocation + "/WinnerList.json"))
			//{
			//	string jsonString;
			//	while ((jsonString = sr.ReadLine()) != null)
			//	{
			//		_winners = JsonUtility.FromJson<Winners>(jsonString);
			//	}
			//}
		}
		else
		{
			Debug.Log("No Saved List Found!");
			////create the file...
			//using(StreamWriter sw = File.CreateText(_myDataLocation + "/WinnerList.json"))
			//{
			//	string jsonString = "";
			//	sw.WriteLine(jsonString);
			//}
		}

		if (_winners == null)
		{
			//no stored list, initialize
			_winners = new Winners()
			{
				winnerListEntryList = new List<WinnerEntry>()
			};

			Debug.Log("NO WINNERS LIST...");
		}

		//add new entry to Winners
		_winners.winnerListEntryList.Add(winnerListEntry);

		//save updated Winners
		string json = JsonUtility.ToJson(_winners);
		//File.WriteAllText(_myDataLocation, json);

		using (StreamWriter sw = File.CreateText(_myDataLocation + "/WinnerList.json"))
		{
			//string jsonString = "";
			sw.WriteLine(json);
		}


		if (GameManager.Instance._numberOfPlayers > 1)
			SendListToOthers();
	}

	void SendListToOthers()
	{
		if (!PhotonNetwork.IsMasterClient) return;

		//load saved data...
		if (File.Exists(_myDataLocation + "/WinnerList.json"))
		{
			string jsonString = File.ReadAllText(_myDataLocation + "/WinnerList.json");
			_winners = JsonUtility.FromJson<Winners>(jsonString);
		
		//using (StreamReader sr = File.OpenText(_myDataLocation + "/WinnerList.json"))
		//	{
		//		string jsonString;
		//		while ((jsonString = sr.ReadLine()) != null)
		//		{
		//			_winners = JsonUtility.FromJson<Winners>(jsonString);
		//		}
		//	}
		}
		else
		{
			Debug.LogWarning("NO DAMN LIST PRESENT!");
			//File.Create(_myDataLocation + "/WinnerList.json");
			return;
		}

		if (GameManager.Instance._numberOfPlayers > 1)
		{
			//MasterClient send his list to all
			if (PhotonNetwork.IsMasterClient)
			{
				//event data
				string myList = File.ReadAllText(_myDataLocation + "/WinnerList.json");
				//string myList;
				//using (StreamReader sr = File.OpenText(_myDataLocation + "/WinnerList.json"))
				//{
				//	//string jsonString;
				//	while ((myList = sr.ReadLine()) != null)
				//	{
				//		_winners = JsonUtility.FromJson<Winners>(myList);
				//	}
				//}

				object[] sndData = new object[] { myList };
				//event options
				RaiseEventOptions eventOptions = new RaiseEventOptions
				{
					Receivers = ReceiverGroup.Others,
					CachingOption = EventCaching.DoNotCache
				};
				//send options
				SendOptions sendOptions = new SendOptions { Reliability = true };
				//fire the event...
				PhotonNetwork.RaiseEvent((byte)RaiseEventCodes.Update_WinnersList_Event_Code, sndData, eventOptions, sendOptions);
			}
		}
	}

	public void PopulateAndShowWinnersList()
	{
		_uiManager._winnersListPanel.SetActive(true);

		List<GameObject> winnerPrefabs = GameObject.FindGameObjectsWithTag("WinnerPrefab").ToList();

		//Debug.LogWarning("INSIDE PASWL...");

		for (int i = 0; i < winnerPrefabs.Count; i++)
		{
			//Debug.Log("INSIDE DESTROY");
			Destroy(winnerPrefabs[i]);
		}

		if (_winners == null)
		{
			Debug.Log("_winners is NULL");

			if (File.Exists(_myDataLocation + "/WinnerList.json"))
			{
				string jsonString = File.ReadAllText(_myDataLocation + "/WinnerList.json");
				_winners = JsonUtility.FromJson<Winners>(jsonString);

				////open the file to read...
				//using (StreamReader sr = File.OpenText(_myDataLocation + "/WinnerList.json"))
				//{
				//	string jsonString;
				//	while ((jsonString = sr.ReadLine()) != null)
				//	{
				//		_winners = JsonUtility.FromJson<Winners>(jsonString);
				//	}
				//}
			}
			else
			{
				Debug.LogError("NO WINNERS LIST FOUND!");
				
				return;
			}
		}
		foreach (WinnerEntry winnerEntry in _winners.winnerListEntryList)
		{
			//Debug.Log("INSIDE FOREACH");
			//main winner entries
			GameObject listEntry = Instantiate(_winnerEntryPerfab, _content);
			listEntry.transform.localScale = Vector3.one;
			listEntry.GetComponent<WinnerInitializer>().Initialize(winnerEntry.date, winnerEntry.playerName, winnerEntry.farmerName, winnerEntry.networth, winnerEntry.endGameNetworth, winnerEntry.numberOfPlayers, winnerEntry.rPlayerNames, winnerEntry.rFarmerNames, winnerEntry.rNetworths);
		}

		if (_uiManager == null)
			_uiManager = GameManager.Instance.uiManager;
	}

	public void OnCloseWPButtonClicked()
	{
		_winnersPanel.SetActive(false);
		_uiManager._optionsPanel.SetActive(true);
		_uiManager._optionsPanel.GetComponent<DOTweenAnimation>().DOPlayForward();

	}

	public void OnCloseRUPButtonClicked()
	{
		_runnersUpPanel.SetActive(false);
	}

	public void OnShowWinnerListButtonClicked()
	{
		PopulateAndShowWinnersList();
	}

	public void ResetWinnersList()
	{
		if (!PhotonNetwork.IsMasterClient) return;

		//copy the MasterList to the WinnerList...
		if (File.Exists(_myDataLocation + "/MasterList.txt"))
		{
			File.Copy(_myDataLocation + "/MasterList.txt", _myDataLocation + "/WinnerList.json", true);
		}
		PopulateAndShowWinnersList();
	}

	public void MakeTheMasterList()
	{
		if (File.Exists(_myDataLocation + "/WinnerList.json"))
		{
			File.Copy(_myDataLocation + "/WinnerList.json", _myDataLocation + "/MasterList.txt", true);
		}
	}
	#endregion

	#region Private Methods

	void OnUpdateWinnersListEventReceived(EventData eventData)
	{
		if (eventData.Code == (byte)RaiseEventCodes.Update_WinnersList_Event_Code)
		{
			//extract data
			object[] recData = (object[])eventData.CustomData;
			string jsonString = (string)recData[0];

			//File.WriteAllText(Application.persistentDataPath + "/WinnerList.json", jsonString);
			using (StreamWriter sw = File.CreateText(_myDataLocation + "/WinnerList.json"))
			{
				//string jsonString = "";
				sw.WriteLine(jsonString);
			}

			if (File.Exists(Application.persistentDataPath + "/WinnerList.json"))
			{
				//open the file to read...
				jsonString = File.ReadAllText(Application.persistentDataPath + "/WinnerList.json");
				_winners = JsonUtility.FromJson<Winners>(jsonString);

				//using (StreamReader sr = File.OpenText(_myDataLocation + "/WinnerList.json"))
				//{
				//	//string jsonString;
				//	while ((jsonString = sr.ReadLine()) != null)
				//	{
				//		_winners = JsonUtility.FromJson<Winners>(jsonString);
				//	}
				//}
			}
			else
			{
				Debug.Log("No Saved List Found!");
			}
		}
	}
	#endregion
}

class Winners  //High Scores
{
	public List<WinnerEntry> winnerListEntryList;
}

[System.Serializable]
class WinnerEntry //HighscoreEntry
{
	//winner & game data
	public string date;
	public string playerName;
	public string farmerName;
	public int networth;
	public int endGameNetworth;
	public int numberOfPlayers;
	//runners up data
	public string[] rPlayerNames;
	public string[] rFarmerNames;
	public int[] rNetworths;
}
