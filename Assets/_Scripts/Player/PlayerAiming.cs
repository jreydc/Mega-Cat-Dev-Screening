using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAiming : MonoBehaviour
{
    [SerializeField] private Transform turretTransform;
    [SerializeField] private InputReader inputReader;

    private void LateUpdate()
    {
        Vector2 aimScreenPosition = inputReader.AimPosition;
        Vector2 aimWorldPosition = Camera.main.ScreenToWorldPoint(aimScreenPosition);

        // Calculate the direction vector from turret to aim position
        Vector2 direction = aimWorldPosition - aimScreenPosition;

        // Calculate the angle in radians using the direction vector
        float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;

        // Rotate the turret towards the aim position
        turretTransform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
