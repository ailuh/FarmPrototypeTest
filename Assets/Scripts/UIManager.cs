using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
	public static UIManager Instance { get; private set; }

	public Button GardenBedButton;
	public Button CucumberButton;
	public Button TomatoButton;
	public Button WheatButton;

	public Text CucumberText;
	public Text TomatoText;
	public Text WheatText;

	public int CucumberCounter;
	public int TomatoCounter;
	public int WheatCounter;

	void Start()
	{
		if (Instance == null)
		{ 
			Instance = this; 
		}
		else if (Instance == this)
		{ 
			Destroy(gameObject);
		}

		Button btnCucumber = CucumberButton.GetComponent<Button>();
		btnCucumber.onClick.AddListener(CreateCucumber);

		Button btnTomato = TomatoButton.GetComponent<Button>();
		btnTomato.onClick.AddListener(CreateTomato);

		Button btnWheat = WheatButton.GetComponent<Button>();
		btnWheat.onClick.AddListener(CreateWheat);
		
		Button btnGarden = GardenBedButton.GetComponent<Button>();
		btnGarden.onClick.AddListener(CreateGarden);
	}

	void CreateCucumber()
	{
		StateManager.CurrentCursorState = State.Cucumber;
		Debug.LogFormat("State is: {0}", StateManager.CurrentCursorState);
	}

	void CreateTomato()
	{
		StateManager.CurrentCursorState = State.Tomato;
		Debug.LogFormat("State is: {0}", StateManager.CurrentCursorState);
	}

	void CreateWheat()
	{
		StateManager.CurrentCursorState = State.Wheat;
		Debug.LogFormat("State is: {0}", StateManager.CurrentCursorState);
	}

	void CreateGarden()
	{
		if (GridController.NumberOfGardens <= 36)
        {
			StateManager.CurrentCursorState = State.Garden;
			Debug.LogFormat("State is: {0}", StateManager.CurrentCursorState);
		}
	}

	public void UpdateCounts()
    {
		CucumberText.text = CucumberCounter.ToString();
		TomatoText.text = TomatoCounter.ToString();
		WheatText.text = WheatCounter.ToString();
	}

}
