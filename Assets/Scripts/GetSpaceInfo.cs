using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetSpaceInfo : MonoBehaviour
{
	#region Fields & Properties

	[SerializeField] Text _headerText, _spaceText;
	[SerializeField] GameObject _spacePanel;
	[SerializeField] DOTweenAnimation _anim;

	PlayerMove _pMove;

	int _spaceNumber = -1;
	
	#endregion

	#region Getters


	#endregion

	#region Unity Methods

	void Start() 
	{
		_pMove = GameManager.Instance.myFarmer.GetComponent<PlayerMove>();
	}
	
	void Update() 
	{
		if (Input.GetMouseButton(1))
		{
			RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

			if (hit.collider == null) return;

			if (hit.collider.CompareTag("Space Info"))
			{
				_spaceNumber = int.Parse(hit.collider.gameObject.name);

				Debug.Log(_spaceNumber);
				ShowSpaceInfoPanel();
			}
		}
		if (Input.GetMouseButtonUp(1) && _spacePanel.activeInHierarchy)
		{
			HideSpaceInfoPanel();
		}
	}
	#endregion

	#region Public Methods


	#endregion

	#region Private Methods

	void ShowSpaceInfoPanel()
	{
		switch (_spaceNumber)
		{
			case 0:
				_headerText.text = "CHRISTMAS VACATION";
				_spaceText.text = "<color=green>COLLECT</color> $1000 Christmas Bonus.\nCollect years wage of $5000 as you pass.";
				break;
		}

		_spacePanel.SetActive(true);
		_anim.DOPlay();
	}

	void HideSpaceInfoPanel()
	{
		//_anim.DORewind();
		_anim.DOPlayBackwards();
		_spacePanel.SetActive(false);
	}
	#endregion
}
