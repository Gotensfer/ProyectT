using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Builder : Entity
{
    Building targetBuilding;

    [SerializeField] float buildingDistance = 1f;

    [SerializeField] float movementSpeed = 1f;

    bool CheckNearbyBuilding()
    {
        Vector3 buildingDirection = (transform.position - targetBuilding.transform.position).normalized;

        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, buildingDirection, buildingDistance);

        int len = hits.Length;
        for (int i = 0; i < len; i++)
        {
            if (hits[i].transform == targetBuilding.transform) return true;
        }

        return false;
    }

    void Move(Vector3 direction)
    {
        direction.Normalize();

        Vector3 movementVector = direction * movementSpeed * Time.deltaTime;

        transform.Translate(movementVector);
    }

    IEnumerator BuilderBehaviour_Build()
    {
        while (true)
        {

        }
    }

    IEnumerator BuildBehaviour_Return()
    {
        while (true)
        {

        }
    }
}
