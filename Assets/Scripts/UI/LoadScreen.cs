using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;

public class LoadScreen : MonoBehaviour
{
    private AsyncOperation async;
    [SerializeField]
    private Image progressbar;

    [SerializeField]
    private TextMeshProUGUI txtPercent;

    [SerializeField]
    private TextMeshProUGUI txtLevelName;

    [SerializeField]
    private TextMeshProUGUI txtPressAnyKey;

    private int sceneToLoad = -1;

    private bool ready = false;

    // Start is called before the first frame update
    void Start() {
        txtPressAnyKey.enabled = false;
        sceneToLoad = ChoosePlayer.sceneToLoad;

        txtLevelName.text = LevelNames.levelName[sceneToLoad];
        Time.timeScale = 1;
        Input.ResetInputAxes();
        System.GC.Collect();
        Scene currentScene = SceneManager.GetActiveScene();
        async = SceneManager.LoadSceneAsync(sceneToLoad);
        
        async.allowSceneActivation = false;        
    }
    public void Active() {
        ready = true;
    }

    // Update is called once per frame
    void Update() {
        if (Input.anyKey) {
            Active();
        }
        if (progressbar) {
            progressbar.fillAmount = async.progress + 0.1f;
        }
        if (txtPercent) {
            txtPercent.text = ((async.progress + 0.1f) * 100).ToString("000") + "%";
        }
        if (async.progress > 0.89f && SplashScreen.isFinished && ready) {
            async.allowSceneActivation = true;
        }
        if(async.progress > 0.89f) {
            txtPressAnyKey.enabled = true;
        }
    }
}
