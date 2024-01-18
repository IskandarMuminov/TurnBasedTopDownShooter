using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridObject
{
    private GridPosition gridPosition;
    private GridSystem gridSystem;
    private List<Unit> unitList;

    public GridObject(GridSystem gridSystem, GridPosition gridPosition) {
        this.gridSystem = gridSystem;   
        this.gridPosition = gridPosition;
        unitList = new List<Unit>();
    }

    public void AddUnitAtGridObject(Unit unit) {
        unitList.Add(unit);
    }

    public List<Unit> GetUnitListAtGridObject() {
        return unitList;
    }
    public void RemoveUnit(Unit unit) {
        unitList.Remove(unit);
    }

    public override string ToString()
    {
        string unitString = string.Empty;
        foreach (Unit unit in unitList)
        {
            unitString += unit + "\n" ;
        }
        return gridPosition.ToString()+ "\n" + unitString;
    }

    public bool HasUnit() {
        return unitList.Count > 0;
    }
}
