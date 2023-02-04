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

    bool positionStablished;

    Vector3 tempOriginalPosition; 

    public PlayerMovement playermovement; 
    // Start is called before the first frame update
    void Start()
    {
        playermovement = GetComponent<PlayerMovement>(); 

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
        if (Physics.Raycast(ray, out RaycastHit raycastCardHit, float.MaxValue, cardLayer) && Input.GetMouseButton(0))
        {
            if (!positionStablished)
            {
                tempOriginalPosition = raycastCardHit.transform.position;

                positionStablished = true; 
            }

            //Luego el terreno por el que se puede mover. Se empieza a mover la carta
            if (Physics.Raycast(ray, out RaycastHit terrainHit, float.MaxValue, terrainMask))
            {
                if (terrainHit.transform.gameObject.CompareTag("Dash"))
                {
                    playermovement.DashAttack(); 
                }
                else if (terrainHit.transform.gameObject.CompareTag("Movement"))
                {
                    playermovement.MoveCard();
                }
                else if (terrainHit.transform.gameObject.CompareTag("Lateral"))
                {
                    playermovement.LateralAttack();
                }

                raycastCardHit.transform.position = terrainHit.point;
                child = raycastCardHit.transform.GetChild(0).gameObject; 

                child.transform.position = new Vector3(raycastCardHit.transform.position.x, controlMovingCard, raycastCardHit.transform.position.z);

                ActiveDeactiveLineRenderer(true, child);
                ChangeMaterialAlpha(0.4f); 
            }
        }

        //Cuando se suelta la carta y vuelve
        if (Input.GetButtonUp("Fire1"))
        {
            raycastCardHit.transform.position = tempOriginalPosition;
            child.transform.position = raycastCardHit.transform.position;

            ActiveDeactiveLineRenderer(false, child);

            positionStablished = false;

            ChangeMaterialAlpha(1); 
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
