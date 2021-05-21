using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AltarOfRespawn : NPCBehaviour {

    private Respawner respawner;
    private static GameManager manager;
    // Start is called before the first frame update
    void Start() {
        respawner = GetComponent<Respawner>();
        manager = GameManager.instance;
    }

    public override void OnInteract() {
        base.OnInteract();
        manager.DeactivateSpawners();
        respawner.isActive = true;        
        Debug.Log("Progress was saved on spawner: " + manager.GetSpawnerIndex(respawner));
        manager.SaveCurrent(0, manager.GetSpawnerIndex(respawner));
        this.TMPText.SetText("Progress Saved");                     
    }
}