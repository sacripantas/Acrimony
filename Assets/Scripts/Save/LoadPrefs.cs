using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadPrefs : MonoBehaviour
{
    /*
     * 
     * Create methods for reading player prefs
     * 
     */

    //Get saved scene
    public int GetScene() {
        return PlayerPrefs.GetInt("Scene");
    }
    //Get saved position of respawner
    public int GetRespawner() {
        return PlayerPrefs.GetInt("SpawnPos");
    }

    //get saved HP 
    public int GetHP() {
       return PlayerPrefs.GetInt("HP");
    }
    //get saved mana
    public int GetMana() {
        return PlayerPrefs.GetInt("Mana");
    }
    //get saved money
    public int GetMoney() {
        return PlayerPrefs.GetInt("Money");
    }
    //get saved ammo
    public int GetAmmo() {
        return PlayerPrefs.GetInt("Ammo");
    }
    //get progression
    public string GetProgression() {
        return PlayerPrefs.GetString("Progression");
    }
    //get inventory
    public string GetInventory() {
        return PlayerPrefs.GetString("Inventory");
    }
    //get equipped
    public string GetEquipped() {
        return PlayerPrefs.GetString("Equipped");
    }
}
