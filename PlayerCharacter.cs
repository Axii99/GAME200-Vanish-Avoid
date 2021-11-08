using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCharacter : MonoBehaviour
{
    [SerializeField]
    private int CurrentEnergy = 0;

    public int MaxEnergy = 110;
    public int EnergyConsumptionPerSecond = 2;

    public GameObject RespawnPoint;
    [SerializeField]
    private int EnergyGetPerSecond = 0;

    public Slider EnergyBar;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("EnergyUpdatePerSecond", 0.0f, 0.1f);
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        EnergyBar.value = CurrentEnergy;

        if (CurrentEnergy == 0 && GetComponent<PlayerMovement>().isvanish) {
            GetComponent<PlayerMovement>().onAppear();
        }

        //for Debug
        if (Input.GetKeyDown(KeyCode.R)) {
            Respawn();
        }
    }

    public int GetCurrentEnergy()
    {
        return CurrentEnergy;
    }
    public void SetRespawnPoint(GameObject point) {
        RespawnPoint = point;
    }


    public void SetEnergyGetPerSecond(int amount) {
        EnergyGetPerSecond = amount;
    }

    private void EnergyUpdatePerSecond() {
        AddEnergy(EnergyGetPerSecond);
    }

    public void AddEnergy(int amount) {
        if (CurrentEnergy + amount > MaxEnergy) {
            CurrentEnergy = MaxEnergy;
        }
        else if (CurrentEnergy + amount < 0) {
            CurrentEnergy = 0;
        }
        else {
            CurrentEnergy += amount;
        }
    }

    public void Respawn() {
        //to do
        this.transform.position = RespawnPoint.transform.position;
        CurrentEnergy = 0;
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        AudioManager.Instance.PlaySound("Respawn");
    }
}
