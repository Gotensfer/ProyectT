using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEntity : Entity
{
    protected Targeter aiTarget;
    protected Allegiance targetAllegiance;

    protected SpriteRenderer spriteRenderer;
    protected Color originalColor;
    protected Color damagedColor = Color.red;
    protected Color dyingColor = Color.magenta;

    [SerializeField] float damagedVisualVelocityModifier = 5f;

    protected void Awake()
    {
        aiTarget = GetComponent<Targeter>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;

        if (team == Allegiance.town) targetAllegiance = Allegiance.invader;
        else targetAllegiance = Allegiance.town;
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
}
