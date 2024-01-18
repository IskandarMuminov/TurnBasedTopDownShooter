using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitActionSystem : MonoBehaviour
{
    public static UnitActionSystem Instance { get; private set; }

    [SerializeField] private Unit selectedUnit;
    [SerializeField] private MoveAction moveAction;
    [SerializeField] private LayerMask unitLayerMask;

    public event EventHandler OnSelctedUnitChanged;

    private void Awake()
    {
        if (Instance != null) {
            Destroy(gameObject);
        }
        Instance = this;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (TryHandleUnitSelection()) return;

            GridPosition mouseGridPosition = LevelGrid.Instance.GetGridPosition(MouseWorld.GetMousePosition());

            if (selectedUnit.GetMoveAction().IsValidActionGridPosition(mouseGridPosition)) {

                selectedUnit.GetMoveAction().Move(mouseGridPosition);
            }
        }
        if (Input.GetMouseButtonDown(1)){
            selectedUnit.GetSpinAction().Spin();
        }
    }

    private bool TryHandleUnitSelection() {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, unitLayerMask)) {
                    if (raycastHit.transform.TryGetComponent<Unit>(out Unit unit)){

                        SetSelectedUnit(unit);
                        return true;
                    }
            }
            return false;
    }

    //Event
    private void SetSelectedUnit(Unit unit) {
        selectedUnit = unit;

        OnSelctedUnitChanged?.Invoke(this, EventArgs.Empty);        
    }

    public Unit GetSelectedUnit() {
        return selectedUnit; 
    }
}
