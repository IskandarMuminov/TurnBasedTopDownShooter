using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseWorld : MonoBehaviour
{
    [SerializeField] private LayerMask mousePlaneMask;
    private static MouseWorld instance;

    private void Awake()
    {
        instance = this;
    }

    void Update()
    {

        transform.position = MouseWorld.GetMousePosition();
        
    }

    public static Vector3 GetMousePosition() {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, instance.mousePlaneMask);
        return raycastHit.point;
    }
}
