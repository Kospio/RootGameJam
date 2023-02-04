using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.HID;
using UnityEngine.Tilemaps;

public class PlayerMovement : MonoBehaviour
{
    public Tilemap groundTilemap;
    public LayerMask obstacleLayerMask;

    [SerializeField]
    Vector3 raycastCorrectionVector = new Vector3(0, 0.5f, 0);
    //Tile Position of the player. It is used to use adyacent tiles in future functions
    private Vector3Int playerTilePosition;
    private Vector3Int playerTileDestination;

    // Start is called before the first frame update
    void Start()
    {
        playerTilePosition = groundTilemap.WorldToCell(transform.position);

        SetPlayerPosition();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("w"))
        {
            Vector3Int directionAndSize;
            directionAndSize = new Vector3Int(0, +1, 0);

            playerTileDestination = playerTilePosition + directionAndSize;

            Collider[] sphereColliders = Physics.OverlapSphere(groundTilemap.GetCellCenterWorld(playerTileDestination), 0.1f);

            

            if (sphereColliders.Length != 0)
            {
                RaycastHit hit;
                if (!Physics.Raycast(transform.position + raycastCorrectionVector, new Vector3(directionAndSize.x,directionAndSize.z,directionAndSize.y), out hit, directionAndSize.y))
                {

                    playerTilePosition = playerTileDestination;
                    SetPlayerPosition();
                }

            }
        }
        if (Input.GetKeyDown("s"))
        {
            playerTileDestination = playerTilePosition + new Vector3Int(0, -1, 0);

            Collider[] sphereColliders = Physics.OverlapSphere(groundTilemap.GetCellCenterWorld(playerTileDestination), 0.1f);

            if (sphereColliders.Length > 0)
            {

                playerTilePosition = playerTileDestination;
                SetPlayerPosition();
            }

        }
        if (Input.GetKeyDown("d"))
        {
            playerTileDestination = playerTilePosition + new Vector3Int(+1, 0, 0);

            Collider[] sphereColliders = Physics.OverlapSphere(groundTilemap.GetCellCenterWorld(playerTileDestination), 0.1f);

            if (sphereColliders.Length > 0)
            {
                playerTilePosition = playerTileDestination;
                SetPlayerPosition();
            }

        }
        if (Input.GetKeyDown("a"))
        {
            playerTileDestination = playerTilePosition + new Vector3Int(-1, 0, 0);
            Collider[] sphereColliders = Physics.OverlapSphere(groundTilemap.GetCellCenterWorld(playerTileDestination), 0.1f);

            if (sphereColliders.Length > 0)
            {

                playerTilePosition = playerTileDestination;
                SetPlayerPosition();
            }
        }
    }

    void SetPlayerPosition()
    //Sets the playerTilePosition to the Tile it is in
    {
        transform.position = groundTilemap.GetCellCenterWorld(playerTilePosition);
    }

    void DashAttack()
    {

    }

    void MoveCard()
    {

    }

    void LateralAttack()
    {

    }

}
