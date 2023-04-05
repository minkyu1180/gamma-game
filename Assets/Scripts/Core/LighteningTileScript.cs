using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;
using System;


public class LighteningTileScript : MonoBehaviour
{
    public int FireAnimationPoint;
    private Tilemap tiles;
    private Vector3Int targetPos;
    private PlatformEffector2D effector;
    public GameObject minkyu;
    private GridLayout gridLayout;

    void Start()
    {
        tiles = gameObject.GetComponent<Tilemap>();
        effector = gameObject.GetComponent<PlatformEffector2D>();
        gridLayout = transform.parent.GetComponentInParent<GridLayout>();
    }


    void Update() // if ground that player on is lightning, collision off
    {
        //Debug.Log(gridLayout.WorldToCell(minkyu.transform.position));
        targetPos = gridLayout.WorldToCell(minkyu.transform.position);
        if (tiles.GetAnimationFrame(targetPos + Vector3Int.down) == FireAnimationPoint
        || tiles.GetAnimationFrame(targetPos + Vector3Int.down + Vector3Int.left) == FireAnimationPoint
        || tiles.GetAnimationFrame(targetPos + Vector3Int.down + Vector3Int.right) == FireAnimationPoint)
        {
            effector.surfaceArc = 0;
            gameObject.tag = "Untagged";
            gameObject.layer = LayerMask.NameToLayer("Default");
        }
        else
        {
            effector.surfaceArc = 1;
            gameObject.tag = "Ground";
            gameObject.layer = LayerMask.NameToLayer("Platform");
        }



    }
}
