using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.HID;

public class CardMovement : MonoBehaviour
{
    Camera mainCamera;
    public LayerMask cardLayer;
    public LayerMask terrainMask;

    public int controlMovingCard;

    public GameObject card1, card2, card3, deck;
    GameObject child;
    public GameObject cardSelected; 

    bool positionStablished;

    [SerializeField] Vector3 tempOriginalPosition;
    public Vector3 groundhittingPoint; 

    public PlayerMovement playermovement;

    [HideInInspector] public Vector3 selectedCardPosition; 
    // Start is called before the first frame update
    void Start()
    {
        playermovement = playermovement.GetComponent<PlayerMovement>();

        mainCamera = GetComponent<Camera>();

        ActiveDeactiveLineRenderer(false, card1.transform.GetChild(0).gameObject);
        ActiveDeactiveLineRenderer(false, card2.transform.GetChild(0).gameObject);
        ActiveDeactiveLineRenderer(false, card3.transform.GetChild(0).gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        //Primero checa si se coge la carta
        if (Input.GetMouseButtonDown(0) && !positionStablished)
        {
            if (Physics.Raycast(ray, out RaycastHit raycastCardHit, float.MaxValue, cardLayer))
            {
                cardSelected = raycastCardHit.transform.gameObject; 

                tempOriginalPosition = raycastCardHit.transform.position;

                positionStablished = true;

                if (cardSelected.transform.GetChild(0).gameObject.CompareTag("Dash"))
                {
                    playermovement.DashAttack();
                }
                if (cardSelected.transform.GetChild(0).gameObject.CompareTag("Movement"))
                {
                    playermovement.CheckIfTiles(1);
                }
                if (cardSelected.transform.GetChild(0).gameObject.CompareTag("Lateral"))
                {
                    playermovement.LateralAttack();
                }

                child = raycastCardHit.transform.GetChild(0).gameObject;

                child.transform.position = new Vector3(raycastCardHit.transform.position.x, controlMovingCard, raycastCardHit.transform.position.z);

                ActiveDeactiveLineRenderer(true, child);
                ChangeMaterialAlpha(0.4f);
            }
            
        }

        //Se tiene que hacer mientras se mantenga pulsado para poder moverse bien
        if (Physics.Raycast(ray, out RaycastHit terrainHit, float.MaxValue, terrainMask) && Input.GetMouseButton(0))
        {
            cardSelected.transform.position = terrainHit.point;

            groundhittingPoint = terrainHit.point;
        }

        //Cuando se suelta la carta y vuelve
        if (Input.GetButtonUp("Fire1"))
        {
            if (cardSelected.transform.GetChild(0).gameObject.CompareTag("Dash"))
            {
                playermovement.DashAttack();
            }

            if (cardSelected.transform.GetChild(0).gameObject.CompareTag("Movement"))
            {
                playermovement.MoveCard();
            }

            if (cardSelected.transform.GetChild(0).gameObject.CompareTag("Lateral"))
            {
                playermovement.LateralAttack();
            }

            cardSelected.transform.position = tempOriginalPosition;
            child.transform.position = cardSelected.transform.position;

            ActiveDeactiveLineRenderer(false, child);

            positionStablished = false;

            ChangeMaterialAlpha(1);

            //Limpiar la lista para otra función

            foreach (GameObject go in playermovement.interactableTiles)
            {
                go.GetComponent<MeshRenderer>().material = playermovement.tileActualMaterial;
            }
            playermovement.interactableTiles.Clear();
        }
    }

    void ActiveDeactiveLineRenderer(bool isActive, GameObject cardX)
    {
        cardX.GetComponent<LineRenderer>().enabled = isActive;
    }

    void ChangeMaterialAlpha(float alphaColor)
    {
        Color previousColor = child.GetComponent<MeshRenderer>().material.color;

        child.GetComponent<MeshRenderer>().material.color = new Color(previousColor.r, previousColor.g, previousColor.b, alphaColor);
    }
}
