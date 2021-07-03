using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private static PlayerManager playerManager;

    [Tooltip("Insert all spawners in the scene")]
    [SerializeField]
    private List<Respawner> respawners;

    [Tooltip("Player Prefab")]
    [SerializeField]
    private GameObject player;

    public bool isPaused;

    private SaveHelper saveState;
    private LoadPrefs loadSave;


    public SaveHelper SaveState { get => saveState; }
    public LoadPrefs LoadSave { get => loadSave; }

    private SaveStruct save = new SaveStruct();

    public SaveStruct SavedInMemory { get => save; set => save = value; }

    [SerializeField]
    [Tooltip("Name used for saving")]
    private string playerName;

    [SerializeField]
    private float elapsedTime;
        
    public List<Teleporter> teleporters;

    //singleton
    void Awake() {
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Destroy(gameObject);
        }

        saveState = GetComponent<SaveHelper>();
        loadSave = GetComponent<LoadPrefs>();
        StartCoroutine(ScanGraph());
	}
    // Start is called before the first frame update
    void Start()
    {
		Time.timeScale = 1f;
        playerManager = PlayerManager.instance;
        playerName = ChoosePlayer.playerName;
        SaveState.SaveName = ChoosePlayer.saveSlot + "." + playerName;
        LoadSaved();
        SpawnHandler();//spawns the  player upon start
    }

    //Called when its a new game
    private void NewGame() {
        Debug.Log("Starting a new game");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //respawns player in active respawner -> do not use for death
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

    //Deactivate all spawners in the list
    public void DeactivateSpawners() {
        foreach (Respawner r in respawners) {
            r.isActive = false;
        }
    }

    //activate saved spawn
    private void ActivateSpawn() {
        int index = SavedInMemory.position;
        if (respawners.Count == 0 || respawners.Count <= index) return;
        respawners[index].isActive = true;        
    }

    //Save current stats on defined spawner
    public void SaveCurrent(int scene, int respawner) {
        SaveState.SetScene(scene);
        SaveState.SetRespawner(respawner);
        SaveState.SetHP(playerManager.currentHealth);
        SaveState.SetMana(playerManager.currentMana);
        SaveState.SetMoney(playerManager.currentMoney);
        SaveState.SetAmmo(playerManager.currentAmmo);
        SaveState.SetProgression(ProgressionTracker.instance.GetProgression());
        SaveState.SetInventory(GetComponent<InventorySaveHelper>().SerializeInventory(InventoryHandler.instance.Inventory));
        SaveState.SetEquipped(GetComponent<InventorySaveHelper>().SerializeInventory(InventoryHandler.instance.Equiped));
        ChoosePlayer.minimaps[scene - 1] = MinimapManager.instance.GetMiniMapRooms();
        SaveState.SetMinimap(MinimapManager.instance.SerializeMinimap(ChoosePlayer.minimaps));
        SaveState.SetTime(TimerHandler.instance.ElapsedTime);

        try {
            SaveState.Save();
        }
        catch (System.Exception e) {
            Debug.Log("Couldnt save");
            Debug.Log(e.ToString());
        }
    }

    //Load saved player stats
    private void LoadSaved() {
        try {
            SaveState.LoadSave(); //load the save file content to memory
        }
        catch (System.Exception e){
            Debug.Log("Error Loading with exception:" + e.ToString());
            NewGame();
            return;
        }
        ActivateSpawn();
        playerManager.SetPlayer(SavedInMemory.hp, SavedInMemory.mana, SavedInMemory.money, SavedInMemory.ammo);
        ProgressionTracker.instance.SetProgression(SavedInMemory.progression);
        MinimapManager.instance.DeserializeMinimap(SavedInMemory.miniMapRooms);
        MinimapManager.instance.SetMiniMapRooms(ChoosePlayer.minimaps[save.scene-1]);
        TimerHandler.instance.SetTimer(SavedInMemory.timeElapsed);
        LoadInventory();
    }

    //to be called after the UI Manager is loaded fully
    public void LoadInventory() {
        //check if there is a file first
        GetComponent<InventorySaveHelper>().DeserializeInventory(SavedInMemory.inventory, false);
        GetComponent<InventorySaveHelper>().DeserializeInventory(SavedInMemory.equipped, true);
    }

    //inserts current minimap in static minimap string
    public void SetMinimapString() {
        ChoosePlayer.minimaps[save.scene - 1] = MinimapManager.instance.GetMiniMapRooms();
    }
    //returns Spawner Index or -1 if not present
    public int GetSpawnerIndex(Respawner respawner) {
        int i = 0;
        foreach (Respawner r in respawners) {
            if (r.Equals(respawner))
                return i;
            i++;
        }
        return -1;
    }

    // to be called upon death
    public void DeathHandler() {
        /*
        //discards current inventory and equipped items
        InventoryHandler.instance.ClearItems();
        //load saved stats
        this.LoadSaved();
        //load save inventory and equipped items
        this.LoadInventory();
        //respawns player
        this.SpawnHandler();
        */

        //Activate Death Screen
        PauseMenuManager.instance.ActivateDeathScreen();
    }

    public void ReloadScene() {
        SceneManager.LoadScene(save.scene);
    }

    public void Pause(bool flag) {
        if (flag) Time.timeScale = 0f;
        else Time.timeScale = 1f;
        isPaused = flag;
    }

	IEnumerator ScanGraph()
	{
		yield return new WaitForSeconds(0.1f);
		
        try {
            AstarPath.active.data.DeserializeGraphs();
            AstarPath.active.Scan();
            Debug.Log("Graph scan");
        }
        catch(System.Exception e) {
            Debug.Log("failed to scan A* path with error: " + e.ToString());
        }
	}

}
