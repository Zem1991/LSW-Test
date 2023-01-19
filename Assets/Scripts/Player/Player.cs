using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private Inputs inputs;
    [SerializeField] private int money = 100;
    public Inputs Inputs { get => inputs; private set => inputs = value; }
    public int Money { get => money; private set => money = value; }

    [Header("Runtime")]
    [SerializeField] private UI ui;
    [SerializeField] private new Camera camera;
    [SerializeField] private PlayerCharacter playerCharacter;
    [SerializeField] private InteractableHighlight interactableHighlight;
    [SerializeField] private InteractablePicker interactablePicker;
    public UI UI { get => ui; private set => ui = value; }
    public Camera Camera { get => camera; private set => camera = value; }
    public PlayerCharacter PlayerCharacter { get => playerCharacter; private set => playerCharacter = value; }
    public InteractableHighlight InteractableHighlight { get => interactableHighlight; set => interactableHighlight = value; }
    public InteractablePicker InteractablePicker { get => interactablePicker; private set => interactablePicker = value; }

    private void Awake()
    {
        UI = FindObjectOfType<UI>();
        Camera = FindObjectOfType<Camera>();
        PlayerCharacter = FindObjectOfType<PlayerCharacter>();
        InteractableHighlight = GetComponentInChildren<InteractableHighlight>();
        InteractablePicker = null;
    }

    private void Start()
    {
        Inventory inventory = PlayerCharacter.GetInventory();
        Action inventoryOnChange = new InventoryUIUpdater(this, UI).Update;
        inventory.SetOwner(this);
        inventory.SetOnChange(inventoryOnChange);
    }

    private void FixedUpdate()
    {
        InputUpdater inputUpdater = new(this);
        inputUpdater.FixedUpdate();
    }

    private void Update()
    {
        InteractablePicker = new(PlayerCharacter, InteractableHighlight);
        InteractablePicker.Pick();

        InputUpdater inputUpdater = new(this);
        inputUpdater.Update();
    }

    private void LateUpdate()
    {
        CameraUpdater cameraUpdater = new(this);
        cameraUpdater.LateUpdate();

        UIUpdater inputUpdater = new(this);
        inputUpdater.LateUpdate();
    }

    //private PlayerUpdateTargets GetPlayerUpdateTargets()
    //{
    //    return new(this, camera, playerCharacter);
    //}

    public void AddMoney(int amount)
    {
        if (amount <= 0) return;
        money += amount;
    }

    public void SubtractMoney(int amount)
    {
        if (amount <= 0) return;
        money -= amount;
    }
}
