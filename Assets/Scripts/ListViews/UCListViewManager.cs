using Endgame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UCListViewManager : MonoBehaviour
{
	#region Fields & Properties

	[SerializeField] ListView _theListView;
	//[SerializeField] UIManager _uiManager;
	public UncleChester _uCheester;

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

	public PlayerManager _pManager;

	#endregion

	#region Getters


	#endregion

	#region MonoBehaviour Methods

	void Awake()
	{
		Debug.Log("UC LISTVIEW IS AWAKE");
		//_pManager = _uiManager._pManager;

		// Initialize an array with some example column widths
		// that will be toggled between by clicking on the column header.
		// (-1 in Windows Forms means size to the longest item, and
		// -2 means size to the column header.)
		_columnWidths[0] = -2;
		//_columnWidths[1] = -1;
		//columnWidths[2] = -2;
		//_uCheester = GetComponentInParent<UncleChester>();
	}

	void Start()
	{
		for (int index = 0; index < columnCount; index++)
		{
			_columnWidthStates[index] = 0;
		}

		SetupListView();

		_theListView.Columns[0].Width = 480;

		//_uiManager = GameManager.Instance.uiManager;
		//_pManager = _uiManager._pManager;	//set when instantiated in PlayerManager

	}
	#endregion

	#region Public Methods

	public void AddListViewDownPaymentItems()
	{
		_theListView.Items.Clear();
		_theListView.SelectedIndices.Clear();

		int counter = 0;
		//Debug.Log("OTBs" + _pManager._myOtbs.Count);
		foreach (var item in _uCheester._downpayments)
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

		_uCheester.OnDownpaymentChanged(_theListView.SelectedIndices[0]);
	}

	void GetItemCustomButtons()
	{
		UncleCheesterCustomButton[] itemButtons = FindObjectsOfType<UncleCheesterCustomButton>();

		foreach (UncleCheesterCustomButton button in itemButtons)
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
			Debug.Log("UC ListView is null!");
			return;
		}

		_theListView.SuspendLayout();
		AddColumn("   Select Down Payment");
		_theListView.ResumeLayout();
	}

	void AddColumn(string title)
	{
		ColumnHeader column = new ColumnHeader();
		column.Text = title;
		_theListView.Columns.Add(column);
	}

	ListViewItem AddItem(string newItem)
	{
		_tester++;
		//string down = newItem;

		//string[] data = new string[]
		//{
		//	down
		//};
		Debug.Log("Tester: " +_tester + ":" + newItem);

		ListViewItem item = new ListViewItem(newItem);
		return item;
	}

	void Test()
	{

	}
	#endregion

}
