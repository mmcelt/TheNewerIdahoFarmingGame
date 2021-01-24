using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;
using TMPro;

[SerializeField]
public class MyDiceRoll : MonoBehaviour
{
	enum RollStarte
	{
		None = 0,
		Roll,
		Stop,
	};

	[SerializeField] int m_pip = -1;
	[SerializeField] Text buttonTxt;
	[SerializeField] Vector2 rollVec = Vector2.one * 200f;
	[SerializeField] AudioClip _dieRoll;
	[SerializeField] float stopRollAmount = 10f;
	[SerializeField] float startMoveAmount = 0.25f;

	public int Pip { get { return m_pip; } }

	public bool isOtherRoll;
	public bool isHarvestRoll;
	public bool isTetonDamRoll;         //used as a roll type designator
	public bool tetonDamRollComplete;   //used to coroutine wait point

	RollStarte state;
	Animator _anim;
	Rigidbody2D _rb;
	AudioSource _aSource;
	Vector3 _defPos;
	bool _toRoll;

	SpriteRenderer _sprite;
	PlayerMove _pMove;
	UIManager _uiManager;
	PlayerManager _pManager;
	HarvestManager _hManager;

	Button _harvestOkButton;
	Text _harvestOkButtonText;
	Button _harvestRollButton;
	TextMeshProUGUI _tetonDamMessageText;

	void Start()
	{
		_anim = GetComponent<Animator>();
		_aSource = GetComponent<AudioSource>();
		_rb = GetComponent<Rigidbody2D>();

		_uiManager = GameManager.Instance.uiManager;
		_harvestRollButton = _uiManager._harvestRollButton;
		_harvestOkButton = _uiManager._harvestOk1Button;
		buttonTxt = _uiManager._rollButton.GetComponentInChildren<Text>();
		_harvestRollButton = _uiManager._harvestRollButton;
		_harvestOkButtonText = _uiManager._harvestOk1Button.GetComponentInChildren<Text>();
		_tetonDamMessageText = _uiManager._tetonMessageText;

		_sprite = GetComponent<SpriteRenderer>();
		_pMove = GameManager.Instance.myFarmer.GetComponent<PlayerMove>();
		_pManager = GameManager.Instance.myFarmer.GetComponent<PlayerManager>();
		_hManager = GameManager.Instance.hManager;

		_rb.isKinematic = true;
		_defPos = transform.position;
		_toRoll = false;
		StartCoroutine(diceRollCo());
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		//Debug.Log(rb.velocity.sqrMagnitude);
		if (_aSource != null)
			_aSource.Play();

		if (_rb != null)
		{
			if (_rb.velocity.sqrMagnitude < stopRollAmount)  //10
			{
				state = RollStarte.Stop;
			}
			if (_rb.velocity.sqrMagnitude < startMoveAmount) //0.25f/4.5f
			{
				StartCoroutine(StartMove());
			}
		}
	}

	IEnumerator StartMove()
	{
		yield return new WaitForSeconds(1.0f);
		if (!isOtherRoll)
		{
			//Debug.Log("MOVE! " + Pip);
			_pMove.InitMove(Pip);
		}
		yield return new WaitForSeconds(0.95f);
		_sprite.enabled = false;
	}

	IEnumerator diceRollCo()
	{
		tetonDamRollComplete = false;

		while (true)
		{
			if (!isOtherRoll)
				buttonTxt.text = "ROLL";
			if (isHarvestRoll)
			{
				_harvestOkButtonText.text = "";
				_harvestOkButton.interactable = false;
			}

			_rb.isKinematic = true;
			transform.position = _defPos;
			state = RollStarte.Stop;
			m_pip = -1;
			while (!_toRoll)
			{
				yield return null;
			}
			state = RollStarte.Roll;
			if (state == RollStarte.Roll)
			{
				_sprite.color = ChoosePlayerDieTint();
				_sprite.enabled = true;
			}

			_rb.isKinematic = false;
			_rb.velocity = Vector2.zero;
			_rb.AddForce(rollVec);

			Roll();

			while (state == RollStarte.Roll)
			{
				yield return null;
			}

			SetPip(Random.Range(0, 6) + 1);

			yield return new WaitForSeconds(0.75f);

			if (!isOtherRoll)
				buttonTxt.text = Pip.ToString();
			else if (isHarvestRoll)
			{
				_harvestOkButtonText.text = Pip.ToString();
				_harvestOkButton.interactable = true;

				//send harvest roll msg to others
				_hManager._dieRoll = Pip;
				SendHarvestDieRollMessage();  //CHANGE TO JUST SENDING DIE ROLL TO OTHERS MSG
				_hManager._rollButtonPressed = true;
				yield return new WaitForSeconds(1.2f);
				_harvestOkButtonText.text = "OK";
			}
			else if (isTetonDamRoll)
			{
				tetonDamRollComplete = true;
			}

			_toRoll = false;
			while (!_toRoll)
			{
				yield return null;
			}
			buttonTxt.text = "ROLL";
			_harvestOkButtonText.text = "OK";
		}
	}

