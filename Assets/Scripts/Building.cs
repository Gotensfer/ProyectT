using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Building : Entity
{  
    [Header("Building config")]
    [SerializeField] GameObject foundationBuilding;
    [SerializeField] GameObject halfdoneBuilding;
    [SerializeField] GameObject finishedBuildingVisual;
    [SerializeField] SpriteRenderer getHitVisual;

    [SerializeField] private int requieredBuildPoints;
    private int currentBuildPoints;

    [SerializeField] protected bool isActive;
    private bool isBuilt;

    [SerializeField] float damagedVisualVelocityModifier = 5f;

    protected Color originalColor = new Color(0.2f, 0.2f, 0.2f, 0);
    protected Color damagedColor = new Color(0.2f, 0.2f, 0.2f);
    protected Color dyingColor = Color.black;

    public void AddBuildPoints(int buildPoints)
    {
        if (!isBuilt) return;

        currentBuildPoints = Mathf.Clamp(currentBuildPoints + buildPoints, 0, requieredBuildPoints);

        float progress = (float)requieredBuildPoints / currentBuildPoints;
        int healthPerConstructionPoint = maxHealth / requieredBuildPoints;
        int finishingHealthAtLastConstructionPoint = maxHealth % requieredBuildPoints;

        AddHealth(healthPerConstructionPoint * buildPoints);

        if (progress > 0.5f)
        {
            foundationBuilding.SetActive(false);
            halfdoneBuilding.SetActive(true);
        }

        if (currentBuildPoints == requieredBuildPoints)
        {
            AddHealth(finishingHealthAtLastConstructionPoint);
            FinishBuilding();
        }
    }

    private void Update()
    {
        getHitVisual.color = Color.Lerp(getHitVisual.color, originalColor, Time.deltaTime * damagedVisualVelocityModifier);
    }

    private void FinishBuilding()
    {
        Destroy(foundationBuilding);
        Destroy(halfdoneBuilding);

        finishedBuildingVisual.SetActive(true);

        isActive = true;
        isBuilt = true;
    }

    public override void ReceiveHit(int damage)
    {
        base.ReceiveHit(damage);

        if (health > 0) getHitVisual.color = damagedColor;
        else getHitVisual.color = dyingColor;
    }

    public virtual void InitPlacement()
    {
        foundationBuilding.SetActive(true);
        halfdoneBuilding.SetActive(false);
        finishedBuildingVisual.SetActive(false);

        health = 1;

        isActive = false;
        isBuilt = false;
    }
}
