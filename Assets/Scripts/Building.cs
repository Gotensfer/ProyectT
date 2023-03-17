using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : Entity
{  
    [Header("Building config")]
    [SerializeField] GameObject foundationBuilding;
    [SerializeField] GameObject halfdoneBuilding;
    [SerializeField] GameObject finishedBuildingVisual;

    [SerializeField] private int requieredBuildPoints;
    private int currentBuildPoints;

    protected bool isActive;
    private bool isBuilt;

    [SerializeField] protected int size;
    public int Size { get => size; }

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

    private void FinishBuilding()
    {
        Destroy(foundationBuilding);
        Destroy(halfdoneBuilding);

        finishedBuildingVisual.SetActive(true);

        isActive = true;
        isBuilt = true;
    }
}
