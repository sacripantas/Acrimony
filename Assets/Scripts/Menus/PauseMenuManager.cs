using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuManager : MonoBehaviour
{
	public bool isPaused = false;
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
		if (Input.GetKeyDown(KeyCode.Escape))
		{

            isPaused = !isPaused;
            GameManager.instance.Pause(isPaused);
		}


		if (isPaused)
		{
			ActivateMenu();
		}
		else
		{
			DeactivateMenu();
		}
	}

	void ActivateMenu()
	{
		Time.timeScale = 0;
		AudioListener.pause = true;
		pauseMenuContainer.SetActive(true);
	}

	public void DeactivateMenu()
	{
		AudioListener.pause = false;
		pauseMenuContainer.SetActive(false);
		Time.timeScale = 1;	
		isPaused = false;
	}
}
