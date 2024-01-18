using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoveAction : MonoBehaviour
{
    private Unit unit; 

    private Vector3 targetPosition;
    [SerializeField] private Animator animator;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float stoppingDistance = .1f;
    [SerializeField] private float rotationSpeed = 5f;

    [SerializeField] private int maxMoveDistance = 5;

    private void Awake()
    {
        unit = GetComponent<Unit>();
        targetPosition = transform.position;
        
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, targetPosition) > stoppingDistance)
        {
            Vector3 moveDirection = (targetPosition - transform.position).normalized;
            transform.position += moveDirection * moveSpeed * Time.deltaTime;
            transform.forward = Vector3.Lerp(transform.forward, moveDirection, rotationSpeed * Time.deltaTime);
            animator.SetBool("IsWalking", true);

        }

        else
        {
            animator.SetBool("IsWalking", false);
        }
    }

    public void Move(GridPosition gridPosition)
    {
        this.targetPosition = LevelGrid.Instance.GetWorldPosition(gridPosition);

    }

    public bool IsValidActionGridPosition(GridPosition gridPosition) {
        List<GridPosition> validGridPositionList = GetValidActionGridPositionList();
        return validGridPositionList.Contains(gridPosition);

    }

    //Calculate closest valid GridPos for selected unit within max distance
    public List<GridPosition> GetValidActionGridPositionList() {

        List<GridPosition> validGridPositionList = new List<GridPosition>();

        GridPosition unitGridPosition = unit.GetGridPosition();

        for (int x = -maxMoveDistance; x <= maxMoveDistance; x++) {
            for (int z = -maxMoveDistance; z <= maxMoveDistance; z++) { 

                GridPosition offsetGridPosition = new GridPosition(x, z);
                GridPosition testGridPosition = unitGridPosition + offsetGridPosition;

                //If pos in invalid, skip it 
                if(!LevelGrid.Instance.IsValidGridPosition(testGridPosition)) continue;

                //Test if testPos is the same as the one on which Unit is standing
                if (unitGridPosition == testGridPosition) continue;

                //GridPos is already occupied by Unit
                if (LevelGrid.Instance.HasUnitOnGridPosition(testGridPosition)) continue;

                validGridPositionList.Add(testGridPosition);

                Debug.Log(testGridPosition);
            }

        }


        return validGridPositionList;
    }
}
