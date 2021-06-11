using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SaveSlotManager : MonoBehaviour
{
	public GameObject inputField;
	public GameObject backMenu;
	public GameObject text;

	private void Start()
	{
		inputField.SetActive(false);
	}

	public void SetName()
	{
		inputField.SetActive(true);
		backMenu.SetActive(false);
	}

	public void Cancel()
	{
		inputField.SetActive(false);
		backMenu.SetActive(true);
	}

	public void Confirm(string userName)
	{
		userName = text.GetComponent<TMP_Text>().text;
        if (userName.Length > 3) {
            ChoosePlayer.playerName = userName;
            StartCoroutine(CloseWindow());
            MainMenuManager.instance.LoadNewGame();
        } else {
            Debug.Log("Name is not long enough");
        }
    }

    public void SetSlot(int slot) {
        ChoosePlayer.saveSlot = slot;
    }
    
	IEnumerator CloseWindow()
	{
		yield return new WaitForSeconds(0.4f);
		inputField.SetActive(false);
		backMenu.SetActive(true);
	}
}
