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
    public Vector3Int playerTilePosition;
    private Vector3Int playerTileDestination;

    public Material tileActuationMaterial;
    public Material tileActualMaterial;

    public CardMovement cardMovement;

    public List<GameObject> interactableTiles;

    public GameObject playerMesh; 

    // Start is called before the first frame update
    void Start()
    {
        cardMovement = cardMovement.GetComponent<CardMovement>();

        playerTilePosition = groundTilemap.WorldToCell(transform.position);

        playerTileDestination = playerTilePosition;
        StartCoroutine(SetPlayerPosition(0.01f));
    }

    IEnumerator SetPlayerPosition(float duration)
    //Sets the playerTilePosition to the Tile it is in
    {
        //Arriba
        if (playerTilePosition.y < playerTileDestination.y)
        {
            Debug.Log("Arriba");
            transform.localEulerAngles = new Vector3(0, 0, 0);
        }
        //Abajo
        else if (playerTilePosition.y > playerTileDestination.y)
        {
            Debug.Log("Abajo");
            transform.localEulerAngles = new Vector3(0, 180, 0);
        }
        //Derecha
        else if (playerTilePosition.x < playerTileDestination.x)
        {
            Debug.Log("Derecha");
            transform.localEulerAngles = new Vector3(0, 90, 0);
        }
        //Izquierda
        else if (playerTilePosition.x > playerTileDestination.x)
        {
            Debug.Log("Izquierda");
            transform.localEulerAngles = new Vector3(0, 270, 0);
        }

        playerTilePosition = playerTileDestination;

        float time = 0;

        while (time < duration)
        {
            transform.position = Vector3.Lerp(transform.position, groundTilemap.GetCellCenterWorld(playerTilePosition), time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        transform.position = groundTilemap.GetCellCenterWorld(playerTilePosition);
    }

    public void DashAttack()
    {
        for (int i = 0; i < interactableTiles.Count; i++)
        {
            if (groundTilemap.WorldToCell(interactableTiles[i].transform.position) == groundTilemap.WorldToCell(cardMovement.groundhittingPoint))
            {
                playerTileDestination = groundTilemap.WorldToCell(interactableTiles[i].transform.position);
                StartCoroutine(SetPlayerPosition(0.4f));
            }
        }
    }

    public void MoveCard()
    {
        for (int i = 0; i < interactableTiles.Count; i++)
        {
            if (groundTilemap.WorldToCell(interactableTiles[i].transform.position) == groundTilemap.WorldToCell(cardMovement.groundhittingPoint))
            {
                playerTileDestination = groundTilemap.WorldToCell(interactableTiles[i].transform.position);
                StartCoroutine(SetPlayerPosition(0.2f));
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

            if (sphereColliders.Length == 1 && sphereColliders[0].gameObject.CompareTag("Suelo"))
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

            if (sphereColliders.Length == 1 && sphereColliders[0].gameObject.CompareTag("Suelo"))
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

            if (sphereColliders.Length == 1 && sphereColliders[0].gameObject.CompareTag("Suelo"))
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

            if (sphereColliders.Length == 1 && sphereColliders[0].gameObject.CompareTag("Suelo"))
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
