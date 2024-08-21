using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ShopController : MonoBehaviour
{
    [SerializeField] Button closeBtn, farmerBtn1, farmerBtn2, farmerBtn3, shopBtn, upgradeBtn;
    [SerializeField] GameObject farmer1, farmer2, farmer3, shop;
    private int fee1, fee2, fee3;

    public Transform pos;

    private void OnEnable()
    {
        shopBtn.gameObject.SetActive(true);
        
        shop.SetActive(false);

        if (pos.childCount <= 0)
        {
            upgradeBtn.interactable = false;
        }
    }

    private void Start()
    {
        Int32.TryParse(farmerBtn1.GetComponentInChildren<Text>().text, out fee1);
        Int32.TryParse(farmerBtn2.GetComponentInChildren<Text>().text, out fee2);
        Int32.TryParse(farmerBtn3.GetComponentInChildren<Text>().text, out fee3);

        closeBtn.onClick.AddListener(() =>
        {
            if (shopBtn.gameObject.activeSelf)
            {
                this.gameObject.SetActive(false);
            }
            else
            {
                shopBtn.gameObject.SetActive(true);
                upgradeBtn.gameObject.SetActive(true);
                shop.gameObject.SetActive(false);
            }
        }
        );

        farmerBtn1.onClick.AddListener(() =>
        {
            if (pos.childCount > 0)
            {
                Destroy(pos.GetChild(0).gameObject);
            }
            
            if (GameManager.Instance.totalCoin >= fee1)
            {
                Instantiate(farmer1, pos);
                this.gameObject.SetActive(false);
                GameManager.Instance.totalCoin -= fee1;
            }
            
        });

        farmerBtn2.onClick.AddListener(() =>
        {
            if (pos.childCount > 0)
            {
                Destroy(pos.GetChild(0).gameObject);
            }

            if (GameManager.Instance.totalCoin >= fee2)
            {
                Instantiate(farmer2, pos);
                this.gameObject.SetActive(false);
                GameManager.Instance.totalCoin -= fee2;
            }
        });

        farmerBtn3.onClick.AddListener(() =>
        {
            if (pos.childCount > 0)
            {
                Destroy(pos.GetChild(0).gameObject);
            }

            if (GameManager.Instance.totalCoin >= fee3)
            {
                Instantiate(farmer3, pos);
                this.gameObject.SetActive(false);
                GameManager.Instance.totalCoin -= fee3;
            }
        });

        shopBtn.onClick.AddListener(() => { shop.SetActive(true); upgradeBtn.gameObject.SetActive(false); shopBtn.gameObject.SetActive(false); });

        upgradeBtn.onClick.AddListener(UpgradeFarmer);
    }

    private void UpgradeFarmer()
    {
        FarmerController farmer = pos.GetComponentInChildren<FarmerController>();
        farmer.dame *= 1.2f;
        farmer.cost = Mathf.RoundToInt(farmer.cost * 1.5f);
    }

    private void OnDisable()
    {
        pos = null;
    }
}
