using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoviment : Moviment
{
    public void PlayerRotation(LayerMask floorMask)
    {
        Ray radius = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(radius.origin, radius.direction * 100, Color.red);

        RaycastHit impact; // check collision

        if (Physics.Raycast(radius, out impact, 100, floorMask))
        {
            Vector3 playerAimPosition = impact.point - transform.position;

            playerAimPosition.y = transform.position.y;

            RotationCaracter(playerAimPosition);
        }
    }
}
