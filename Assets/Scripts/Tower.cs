using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AITarget))]
public class Tower : Building
{
    [SerializeField] float attackRange;

    [SerializeField] int damage;
    [SerializeField] float attackCD;

    [SerializeField] GameObject towerProyectile;

    protected Allegiance targetAllegiance;
    Transform target;
    AITarget aiTarget;

    private void Awake()
    {
        aiTarget = GetComponent<AITarget>();

        if (team == Allegiance.town) targetAllegiance = Allegiance.invader;
        else targetAllegiance = Allegiance.town;

        StartCoroutine(TowerBehaviour());
    }

    void Fire()
    {
        GameObject newProyectile = Instantiate(towerProyectile, transform.position, Quaternion.identity, TemporalObjectsSystem.Instance.proyectilesContainer);
        Vector3 targetDirection = (target.position - transform.position).normalized;
        newProyectile.GetComponent<Proyectile>().Init(targetDirection, targetAllegiance, damage);
    }

    IEnumerator TowerBehaviour()
    {
        yield return null;

        while (true)
        {
            yield return new WaitForSeconds(attackCD);

            if (!isActive) continue;

            target = aiTarget.TargetClosest(attackRange, targetAllegiance);

            if (target != null) Fire();

            target = null;
        }
    }
}
