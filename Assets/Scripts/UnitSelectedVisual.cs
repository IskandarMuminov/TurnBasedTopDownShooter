using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSelectedVisual : MonoBehaviour
{
    [SerializeField] private Unit unit;

    private MeshRenderer meshRenderer;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Start()
    {
        //Subscribed to the Event
        UnitActionSystem.Instance.OnSelctedUnitChanged += UnitActionSystem_OnSelectedUnitChange;
        UpdateVisual();
    }

    private void UnitActionSystem_OnSelectedUnitChange(object sender, EventArgs empty) {
        UpdateVisual();
    }

    private void UpdateVisual() {
        if (UnitActionSystem.Instance.GetSelectedUnit() == unit) meshRenderer.enabled = true;
        else meshRenderer.enabled = false;
    }
}
