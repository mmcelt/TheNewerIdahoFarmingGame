using Endgame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WLListViewManager : MonoBehaviour
{
	#region Fields & Properties

	[SerializeField] ListView _theListView;
	[SerializeField] WinnerList _winnerList;

	const int _columnWidthCount = 1;

	int columnCount
	{
		get
		{
			return _theListView.Columns.Count;
		}
	}

	int[] _columnWidths = new int[_columnWidthCount];
	int[] _columnWidthStates = null;

	int _selectedIndex;
	int _tester;

	Winners _winners;

	#endregion

	#region Getters


	#endregion

	#region MonoBehaviour Methods

	void Awake()
	{
		Debug.Log("WL LISTVIEW IS AWAKE");
		//_pManager = _uiManager._pManager;

		// Initialize an array with some example column widths
		// that will be toggled between by clicking on the column header.
		// (-1 in Windows Forms means size to the longest item, and
		// -2 means size to the column header.)
		_columnWidths[0] = -2;
		_columnWidths[1] = -1;
		_columnWidths[2] = -2;
		_columnWidths[3] = -2;
		_columnWidths[4] = -2;

	}

	void Start() 
	{
		for (int index = 0; index < columnCount; index++)
		{
			_columnWidthStates[index] = 0;
		}

		SetupListView();

		_theListView.Columns[0].Width = 180;
		_theListView.Columns[1].Width = 480;
		_theListView.Columns[2].Width = 380;
		_theListView.Columns[3].Width = 380;
		_theListView.Columns[4].Width = 180;

	}
	#endregion

	#region Public Methods

	public void AddListViewDownPaymentItems()
	{
		_theListView.Items.Clear();
		_theListView.SelectedIndices.Clear();

		int counter = 0;
		//Debug.Log("OTBs" + _pManager._myOtbs.Count);
		foreach (var item in _winners.winnerListEntryList)
		{
			_theListView.Items.Add(AddItem(item));

			counter++;
			Debug.Log("Counter: " + counter);
		}

		Invoke(nameof(GetItemCustomButtons), 0.1f);
	}

	public void ListViewItemButtonHandler()
	{
		string targetListView = _theListView.name;

		Debug.Log($"UC Selected Index: {_theListView.SelectedIndices[0]}");
		//Debug.Log($"ListView: {targetListView}");

		//_uCheester.OnDownpaymentChanged(_theListView.SelectedIndices[0]);
	}
	#endregion

	#region Private Methods

	void GetItemCustomButtons()
	{
	WinnersListCustomButton[] itemButtons = FindObjectsOfType<WinnersListCustomButton>();

		foreach (WinnersListCustomButton button in itemButtons)
		{
			button.onClick.AddListener(ListViewItemButtonHandler);
		}
	}

	void SetupListView()
	{
		if (_theListView == null)
		{
			Debug.Log("UC ListView is null!");
			return;
		}

		_theListView.SuspendLayout();
		AddColumn("Date");
		AddColumn("   Player");
		AddColumn("Winning Networth");
		AddColumn("Game End/Time");
		AddColumn("NOP");
		_theListView.ResumeLayout();
	}

	void AddColumn(string title)
	{
		ColumnHeader column = new ColumnHeader();
		column.Text = title;
		_theListView.Columns.Add(column);
	}

	ListViewItem AddItem(WinnerEntry newItem)
	{
		_tester++;
		string date = newItem.date.ToString();
		string winnerName = newItem.playerName;
		string farmerName = newItem.farmerName;

		string winningNetworth = newItem.networth.ToString();
		
		string[] data = new string[]
		{
			
		};

		Debug.Log("Tester: " + _tester + ":" + newItem);

		ListViewItem item = new ListViewItem(data);
		return item;
	}

	#endregion
}
