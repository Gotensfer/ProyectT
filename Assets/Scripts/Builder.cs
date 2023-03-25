using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Common;
using UnityEngine;

public class Builder : Entity
{
    Building targetBuilding;

    [SerializeField] float buildingDistance = 1f;

    [SerializeField] float movementSpeed = 1f;

    [SerializeField] int buildingStrenght;

    [SerializeField] float buildingCD;

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

    IEnumerator BuildBehaviour()
    {
        while (true)
        {
            if (!CheckNearbyBuilding())
            {
                Move(transform.position - targetBuilding.transform.position);
            }
            else
            {
                targetBuilding.AddBuildPoints(buildingStrenght);
                yield return new WaitForSeconds(buildingCD);
            }

            yield return null;
        }
    }

    IEnumerator ReturnBehaviour()
    {
        while (true)
        {
            
        }
    }
}
