using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleSoldier : Soldier
{
    protected override void Attack()
    {
        target.TryGetComponent(out Entity entity);

        if (entity != null) entity.ReceiveHit(damage);
        else Debug.LogWarning("El objetivo no es una Entidad. ¿Se asigno un Target no válido?");
    }
}
