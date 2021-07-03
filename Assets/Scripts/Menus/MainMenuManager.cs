using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.IO;
using TMPro;

public class MainMenuManager : MonoBehaviour
{
    public static MainMenuManager instance;
	public EventSystem eventSystem;
	public string currentMenu;
	public string nextMenu;

	[Header("Animators")]
	public Animator mainAnimator;
	public Animator optionsAnimator;
	public Animator confirmAnimator;
	public Animator audioAnimator;
	public Animator videoAnimator;
	public Animator saveAnimator;
	public Animator loadAnimator;


	[Header("Buttons")]
	public GameObject startButton;
	public GameObject loadButton;
	public GameObject optionsButton;
	public GameObject exitButton;

	[Header("ReturnButtons")]
	public GameObject returnOptions;
	public GameObject returnAudio;
	public GameObject returnVideo;
	public GameObject returnSave;
	public GameObject returnLoad;

	[Header("Containers")]
	public GameObject mainMenu;
	public GameObject optionsMenu;
	public GameObject confirmMenu;
	public GameObject audioMenu;
	public GameObject videoMenu;
	public GameObject saveSlotMenu;
	public GameObject loadSlotMenu;
	public GameObject expanded;



	[Header("Save Slots")]
    [SerializeField]
    [Tooltip("All json files on the folder /UserSaves")]
    private List<string> currentSaves;

    [SerializeField]
    [Tooltip("Buttons of Save Slots - Load Game Menu")]
    private List<GameObject> loadGameSlots;

    [SerializeField]
    [Tooltip("Buttons of Save Slots - New Game Menu")]
    private List<GameObject> newGameSlots;

    [SerializeField]
    [Tooltip("Color for disabled buttons")]
    private Color disabledColor;

    [SerializeField]
    [Tooltip("Color for enabled buttons")]
    private Color enabledColor;


