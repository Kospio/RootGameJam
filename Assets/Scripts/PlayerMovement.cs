using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerMovement : MonoBehaviour
{
    public Tilemap groundTilemap;
    public GameObject InitialTile;

    private Vector3Int playerTilePosition;

    // Start is called before the first frame update
    void Start()
    {
        SetPlayerTilePosition();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(groundTilemap.WorldToCell(gameObject.transform.position));

        if (Input.GetButtonDown("W"))
        {
            transform.position = groundTilemap.CellToWorld(playerTilePosition + new Vector3Int(0, 1, 0));
            SetPlayerTilePosition();
        }
        if (Input.GetButtonDown("S"))
        {
            transform.position = groundTilemap.CellToWorld(playerTilePosition + new Vector3Int(0, -1, 0));
            SetPlayerTilePosition();
        }
        if (Input.GetButtonDown("D"))
        {
            transform.position = groundTilemap.CellToWorld(playerTilePosition + new Vector3Int(1, 0, 0));
            SetPlayerTilePosition();
        }
        if (Input.GetButtonDown("A"))
        {
            transform.position = groundTilemap.CellToWorld(playerTilePosition + new Vector3Int(1, 0, 0));
            SetPlayerTilePosition();
        }
    }

    void SetPlayerTilePosition()
    {
        playerTilePosition = groundTilemap.WorldToCell(gameObject.transform.position);
    }

}
