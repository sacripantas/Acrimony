using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Tooltip("Insert all spawners in the scene")]
    [SerializeField]
    private List<Respawner> respawners;

    [Tooltip("Player Prefab")]
    [SerializeField]
    private GameObject player;

    public bool isPaused;

    //singleton
    void Awake() {
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Destroy(gameObject);
        }

		AstarPath.active.Scan();
	}
    // Start is called before the first frame update
    void Start()
    {
        SpawnHandler();//spawns the  player upon start
        if (player == null) {
            //player = Instantiate<Player>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //respawns player in active respawner
    void SpawnHandler() {
		if (respawners.Count == 0) return;
        foreach(Respawner r in respawners) {
            if (r.isActive) {
                player.transform.localPosition = r.Position;
				return;
            }
        }
        player.transform.localPosition = respawners[0].Position; //if no spawner is active, respawns on the first spawner of the scene
		
    }

    // to be called upon death
    public void DeathHandler() {
        //add death screen, change song, display menu etc.
        this.SpawnHandler();
        Debug.Log("Died");
    }

    public void Pause(bool flag) {
        if (flag) Time.timeScale = 0f;
        else Time.timeScale = 1f;
        isPaused = flag;
    }
}
