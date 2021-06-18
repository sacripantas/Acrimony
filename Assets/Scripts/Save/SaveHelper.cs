using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveHelper : MonoBehaviour
{
    private SaveStruct save;

    [SerializeField]
    [Tooltip("path for saving")]
    private string path;
    [SerializeField]
    [Tooltip("name for file saving")]
    private string saveName;

    public string SaveName { get => saveName; set => saveName = value; }
    void Awake() {
        path = Application.persistentDataPath + "/UserSave/";
    }
    // Start is called before the first frame update
    void Start() {
        save = GameManager.instance.SavedInMemory;
    }

    //Save the room (scene) the player is currently saved (where 0 => first level, n-1 => last level)
    public void SetScene(int scene) {
        save.scene = scene;
    }
    //Set the position of respawner on Respawner list of GameManager
    public void SetRespawner(int pos) {
        save.position = pos;
    }
    //Set HP when saved
    public void SetHP(int hp) {
        save.hp = hp;
    }
    //Set Mana when saved
    public void SetMana(int mana) {
        save.mana = mana;
    }
    //set Money when saved
    public void SetMoney(int money) {
        save.money = money;
    }
    //set ammo when saved
    public void SetAmmo(int ammo) {
        save.ammo = ammo;
    }
    //Set Progression tracker
    public void SetProgression(string progression) {
        save.progression = progression;
    }
    //Save current inventory
    public void SetInventory(string inventory) {
        save.inventory = inventory;
    }
    //Save current equipped items
    public void SetEquipped(string equipped) {
        save.equipped = equipped;
    }
    //Save current minimap
    public void SetMinimap(string rooms) {
        save.miniMapRooms = rooms;
    }
    //save time elapsed in seconds
    public void SetTime(float time) {
        save.timeElapsed = (float) System.Convert.ToInt32(time.ToString("0"));
    }

	public void Save() {
        Debug.Log("Saving at: " + path + saveName);
        //checks if the path exists before saving the file. If not, create;
        if (!System.IO.Directory.Exists(path)) System.IO.Directory.CreateDirectory(path);
        string data = JsonUtility.ToJson(this.save);
        try {
            File.WriteAllText(path + saveName + ".json", data);
        }
        catch (System.Exception e) {
            Debug.Log("Could not save file with exception: " + e.ToString());
        }
    }

    public void LoadSave() {
        Debug.Log("Loading from " + path + saveName + ".json");
        string file = File.ReadAllText(path + saveName + ".json");
        save = JsonUtility.FromJson<SaveStruct>(file);
        GameManager.instance.SavedInMemory = save;
        Debug.Log("From game manager: " + GameManager.instance.SavedInMemory.scene);        
    }
}
