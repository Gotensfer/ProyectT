using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITarget : MonoBehaviour
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
}
