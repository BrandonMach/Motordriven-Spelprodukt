using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestHUDPlacment : MonoBehaviour
{
    public RectTransform ETPbar;
    public Vector3[] ETPbarPositions;

    public RectTransform HealthOrb;
    public Vector3[] HealthOrbPositions;



    public GameObject AttackTreeImage;
    bool AttackTreeIsActive;

    public GameObject[] Areans;
    bool BigArena;

    public HealthManager ActivateGodMode;
    public GameObject light;
    // Update is called once per frame

    private void Start()
    {
        ETPbar.anchoredPosition = ETPbarPositions[0];
        HealthOrb.anchoredPosition = HealthOrbPositions[0];
        BigArena = false;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            ActivateGodMode.GodMode = true;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            AttackTreeIsActive = !AttackTreeIsActive;
        }

        AttackTreeImage.SetActive(AttackTreeIsActive);

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            //Default

            ETPbar.anchoredPosition = ETPbarPositions[0];
            HealthOrb.anchoredPosition = HealthOrbPositions[0];
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            //Default

            ETPbar.anchoredPosition = ETPbarPositions[1];
            HealthOrb.anchoredPosition = HealthOrbPositions[1];
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            //Default

            ETPbar.anchoredPosition = ETPbarPositions[2];
            HealthOrb.anchoredPosition = HealthOrbPositions[2];
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            //Default

            ETPbar.anchoredPosition = ETPbarPositions[3];
            HealthOrb.anchoredPosition = HealthOrbPositions[3];
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            //Default

            ETPbar.anchoredPosition = ETPbarPositions[4];
            HealthOrb.anchoredPosition = HealthOrbPositions[4];
        }


        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            BigArena = !BigArena;

            foreach (var item in Areans)
            {
                item.SetActive(false);
            }

            

            Areans[BigArena ? 1 : 0].SetActive(true); //Bool true =0, false = 1
        }

        if (BigArena)
        {
            light.SetActive(true);
        }
        else
        {
            light.SetActive(false);
        }
      
    }
}
