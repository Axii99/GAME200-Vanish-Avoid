using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerArea : MonoBehaviour
{

    public int EnergyGeneration = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision) {
        PlayerMovement pm = collision.GetComponent<PlayerMovement>();
        if (pm && collision.GetComponent<PlayerMovement>().isInvincible) {
            collision.GetComponent<PlayerCharacter>().SetEnergyGetPerSecond(0);
        }
        else if (collision.gameObject.tag == "Player" && !collision.GetComponent<PlayerMovement>().isvanish)
        {
            //Debug.Log("In Area");
            collision.GetComponent<PlayerCharacter>().SetEnergyGetPerSecond(EnergyGeneration);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !collision.GetComponent<PlayerMovement>().isvanish)
        {
            collision.GetComponent<PlayerCharacter>().SetEnergyGetPerSecond(0);
        }
    }
}
