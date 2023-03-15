using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedSoldier : Soldier
{
    [Header("Ranged soldier stats")]
    [SerializeField] GameObject rangedProyectile;

    protected override void Attack()
    {
        target.TryGetComponent(out Entity entity);

        if (entity != null)
        {
            GameObject newProyectile = Instantiate(rangedProyectile, transform.position, Quaternion.identity, TemporalObjectsSystem.Instance.proyectilesContainer);
            Vector3 targetDirection = (target.position - transform.position).normalized;
            newProyectile.GetComponent<Proyectile>().Init(targetDirection, targetAllegiance, damage);
        }
        else Debug.LogWarning("El objetivo no es una Entidad. ¿Se asigno un Target no válido?");
    }
}
