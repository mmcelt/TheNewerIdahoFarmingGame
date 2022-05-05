using Endgame;
using ExitGames.Client.Photon;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class OTBListViewManager: MonoBehaviour
{
	#region Fields & Properties

	[SerializeField] ListView _theListView;
	[SerializeField] UIManager _uiManager;

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

	public PlayerManager _pManager;

	#endregion

	#region Getters


	#endregion

	#region MonoBehaviour Methods

	void Awake()
	{
		//Debug.Log("OTB LISTVIEW IS AWAKE");
		//_pManager = _uiManager._pManager;

		// Initialize an array with some example column widths
		// that will be toggled between by clicking on the column header.
		// (-1 in Windows Forms means size to the longest item, and
		// -2 means size to the column header.)
		_columnWidths[0] = -2;
		//_columnWidths[1] = -1;
		//columnWidths[2] = -2;

	}

	void Start()
	{
		for (int index = 0; index < columnCount; index++)
		{
			_columnWidthStates[index] = 0;
		}

		SetupListView();

		_theListView.Columns[0].Width = 370;
		//_theListView.Columns[1].Width = 270;

		_uiManager = GameManager.Instance.uiManager;
		_pManager = _uiManager._pManager;
	}
	#endregion

	#region Public Methods

	public void AddListViewOTBItems()
	{
		_theListView.Items.Clear();
		_theListView.SelectedIndices.Clear();

		//Debug.Log("OTBs"+_pManager._myOtbs.Count);
		//unsorted...
		//sorted otb's...
		_pManager.SortMyOTBs();

		foreach (OTBCard card in _pManager._myOtbs)
			_theListView.Items.Add(AddItem(card));

		Invoke(nameof(GetItemCustomButtons), 0.1f);
	}

	public void ListViewItemButtonHandler()
	{
		//string targetListView = _theListView.name;

		//Debug.Log($"Selected Index: {_theListView.SelectedIndices[0]}");
		//Debug.Log($"ListView: {targetListView}");

		_uiManager.OnOtbListViewValueChanged(_theListView.SelectedIndices[0]);
	}

	void GetItemCustomButtons()
	{
		CustomButton[] itemButtons = FindObjectsOfType<CustomButton>();

		foreach (CustomButton button in itemButtons)
		{
			button.onClick.AddListener(ListViewItemButtonHandler);
		}
	}
	#endregion

	#region Private Methods

	void SetupListView()
	{
		if (_theListView == null)
		{
			Debug.Log("OTB ListView is null!");
			return;
		}

		_theListView.SuspendLayout();
		AddColumn("             My OTB's");
		//AddColumn("Summary");
		_theListView.ResumeLayout();
	}

	void AddColumn(string title)
	{
		ColumnHeader column = new ColumnHeader();
		column.Text = title;
		_theListView.Columns.Add(column);
	}

	ListViewItem AddItem(OTBCard card)
	{
		//int cNum = card.cardNumber;
		string summary = card.summary;

		string[] data = new string[]
		{
			//cNum.ToString(),
			summary
		};

		ListViewItem item = new ListViewItem(data);
		return item;
	}

	void Test()
	{
		
	}
	#endregion
}
