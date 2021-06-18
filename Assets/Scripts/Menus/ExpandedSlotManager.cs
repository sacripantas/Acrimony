using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;

public class ExpandedSlotManager : MonoBehaviour
{
	private static MainMenuManager main;
    public static ExpandedSlotManager instance;

	[Header("Containers")]
	public GameObject expanded;
	public GameObject confirmContainer;
	public GameObject messageDelete;

	[Header("Text Fields")]
	public TextMeshProUGUI nameText;
	public TextMeshProUGUI locationText;
	public TextMeshProUGUI timeText;
	public TextMeshProUGUI deleteText;
    public TextMeshProUGUI confirmDeleting;

	private string levelName;
    void Awake() {
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Destroy(gameObject);
        }
    }

    private void Start()
	{
		main = MainMenuManager.instance;	
	}

	private void Update()
	{
        
    }
	

    public void ShowExpanded(string name, int location, float playtime) {
        
        nameText.text = name;
        locationText.text = LevelNames.levelName[location];
        timeText.text = FormatTime(playtime);
        confirmDeleting.text = "Deleting: " + name;
    }

    //format the elapsed time as: HH:mm:ss
    public string FormatTime(float time) {
        string hours = System.Math.Truncate((time / 3600.0f)).ToString("00"); //from seconds to hours
        string min = System.Math.Truncate(((time % 3600.0f) / 60.0f)).ToString("00"); //convert the rest to minutes
        string sec = System.Math.Truncate(((time % 3600.0f) % 60.0f)).ToString("00"); //convert the rest to seconds

        return hours + "H" + min+ "m" + sec + "s";
    }

	public void Cancel()
	{
		main.loadAnimator.Play("FadeInLoad");
		expanded.SetActive(false);
	}

	public void ConfirmDelete() //This opens the menu
	{
		expanded.SetActive(false);
		confirmContainer.SetActive(true);
	}

	public void CancelConfirm()
	{
		expanded.SetActive(true);
		confirmContainer.SetActive(false);
	}

	public void Delete() //This happens when the player clicks confirm to delete. Put the deletion logic here.
	{
        string msg = "", filePath = Application.persistentDataPath + "/UserSave/" + ChoosePlayer.saveSlot + "." + ChoosePlayer.playerName + ".json";        
        if (File.Exists(filePath)) {
            Debug.Log("Deleting save: " + ChoosePlayer.playerName);
            try {
                File.Delete(filePath);
                msg = "Successfully Deleted";
            }
            catch {
                msg = "Error: could not delete file";
            }
        } else {
            msg = "File " + filePath + " doesn't exist";
        }
        //Show message
        Debug.Log(msg);
        deleteText.text = msg;
        messageDelete.SetActive(true);
        main.LoadSaveFiles();
    }

	public void DeleteMessage()
	{
		expanded.SetActive(false);
		main.loadSlotMenu.SetActive(false);
		main.mainMenu.SetActive(true);
		main.mainAnimator.Play("FadeInMain");
		confirmContainer.SetActive(false);
		messageDelete.SetActive(false);
	}
}
