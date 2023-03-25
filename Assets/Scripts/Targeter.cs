using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targeter : MonoBehaviour
{
    public Transform TargetClosest(float range, Allegiance targetAllegiance)
    {
        Collider2D[] possibleTargets = Physics2D.OverlapCircleAll(transform.position, range);

        float closestDistance = range + 1;
        Transform target = null;

        for (int i = 0; i < possibleTargets.Length; i++)
        {
            if (possibleTargets[i].TryGetComponent(out Entity entity))
            {
                if (entity.Team == targetAllegiance)
                {
                    if (Vector2.Distance(entity.transform.position, transform.position) < closestDistance)
                    {
                        target = entity.transform;
                        closestDistance = Vector2.Distance(entity.transform.position, transform.position);
                    }
                }
            }
        }

        return target;
    }

    public bool CheckProximityToTarget(Transform target, float distance)
    {
        Vector3 rayCastDirection = (target.position - transform.position).normalized;

        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, rayCastDirection, distance);

        int len = hits.Length;
        for (int i = 0; i < len; i++)
        {
            if (hits[i].transform == target) return true;
        }

        return false;
    }
}
