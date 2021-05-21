using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePrefs : MonoBehaviour
{
    private static GameManager manager;
    // Start is called before the first frame update
    void Start()
    {
        manager = GameManager.instance;
    }
    /*
     * Add all the methods to set any player atributes to set the Player Prefs variable.
     * To save the set variables call the method Save()
     * 
     */

    //Save the room (scene) the player is currently saved (where 0 => first level, n-1 => last level)
    public void SetScene(int scene) {
        PlayerPrefs.SetInt("Scene", scene);
    }
    //Set the position of respawner on Respawner list of GameManager
    public void SetRespawner(int pos) {
        PlayerPrefs.SetInt("SpawnPos", pos);
    }
    //Set HP when saved
    public void SetHP(int hp) {
        PlayerPrefs.SetInt("HP", hp);
    }
    //Set Mana when saved
    public void SetMana(int mana) {
        PlayerPrefs.SetInt("Mana", mana);
    }
    //set Money when saved
    public void SetMoney(int money) {
        PlayerPrefs.SetInt("Money", money);
    }
    //set ammo when saved
    public void SetAmmo(int ammo) {
        PlayerPrefs.SetInt("Ammo", ammo);
    }
    //Set Progression tracker
    public void SetProgression(string progression) {
        PlayerPrefs.SetString("Progression", progression);
    }
    //Save current inventory
    public void SetInventory(string inventory) {
        PlayerPrefs.SetString("Inventory", inventory);
    }

    public void Save() {
        PlayerPrefs.Save();
    }
}
