using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrewPotionInCauldron : MonoBehaviour {

    // Variables
    public Transform player;
    public Text cauldronWarn;
    public GameObject cauldronLiquid;
    public Material liquidMat;
    public Light liquidLight;


    // Use this for initialization
    private void Start() {
        cauldronWarn.enabled = false;
    }


    // When clicked on cauldron
    private void OnMouseDown() {
        float dist = Vector3.Distance(player.position, transform.position);

        if (dist < 5) {
            AudioSource audio = GetComponent<AudioSource>();
            audio.Play();

            // Brew fire potion if sufficient ingredients
            if ((PersistentManagerScript.Instance.Ingredient1 == true) &&
            (PersistentManagerScript.Instance.Ingredient2 == true) &&
            (PersistentManagerScript.Instance.Ingredient3 == true)) {
                cauldronLiquid.SetActive(true); // Start brewing
                liquidMat.SetColor("_EmissionColor", Color.red); // Change material to potion color
                liquidLight.color = Color.red; // Change light to potion color
                PersistentManagerScript.Instance.fireSpell = true; // Activate fire spell
                PersistentManagerScript.Instance.firePotion = false; // Remove potion from inventory
                PersistentManagerScript.Instance.Ingredient1 = false; // Remove ingredients from inventory
                PersistentManagerScript.Instance.Ingredient2 = false;
                PersistentManagerScript.Instance.Ingredient3 = false;
            }

            // Show warning; insufficient ingredients
            else {
                StartCoroutine(TimedWarning(1));
            }
        }
    }


    // Warning IEnumerator
    private IEnumerator TimedWarning(float duration) {
        cauldronWarn.enabled = true;
        yield return new WaitForSeconds(duration);
        cauldronWarn.enabled = false;
    }
}