using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallChecker : MonoBehaviour
{
    public bool CheckForWall()
    {
        BoxCollider2D box = GetComponent<BoxCollider2D>();
        List<Collider2D> results = new List<Collider2D>();
        ContactFilter2D contactFilter = new ContactFilter2D();
        LayerMask layerMask = LayerMask.GetMask("Wall");
        contactFilter.SetLayerMask(layerMask);
        if (box.OverlapCollider(contactFilter, results) == 0)
        {
            return false;
        }
        return true;
    }

    public bool CheckWalkable()
    {
        BoxCollider2D box = GetComponent<BoxCollider2D>();
        List<Collider2D> results = new List<Collider2D>();
        ContactFilter2D contactFilter = new ContactFilter2D();
        LayerMask layerMask = LayerMask.GetMask("Wall", "Unwalkable");
        contactFilter.SetLayerMask(layerMask);
        if (box.OverlapCollider(contactFilter, results) == 0)
        {
            return false;
        }
        return true;
    }
}