	public void OnRollButton()
	{
		_toRoll = true;
		_aSource.PlayOneShot(_dieRoll);
	}

	/// <summary>
	/// Start roll animation
	/// </summary>
	public void Roll()
	{
		_anim.Play("roll");
	}

	/// <summary>
	/// Set pip and start pip animation
	/// </summary>
	/// <param name="_pip">pip of dice</param>
	public void SetPip(int _pip)
	{
		m_pip = Mathf.Clamp(_pip, 1, 7);
		_anim.Play("to" + m_pip.ToString());
	}

	Color ChoosePlayerDieTint()
	{
		Color dieTint = Color.white;

		switch (GameManager.Instance.myFarmerName)
		{
			case IFG.Becky:
				dieTint = Color.white;
				break;

			case IFG.Jerry:
				dieTint = new Color(0.6923f, 0.5518f, 0.7558f);
				break;

			case IFG.Kay:
				dieTint = new Color(0.8679f, 0.4134f, 0.4345f);
				break;

			case IFG.Mike:
				dieTint = new Color(0.8773f, 0.8773f, 0.4345f);
				break;

			case IFG.Ric:
				dieTint = new Color(0.5188f, 0.5188f, 0.5188f);
				break;

			case IFG.Ron:
				dieTint = new Color(0.5047f, 0.5047f, 1);
				break;
		}

		return dieTint;
	}

	void SendHarvestDieRollMessage()
	{
		string commodity = GetCommodity();

		//data - nickname, farmerName, die, commodity
		object[] sndData = new object[] { PhotonNetwork.LocalPlayer.NickName, GameManager.Instance.myFarmerName, Pip, commodity };
		//event options
		RaiseEventOptions eventOptions = new RaiseEventOptions()
		{
			Receivers = ReceiverGroup.Others,
			CachingOption = EventCaching.DoNotCache
		};
		//send options
		SendOptions sendOptions = new SendOptions() { Reliability = true };
		//fire the event to the UIManagers
		PhotonNetwork.RaiseEvent((byte)RaiseEventCodes.Harvest_Roll_Message_Event_Code, sndData, eventOptions, sendOptions);
	}

	string GetCommodity()
	{
		if (_pMove._currentSpace >= 19 && _pMove._currentSpace <= 22)
			return "Hay";
		else if (_pMove._currentSpace >= 23 && _pMove._currentSpace <= 25)
			return "Cherries";
		else if (_pMove._currentSpace >= 26 && _pMove._currentSpace <= 28)
			return "Hay";
		else if (_pMove._currentSpace >= 29 && _pMove._currentSpace <= 33)
			return "Wheat";
		else if (_pMove._currentSpace >= 34 && _pMove._currentSpace <= 35)
			return "Hay";
		else if (_pMove._currentSpace >= 36 && _pMove._currentSpace <= 39)
			return "Livestock";
		else if (_pMove._currentSpace == 40)
			return "Hay";
		else if (_pMove._currentSpace >= 41 && _pMove._currentSpace <= 43)
			return "Spuds";
		else if (_pMove._currentSpace >= 44 && _pMove._currentSpace <= 46)
			return "Apples";
		else if (_pMove._currentSpace >= 47 && _pMove._currentSpace <= 49)
			return "Corn";
		else
			return "Crap";
	}
}
