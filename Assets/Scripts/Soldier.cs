using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Targeter))]
public abstract class Soldier : Entity
{
    [Header("Soldier stats")]
    [SerializeField] protected float targetRange = 5f;
    [SerializeField] protected float attackRange = 1.5f;
    [SerializeField] protected float attackCD = 1f;
    [SerializeField] protected float newTargetAdquisitionTime = 1f;
    [Space]
    [SerializeField] protected float movementSpeed = 1f;
    [SerializeField] protected int damage = 1;
    [Space]
    [SerializeField] float damagedVisualVelocityModifier = 1;

    protected Targeter targeter;   
    protected bool canAttack = true;
    protected bool atAttackRange;
    protected Transform target;
    protected Allegiance targetAllegiance;

    public float Range { get => targetRange; }

    protected SpriteRenderer spriteRenderer;
    protected Color originalColor;
    protected Color damagedColor = Color.red;
    protected Color dyingColor = Color.magenta;

    protected void Awake()
    {
        targeter = GetComponent<Targeter>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;

        if (team == Allegiance.town) targetAllegiance = Allegiance.invader;
        else targetAllegiance = Allegiance.town;

        StartCoroutine(SoldierBehaviour());
    }

    private void Update()
    {
        spriteRenderer.color = Color.Lerp(spriteRenderer.color, originalColor, Time.deltaTime * damagedVisualVelocityModifier);
    }

    public override void ReceiveHit(int damage)
    {
        base.ReceiveHit(damage);

        if (health > 0) spriteRenderer.color = damagedColor;
        else spriteRenderer.color = dyingColor;
    }

    protected void Move()
    {
        if (target != null)
        {
            if (!atAttackRange)
            {
                Vector2 movementVector = (target.position - transform.position).normalized * Time.deltaTime * movementSpeed;
                transform.Translate(movementVector);
            }

            if (targeter.CheckProximityToTarget(target, attackRange))
            {
                atAttackRange = true;
            }
            else
            {
                atAttackRange = false;
            }          
        }        
    }

    protected void TryAttack()
    {
        if (atAttackRange && canAttack)
        {
            Attack();
            canAttack = false;
            StartCoroutine(CDRecovery());
        }
    }

    protected virtual void Attack()
    {

    }

    protected virtual void Target(Allegiance targetAllegiance)
    {
        target = targeter.TargetClosest(targetRange, targetAllegiance);
    }

    IEnumerator SoldierBehaviour()
    {
        while (true)
        {
            if (target == null)
            {
                yield return new WaitForSeconds(newTargetAdquisitionTime);
                
                Target(targetAllegiance);
            }
            else
            {
                Move();
                TryAttack();
            }

            yield return null;
        }
    }

    protected IEnumerator CDRecovery()
    {
        yield return new WaitForSeconds(attackCD);
        canAttack = true;
    }
}