    [Header("Save File")]
    private SaveStruct save = new SaveStruct();
    // Start is called before the first frame update
    void Awake() {
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Destroy(gameObject);
        }
    }

    void Start()
    {		
		Cursor.visible = true;
		mainMenu.SetActive(true);
		optionsMenu.SetActive(false);
		confirmMenu.SetActive(false);
		audioMenu.SetActive(false);
		videoMenu.SetActive(false);
		saveSlotMenu.SetActive(false);
		loadSlotMenu.SetActive(false);
        this.LoadSaveFiles();
    }

    //Creates a new player file
    public void SetNewPlayer() {//Should use default values
        save.ammo = 0;
        save.equipped = "";
        save.hp = 100;
        save.inventory = "";
        save.mana = 100;
        save.money = 0;
        save.progression = "000000000000"; //All locked
        save.scene  = 1;
        save.position = 0;
        foreach (string str in ChoosePlayer.minimaps)
            save.miniMapRooms += "" + str + "#"; //needs to serialize the struct
        save.timeElapsed = 0f;
    }

    //Save a new file for New Game
    public void SaveNewPlayer() {
        //checks if the path exists before saving the file. If not, create;
        if (!System.IO.Directory.Exists(Application.persistentDataPath + "/UserSave"))
            System.IO.Directory.CreateDirectory(Application.persistentDataPath + "/UserSave");
        string data = JsonUtility.ToJson(this.save);
        try {
            File.WriteAllText(Application.persistentDataPath + "/UserSave/" + ChoosePlayer.saveSlot + "." + ChoosePlayer.playerName + ".json", data);
        }
        catch (System.Exception e) {
            Debug.Log("Could not save file with exception: " + e.ToString());
        }
    }

    public void ReadSavedFile() {
        try {
            ChoosePlayer.playerName = loadGameSlots[ChoosePlayer.saveSlot - 1].GetComponentInChildren<TextMeshProUGUI>().text;
            string file = File.ReadAllText(Application.persistentDataPath + "/UserSave/" + ChoosePlayer.saveSlot + "." + ChoosePlayer.playerName + ".json");
            save = JsonUtility.FromJson<SaveStruct>(file);
        }
        catch (System.Exception e){
            Debug.Log(e.ToString());
        }
    }

    public void LoadNewGame() {
        //Set a new player with default values
        SetNewPlayer();
        //Write the new player on the disc
        SaveNewPlayer();
        //starts a new game on first level
        ChoosePlayer.sceneToLoad = 1;
        SceneManager.LoadScene("Load");
    }

    public void LoadSavedGame() {
        //Reads the player file
        ReadSavedFile();
        //Loads the saved scene
        ChoosePlayer.sceneToLoad = save.scene;
        SceneManager.LoadScene("Load");
    }

    //expands selected slot
	public void OpenSlot(int index) {
        ReadSavedFile();
        ExpandedSlotManager.instance.ShowExpanded(ChoosePlayer.playerName,save.scene, save.timeElapsed);
        loadAnimator.Play("FadeOutLoad");
		expanded.SetActive(true);	
	}

	public void StartNewGame(string name)
	{		
		currentMenu = "Start";
		nextMenu = "Save";
        this.ShowSaveFiles(false);
		StartCoroutine(FadeOut(currentMenu));        
    }

	public void LoadGame()
	{
		currentMenu = "Main";
		nextMenu = "Load";
        this.ShowSaveFiles(true);
		StartCoroutine(FadeOut(currentMenu));
	}

	public void OpenOptions()
	{
		currentMenu = "Main";
		nextMenu = "Options";
		StartCoroutine(FadeOut(currentMenu));
	}

	public void ReturnOptions()
	{
		currentMenu = "Options";
		nextMenu = "Main";
		StartCoroutine(FadeOut(currentMenu));
	}

	public void OpenAudio()
	{
		currentMenu = "Options";
		nextMenu = "Audio";
		StartCoroutine(FadeOut(currentMenu));
	}

	public void ReturnAudio()
	{
		currentMenu = "Audio";
		nextMenu = "Options";
		StartCoroutine(FadeOut(currentMenu));
	}

	public void OpenVideo()
	{
		currentMenu = "Options";
		nextMenu = "Video";
		StartCoroutine(FadeOut(currentMenu));
	}

	public void ReturnVideo()
	{
		currentMenu = "Video";
		nextMenu = "Options";
		StartCoroutine(FadeOut(currentMenu));
	}

	public void ReturnSaveSlot()
	{
		currentMenu = "Save";
		nextMenu = "Main";
		StartCoroutine(FadeOut(currentMenu));
	}

	public void ReturnLoadSlot()
	{
		currentMenu = "Load";
		nextMenu = "Main";
		StartCoroutine(FadeOut(currentMenu));
	}

	public void ConfirmAction()
	{		
		nextMenu = "Exit";
		StartCoroutine(FadeIn(nextMenu));
	}

	public void CancelAction()
	{
		currentMenu = "Exit";
		StartCoroutine(FadeOut(currentMenu));
	}

    //List all json files in the folder UserSave
    public void LoadSaveFiles() {
        currentSaves.Clear();
        for(int i = 0; i < loadGameSlots.Count; i++) {
            loadGameSlots[i].GetComponentInChildren<TextMeshProUGUI>().SetText("Empty Save " + (i+1));
            newGameSlots[i].GetComponentInChildren<TextMeshProUGUI>().SetText("Empty Save " + (i+1));
        }
        foreach (string file in Directory.EnumerateFiles(Application.persistentDataPath + "/UserSave", "*.json")) {
            currentSaves.Add(file.Split('\\')[1]);
        }
    }
    //Shows all files in the form #.UserName
    public void ShowSaveFiles(bool isLoad) {
        if (isLoad) this.EnableAllButtons(loadGameSlots, false); //by default all buttons on "Load Game" are disabled
        else this.EnableAllButtons(newGameSlots, true); //by default all buttons on "New Game" menu are enabled 
        string[] tempStr;
        foreach(string s in currentSaves) {
            tempStr = s.Split('.');
            if(tempStr.Length >= 3) {//the file name must be in the format #.UserName.json
                int index = System.Convert.ToInt32(tempStr[0]);
                if (isLoad) {//if its loading, only the buttons with active files can be loaded
                    loadGameSlots[index - 1].GetComponent<Button>().interactable = true;
                    loadGameSlots[index - 1].GetComponentInChildren<TextMeshProUGUI>().SetText(tempStr[1]);
                    loadGameSlots[index - 1].GetComponent<Image>().color = enabledColor;
                } else {//if its new game, cant click on used slots
                    newGameSlots[index - 1].GetComponent<Button>().interactable = false;
                    newGameSlots[index - 1].GetComponentInChildren<TextMeshProUGUI>().SetText(tempStr[1]);
                    newGameSlots[index - 1].GetComponent<Image>().color = disabledColor;
                }
            } else {
                Debug.Log("Wrong type of file name");
            }
        }
    }

    private void EnableAllButtons(List<GameObject> buttons, bool flag) {
        foreach(GameObject b in buttons) {
            b.GetComponent<Button>().interactable = flag;
            if (flag) b.GetComponent<Image>().color = enabledColor;
            else b.GetComponent<Image>().color = disabledColor;
        }
    }

	public void ExitGame()
	{
#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
	}

	//===========================================================Animations=======================================================

	IEnumerator FadeOut(string menu)
	{
		switch (menu)
		{
			case "Start":
				mainAnimator.Play("FadeOutMain");
				yield return new WaitForSeconds(1f);
				saveSlotMenu.SetActive(true);
				mainMenu.SetActive(false);
				yield return new WaitForSeconds(0.1f);
				StartCoroutine(FadeIn(nextMenu));
				//SceneManager.LoadScene(localName);
				break;
			case "Main":
				mainAnimator.Play("FadeOutMain");
				yield return new WaitForSeconds(1f);
				mainMenu.SetActive(false);				
				eventSystem.SetSelectedGameObject(returnOptions);
				yield return new WaitForSeconds(0.1f);
				StartCoroutine(FadeIn(nextMenu));
				break;
			case "Options":
				optionsAnimator.Play("FadeOutOptions");
				yield return new WaitForSeconds(1f);
				optionsMenu.SetActive(false);
				mainMenu.SetActive(true);
				yield return new WaitForSeconds(0.1f);
				StartCoroutine(FadeIn(nextMenu));
				break;
			case "Audio":
				audioAnimator.Play("FadeOutAudio");
				yield return new WaitForSeconds(1f);
				audioMenu.SetActive(false);
				optionsMenu.SetActive(true);
				yield return new WaitForSeconds(0.1f);
				StartCoroutine(FadeIn(nextMenu));
				break;
			case "Video":
				videoAnimator.Play("FadeOutVideo");
				yield return new WaitForSeconds(1f);
				videoMenu.SetActive(false);
				optionsMenu.SetActive(true);
				yield return new WaitForSeconds(0.1f);
				StartCoroutine(FadeIn(nextMenu));
				break;
			case "Save":
				saveAnimator.Play("FadeOutSave");
				yield return new WaitForSeconds(1f);
				saveSlotMenu.SetActive(false);
				mainMenu.SetActive(true);
				yield return new WaitForSeconds(0.1f);
				StartCoroutine(FadeIn(nextMenu));
				break;
			case "Load":
				loadAnimator.Play("FadeOutLoad");
				yield return new WaitForSeconds(1f);
				loadSlotMenu.SetActive(false);
				mainMenu.SetActive(true);
				yield return new WaitForSeconds(0.1f);
				StartCoroutine(FadeIn(nextMenu));
				break;
			case "Exit":
				confirmAnimator.Play("FadeOutConfirm");
				mainMenu.SetActive(true);
				mainAnimator.Play("FadeInMain");
				yield return new WaitForSeconds(1f);
				confirmMenu.SetActive(false);
				break;
		}
		
	}

	IEnumerator FadeIn(string menuNext)
	{
		switch (menuNext)
		{
			case "Main":
				optionsMenu.SetActive(false);
				mainMenu.SetActive(true);
				mainAnimator.Play("FadeInMain");
				yield return new WaitForSeconds(1f);
				break;
			case "Options":
				optionsMenu.SetActive(true);
				mainMenu.SetActive(false);
				optionsAnimator.Play("FadeInOptions");
				yield return new WaitForSeconds(1f);
				break;
			case "Audio":
				audioMenu.SetActive(true);
				optionsMenu.SetActive(false);
				audioAnimator.Play("FadeInAudio");
				yield return new WaitForSeconds(1f);
				break;
			case "Video":
				videoMenu.SetActive(true);
				optionsMenu.SetActive(false);
				videoAnimator.Play("FadeInVideo");
				yield return new WaitForSeconds(1f);
				break;
			case "Save":
				saveSlotMenu.SetActive(true);
				mainMenu.SetActive(false);
				saveAnimator.Play("FadeInSave");
				yield return new WaitForSeconds(1f);
				break;
			case "Load":
				loadSlotMenu.SetActive(true);
				mainMenu.SetActive(false);
				loadAnimator.Play("FadeInLoad");
				yield return new WaitForSeconds(1f);
				break;
			case "Exit":
				confirmMenu.SetActive(true);
				mainAnimator.Play("FadeOutMain");
				confirmAnimator.Play("FadeInConfirm");
				yield return new WaitForSeconds(1f);
				mainMenu.SetActive(false);
				break;
		}
	}
}
