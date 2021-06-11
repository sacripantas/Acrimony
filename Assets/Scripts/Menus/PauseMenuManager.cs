using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
	public bool isPaused = false;
	[Header("Containers")]
	public GameObject pauseMenuContainer;

	public static PauseMenuManager instance = null;

	private void Awake()//Singleton
	{
		if (instance == null)
		{
			instance = this;
		}
		else if (instance != this)
		{
			Destroy(gameObject);
		}
	}

	// Start is called before the first frame update
	void Start()
    {
        isPaused = false;
    }

    // Update is called once per frame
    void Update()
    {
		PauseGame();
	}

	void PauseGame()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			isPaused = !isPaused;
			GameManager.instance.Pause(isPaused);

			if (isPaused)
			{
				ActivateMenu();
			}
			else
			{
				DeactivateMenu();
			}
		}
	}

	void ActivateMenu()
	{
		Time.timeScale = 0;
		pauseMenuContainer.SetActive(true);
	}

	public void DeactivateMenu()
	{
		pauseMenuContainer.SetActive(false);
		Time.timeScale = 1;	
		isPaused = false;
	}

	public void MainMenu()
	{
		SceneManager.LoadScene("Main Menu");
	}	

	public void ExitGame()
	{
#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
	}

	public void Options()
	{

	}

	public void ConfirmAction()
	{

	}
}
