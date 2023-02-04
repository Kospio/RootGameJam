using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    public Tilemap groundTilemap;
    public LayerMask obstacleLayerMask;

    [SerializeField]
    Vector3 raycastCorrectionVector = new Vector3(0, 0.5f, 0);
    //Tile Position of the player. It is used to use adyacent tiles in future functions
    [SerializeField] private Vector3Int playerTilePosition;
    private Vector3Int playerTileDestination;

    public Material tileActuationMaterial;
    public Material tileActualMaterial;

    public CardMovement cardMovement;

    public List<GameObject> interactableTiles;

    // Start is called before the first frame update
    void Start()
    {
        cardMovement = cardMovement.GetComponent<CardMovement>();

        playerTilePosition = groundTilemap.WorldToCell(transform.position);

        playerTileDestination = playerTilePosition;
        SetPlayerPosition();
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown("w"))
        //{
        //    Vector3Int directionAndSize;
        //    directionAndSize = new Vector3Int(0, +1, 0);

        //    playerTileDestination = playerTilePosition + directionAndSize;

        //    Collider[] sphereColliders = Physics.OverlapSphere(groundTilemap.GetCellCenterWorld(playerTileDestination), 0.1f);

        //    if (sphereColliders[0].gameObject.CompareTag("Suelo"))
        //    {
        //        RaycastHit hit;
        //        if (!Physics.Raycast(transform.position + raycastCorrectionVector, new Vector3(directionAndSize.x, directionAndSize.z, directionAndSize.y), out hit, directionAndSize.y))
        //        {
        //            playerTilePosition = playerTileDestination;
        //            SetPlayerPosition();
        //        }

        //    }
        //}
        //if (Input.GetKeyDown("s"))
        //{
        //    playerTileDestination = playerTilePosition + new Vector3Int(0, -1, 0);

        //    Collider[] sphereColliders = Physics.OverlapSphere(groundTilemap.GetCellCenterWorld(playerTileDestination), 0.1f);

        //    if (sphereColliders[0].gameObject.CompareTag("Suelo"))
        //    {
        //        playerTilePosition = playerTileDestination;
        //        SetPlayerPosition();
        //    }

        //}
        //if (Input.GetKeyDown("d"))
        //{
        //    playerTileDestination = playerTilePosition + new Vector3Int(+1, 0, 0);

        //    Collider[] sphereColliders = Physics.OverlapSphere(groundTilemap.GetCellCenterWorld(playerTileDestination), 0.1f);

        //    if (sphereColliders[0].gameObject.CompareTag("Suelo"))
        //    {
        //        playerTilePosition = playerTileDestination;
        //        SetPlayerPosition();
        //    }

        //}
        //if (Input.GetKeyDown("a"))
        //{
        //    playerTileDestination = playerTilePosition + new Vector3Int(-1, 0, 0);
        //    Collider[] sphereColliders = Physics.OverlapSphere(groundTilemap.GetCellCenterWorld(playerTileDestination), 0.1f);

        //    if (sphereColliders[0].gameObject.CompareTag("Suelo"))
        //    {
        //        playerTilePosition = playerTileDestination;
        //        SetPlayerPosition();
        //    }
        //}
    }

    public void SetPlayerPosition()
    //Sets the playerTilePosition to the Tile it is in
    {
        playerTilePosition = playerTileDestination;
        transform.position = groundTilemap.GetCellCenterWorld(playerTilePosition);
    }

    public void DashAttack()
    {

    }

    public void MoveCard()
    {
        for (int i = 0; i < interactableTiles.Count; i++)
        {
            if (groundTilemap.WorldToCell(interactableTiles[i].transform.position) == groundTilemap.WorldToCell(cardMovement.groundhittingPoint))
            {
                if (interactableTiles[i].gameObject.CompareTag("Suelo"))
                {
                    playerTileDestination = groundTilemap.WorldToCell(interactableTiles[i].transform.position);
                    SetPlayerPosition();
                }
            }
        }
    }

    public void LateralAttack()
    {

    }

    public void CheckIfTiles(int size)
    {
        //Check in +X
        for (int i = 1; i < size + 1; i++)
        {
            playerTileDestination = playerTilePosition + new Vector3Int(i, 0, 0);

            Collider[] sphereColliders = Physics.OverlapSphere(groundTilemap.GetCellCenterWorld(playerTileDestination), 0.1f);

            if (sphereColliders.Length == 1)
            {
                interactableTiles.Add(sphereColliders[0].gameObject);
            }
            else
            {
                break;
            }
        }

        //Check in -X
        for (int i = 1; i < size + 1; i++)
        {
            playerTileDestination = playerTilePosition + new Vector3Int(-i, 0, 0);

            Collider[] sphereColliders = Physics.OverlapSphere(groundTilemap.GetCellCenterWorld(playerTileDestination), 0.1f);

            if (sphereColliders.Length == 1)
            {
                interactableTiles.Add(sphereColliders[0].gameObject);
            }
            else
            {
                break;
            }
        }

        //Check in +Y
        for (int i = 1; i < size + 1; i++)
        {
            playerTileDestination = playerTilePosition + new Vector3Int(0, i, 0);

            Collider[] sphereColliders = Physics.OverlapSphere(groundTilemap.GetCellCenterWorld(playerTileDestination), 0.1f);

            if (sphereColliders.Length == 1)
            {
                interactableTiles.Add(sphereColliders[0].gameObject);
            }
            else
            {
                break;
            }
        }

        //Check in -Y
        for (int i = 1; i < size + 1; i++)
        {
            playerTileDestination = playerTilePosition + new Vector3Int(0, -i, 0);

            Collider[] sphereColliders = Physics.OverlapSphere(groundTilemap.GetCellCenterWorld(playerTileDestination), 0.1f);

            if (sphereColliders.Length == 1)
            {
                interactableTiles.Add(sphereColliders[0].gameObject);
            }
            else
            {
                break;
            }
        }

        foreach (GameObject go in interactableTiles)
        {
            go.GetComponent<MeshRenderer>().material = tileActuationMaterial;
        }
    }



}
