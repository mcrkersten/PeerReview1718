using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryHandler : MonoBehaviour {
    public delegate void OnPickup();
    public static event OnPickup onPickUp;
    public delegate void OnDrop();
    public static event OnDrop onDrop;

    // Use other class to decide what to exactly do with these functions;
    void OnPickHandler () {
		if (onPickUp != null) {
            onPickUp();
        }
	}
	
	void OnDropHandler () {
        if (onDrop != null) {
            onDrop();
        }
	}
}
