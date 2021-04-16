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
	[SerializeField] CanvasGroup _canvasGroup;

	int _spaceNumber = -1;
	bool _panelShown;

	#endregion

	#region Getters


	#endregion

	#region Unity Methods

	void Update() 
	{
		if (Input.GetMouseButton(1) && !_panelShown)
		{
			RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

			if (hit.collider == null) return;

			if (hit.collider.CompareTag("Space Info"))
			{
				_spaceNumber = int.Parse(hit.collider.gameObject.name);

				//Debug.Log(_spaceNumber);
				ShowSpaceInfoPanel();
				_panelShown = true;
			}
		}
		if (Input.GetMouseButtonUp(1) && _spacePanel.activeSelf)
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
		_spacePanel.SetActive(true);

		switch (_spaceNumber)
		{
			case 0:
				_headerText.text = "CHRISTMAS VACATION";
				_spaceText.text = "<color=green>COLLECT</color> $1000 Christmas Bonus.\nCollect years wage of $5000 as you pass.";

				break;
			case 1:
				_headerText.text = "1ST WEEK IN JANUARY";
				_spaceText.text = "<color=red>Pay 10%</color> on Bank Notes On Hand.";
				break;
			case 2:
				_headerText.text = "2ND WEEK IN JANUARY";
				_spaceText.text = "Hibernate.\nDraw O.T.B.";
				break;
			case 3:
				_headerText.text = "3RD WEEK IN JANUARY";
				_spaceText.text = "Bitter cold spell.\n<color=red>PAY $500</color>, if you own cows.";
				break;
			case 4:
				_headerText.text = "4TH WEEK IN JANUARY";
				_spaceText.text = "Beautiful Days!\nDouble all your Hay Harvests this year.";
				break;
			case 5:
				_headerText.text = "1ST WEEK IN FEBRUARY";
				_spaceText.text = "Warm snap, you're in the field two weeks early.\n<color=green>COLLECT $1000</color>.";
				break;
			case 6:
				_headerText.text = "2ND WEEK IN FEBRUARY";
				_spaceText.text = "Stuck in a muddy corral.\nDraw Farmer's Fate.";
				break;
			case 7:
				_headerText.text = "3RD WEEK IN FEBRUARY";
				_spaceText.text = "Ground thaws. Start planting early.\nGo directly to Spring Planting.";
				break;
			case 8:
				_headerText.text = "4TH WEEK IN FEBRUARY";
				_spaceText.text = "Rainy Day.  Draw O.T.B.";
				break;
			case 9:
				_headerText.text = "1ST WEEK IN MARCH";
				_spaceText.text = "Becomes obvious your wheat has winter killed.\n<color=red>PAY $2000</color> to replant.";
				break;
			case 10:
				_headerText.text = "2ND WEEK IN MARCH";
				_spaceText.text = "Start plowing late.  <color=red>PAY $500</color>.";
				break;
			case 11:
				_headerText.text = "3RD WEEK IN MARCH";
				_spaceText.text = "Hurt your back.\nGo back to second week of January.";
				break;
			case 12:
				_headerText.text = "4TH WEEK IN MARCH";
				_spaceText.text = "Frost forces you to heat fruit.\n<color=red>PAY $2000</color>, if you own fruit.";
				break;
			case 13:
				_headerText.text = "1ST WEEK IN APRIL";
				_spaceText.text = "Done plowing. Take a day off.\nDraw O.T.B.";
				break;
			case 14:
				_headerText.text = "SPRING PLANTING";
				_spaceText.text = "Plant corn on time.\nDouble corn yield this year.";
				break;
			case 15:
				_headerText.text = "3RD WEEK IN APRIL";
				_spaceText.text = "More rain.  Field work shutdown.\n<color=red>PAY $500</color>.";
				break;
			case 16:
				_headerText.text = "4TH WEEK IN APRIL";
				_spaceText.text = "Equipment breakdown.\n<color=red>PAY $1000</color>.";
				break;
			case 17:
				_headerText.text = "1ST WEEK IN MAY";
				_spaceText.text = "The whole valley is green!\n<color=green>COLLECT $500</color>.";
				break;
			case 18:
				_headerText.text = "2ND WEEK IN MAY";
				_spaceText.text = "Windstorm makes you replant your corn.\n<color=red>PAY $500</color>.";
				break;
			case 19:
				_headerText.text = "3RD WEEK IN MAY";
				_spaceText.text = "Cut your hay just right.\n<color=green>COLLECT a $1000 bonus</color>.";
				break;
			case 20:
				_headerText.text = "4TH WEEK IN MAY";
				_spaceText.text = "Memorial Day weekend.\nDraw O.T.B.";
				break;
			case 21:
				_headerText.text = "1ST WEEK IN JUNE";
				_spaceText.text = "Rain storm ruins your unbaled hay.\n<color=red>Cut your harvest check in half</color>.";
				break;
			case 22:
				_headerText.text = "2ND WEEK IN JUNE";
				_spaceText.text = "Good growing.\n<color=green>COLLECT a $500 bonus</color>.";
				break;
			case 23:
				_headerText.text = "3RD WEEK IN JUNE";
				_spaceText.text = "Rain splits your cherries.\n<color=red>Cut your harvest check in half</color>.";
				break;
			case 24:
				_headerText.text = "4TH WEEK IN JUNE";
				_spaceText.text = "Dust storm.  Draw Farmer's Fate.";
				break;
			case 25:
				_headerText.text = "INDEPENDENCE DAY BASH";
				_spaceText.text = "WHOOPIE!!";
				break;
			case 26:
				_headerText.text = "1ST WEEK IN JULY";
				_spaceText.text = "Good weather for your second cutting of hay.\n<color=green>Double Hay harvest check</color>.";
				break;
			case 27:
				_headerText.text = "2ND WEEK IN JULY";
				_spaceText.text = "Hot! Wish you were in the mountains.\nDraw O.T.B.";
				break;
			case 28:
				_headerText.text = "3RD WEEK IN JULY";
				_spaceText.text = "It's a cooker! 114 deg in the shade.\nWipe your brow and go to Harvest Moon,\nafter getting Hay check.";
				break;
			case 29:
				_headerText.text = "4TH WEEK IN JULY";
				_spaceText.text = "85 deg, wheat heads filling out beautifully.\n<color=green>Add $50 per acre</color> to your harvest check.";
				break;
			case 30:
				_headerText.text = "1ST WEEK IN AUGUST";
				_spaceText.text = "You're right on time and farming like a Pro.\nGo to the forth week of February.\n<color=green>COLLECT</color> your years wages of <color=green>$5000</color>.";
				break;
			case 31:
				_headerText.text = "2ND WEEK IN AUGUST";
				_spaceText.text = "Storm clouds brewing.\n<color=green>COLLECT $1000</color>, if you have a Harvester.";
				break;
			case 32:
				_headerText.text = "3RD WEEK IN AUGUST";
				_spaceText.text = "Finish wheat harvest with no breakdowns.\n<color=green>COLLECT $500</color>.";
				break;
			case 33:
				_headerText.text = "4TH WEEK IN AUGUST";
				_spaceText.text = "Rain sprouts unharvested wheat.\n<color=red>Cut price $50 per acre</color> on harvest check.";
				break;
			case 34:
				_headerText.text = "1ST WEEK IN SEPTEMBER";
				_spaceText.text = "Tractor owners: bale Hay, then go to fourth week of November.\nCOLLECT your $1000 there,\nthen harvest your Fruit.";
				break;
			case 35:
				_headerText.text = "2ND WEEK IN SEPTEMBER";
				_spaceText.text = "Sunny skies at the County Fair.\nDraw O.T.B.";
				break;
			case 36:
				_headerText.text = "HARVEST MOON";
				_spaceText.text = "Smiles on you. <color=green>COLLECT $500</color>.";
				break;
			case 37:
				_headerText.text = "3RD WEEK IN SEPTEMBER";
				_spaceText.text = "Market collapses.\n<color=red>Cut livestock check in half</color>.";
				break;
			case 38:
				_headerText.text = "4TH WEEK IN SEPTEMBER";
				_spaceText.text = "Codling Moth damage to apples lowers fruit grade.\n<color=red>PAY $2000</color>, if you own fruit.";
				break;
			case 39:
				_headerText.text = "1ST WEEK IN OCTOBER";
				_spaceText.text = "Indian Summer.  <color=green>COLLECT $500</color>.";
				break;
			case 40: //4TH HAY CUT
				_headerText.text = "2ND WEEK IN OCTOBER";
				_spaceText.text = "Lucked-out and got a 4th Hay Cut.  <color=green>COLLECT $500</color>.";
				break;
			case 41: //START SPUD HARVEST
				_headerText.text = "3RD WEEK IN OCTOBER";
				_spaceText.text = "Good Pheasant Hunting.\nDraw Farmer's Fate.";
				break;
			case 42:
				_headerText.text = "4TH WEEK IN OCTOBER";
				_spaceText.text = "Park your baler for the winter.\nDraw O.T.B.";
				break;
			case 43: //END OF SPUD HARVEST
				_headerText.text = "1ST WEEK IN NOVEMBER";
				_spaceText.text = "Annual Deer Hunt.  Draw Farmer's Fate.";
				break;
			case 44: //START APPLE HARVEST
				_headerText.text = "2ND WEEK IN NOVEMBER";
				_spaceText.text = "Irrigation Season over.  Draw O.T.B.";
				break;
			case 45:
				_headerText.text = "3RD WEEK IN NOVEMBER";
				_spaceText.text = "Good weather, harvest winding up.\n<color=green>COLLECT $500</color>.";
				break;
			case 46: //END APPLE HARVEST
				_headerText.text = "4TH WEEK IN NOVEMBER";
				_spaceText.text = "Good weather holding.  <color=green>COLLECT $1000</color>.";
				break;
			case 47: //START CORN HARVEST
				_headerText.text = "1ST WEEK IN DECEMBER";
				_spaceText.text = "Early freeze kills fruit buds.\n<color=red>PAY $1000</color>, if you have Fruit.";
				break;
			case 48:
				_headerText.text = "2ND WEEK IN DECEMBER";
				_spaceText.text = "Cold and dry, perfect Field Corn Harvesting.\n<color=green>COLLECT $500</color>.";
				break;
			case 49: //END CORN HARVEST
				_headerText.text = "3RD WEEK IN DECEMBER";
				_spaceText.text = "First Snow.  Draw Farmer's Fate.";
				break;
		}

		_canvasGroup.DOFade(1, 0.5f);

	}

	void HideSpaceInfoPanel()
	{
		//Debug.Log("In HideSIP");
		_canvasGroup.DOFade(0, 0.5f);
		_panelShown = false;
	}
	#endregion
}
