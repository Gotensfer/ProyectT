using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Proyectile : MonoBehaviour
{
    [SerializeField] float velocity = 1f;

    Vector3 direction;
    Allegiance targetAllegiance;
    int damage;

    int piercedTargets;

    private void Update()
    {
        Vector3 movement = direction * velocity * Time.deltaTime;
        transform.position += movement;
    }

    public void Init(Vector3 direction, Allegiance targetAllegiance, int damage)
    {
        this.direction = direction.normalized;
        this.targetAllegiance = targetAllegiance;
        this.damage = damage;

        float rotationRadians = Mathf.Atan2(direction.y, direction.x);
        RotateSprite(rotationRadians);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Entity entity))
        {
            if (entity.Team == targetAllegiance && piercedTargets < 1)
            {
                piercedTargets++;
                entity.ReceiveHit(damage);
                Destroy(gameObject);
            }
        }
    }

    void RotateSprite(float radians)
    {
        transform.rotation = Quaternion.Euler(0, 0, radians * Mathf.Rad2Deg);
    }
}
