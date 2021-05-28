using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    [SerializeField]
    private SavePrefs savePrefs;
    [SerializeField]
    private LoadPrefs loadPrefs;

    public SavePrefs SavePrefs { get => savePrefs; }
    public LoadPrefs LoadPrefs { get => loadPrefs; }
    //singleton
    void Awake() {
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Destroy(gameObject);
        }

        savePrefs = GetComponent<SavePrefs>();
        loadPrefs = GetComponent<LoadPrefs>();
        StartCoroutine(ScanGraph());
	}
    // Start is called before the first frame update
    void Start()
    {        
        if (player == null) {
            //player = Instantiate(player);
        }
        playerManager = PlayerManager.instance;
        LoadSaved();
        SpawnHandler();//spawns the  player upon start
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
        int index = loadPrefs.GetRespawner();
        if (respawners.Count == 0 || respawners.Count <= index) return;
        respawners[index].isActive = true;        
    }

    //Save current stats on defined spawner
    public void SaveCurrent(int scene, int respawner) {
        SavePrefs.SetScene(scene);
        SavePrefs.SetRespawner(respawner);
        SavePrefs.SetHP(playerManager.currentHealth);
        SavePrefs.SetMana(playerManager.currentMana);
        SavePrefs.SetMoney(playerManager.currentMoney);
        SavePrefs.SetAmmo(playerManager.currentAmmo);
        SavePrefs.SetProgression(ProgressionTracker.instance.GetProgression());
        SavePrefs.SetInventory(GetComponent<InventorySaveHelper>().SerializeInventory(InventoryHandler.instance.Inventory));
        SavePrefs.SetEquipped(GetComponent<InventorySaveHelper>().SerializeInventory(InventoryHandler.instance.Equiped));

        try {
            SavePrefs.Save();
        }
        catch (System.Exception e) {
            Debug.Log("Couldnt save");
            Debug.Log(e.ToString());
        }
    }

    //Load saved player stats
    private void LoadSaved() {
        ActivateSpawn();
        playerManager.SetPlayer(LoadPrefs.GetHP(), LoadPrefs.GetMana(), LoadPrefs.GetMoney(), LoadPrefs.GetAmmo());
        ProgressionTracker.instance.SetProgression(LoadPrefs.GetProgression());
    }

    //to be called after the UI Manager is loaded fully
    public void LoadInventory() {
        GetComponent<InventorySaveHelper>().DeserializeInventory(LoadPrefs.GetInventory(), false);
        GetComponent<InventorySaveHelper>().DeserializeInventory(LoadPrefs.GetEquipped(), true);
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
        //discards current inventory and equipped items
        InventoryHandler.instance.ClearItems();
        //load saved stats
        this.LoadSaved();
        //load save inventory and equipped items
        this.LoadInventory();
        //respawns player
        this.SpawnHandler();
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
