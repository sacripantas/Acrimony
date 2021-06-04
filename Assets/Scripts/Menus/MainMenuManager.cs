using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MainMenuManager : MonoBehaviour
{
	public EventSystem eventSystem;
	public string currentMenu;
	public string nextMenu;
	private string localName;

	[Header("Animators")]
	public Animator mainAnimator;
	public Animator optionsAnimator;
	public Animator confirmAnimator;
	public Animator audioAnimator;
	public Animator videoAnimator;

	[Header("Buttons")]
	public GameObject startButton;
	public GameObject loadButton;
	public GameObject optionsButton;
	public GameObject exitButton;

	[Header("ReturnButtons")]
	public GameObject returnOptions;
	public GameObject returnAudio;
	public GameObject returnVideo;

	[Header("Containers")]
	public GameObject mainMenu;
	public GameObject optionsMenu;
	public GameObject confirmMenu;
	public GameObject audioMenu;
	public GameObject videoMenu;

	// Start is called before the first frame update
	void Start()
    {		
		Cursor.visible = true;		
		optionsMenu.SetActive(false);
		confirmMenu.SetActive(false);
		audioMenu.SetActive(false);
		videoMenu.SetActive(false);
    }

	public void StartNewGame(string name)
	{		
		currentMenu = "Start";
		localName = name;
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
				yield return new WaitForSeconds(1.5f);
				SceneManager.LoadScene(localName);
				break;
			case "Main":
				mainAnimator.Play("FadeOutMain");
				yield return new WaitForSeconds(1f);
				optionsMenu.SetActive(true);
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
