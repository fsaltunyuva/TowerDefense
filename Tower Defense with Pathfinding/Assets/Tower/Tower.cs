using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private int towerCost = 75;
    [SerializeField] private float buildDelay = 1f;

    private void Start()
    {
        StartCoroutine(Build());
    }
    
    public bool CreateTower(Tower towerPrefab, Vector3 position)
    {
        Bank bank = FindObjectOfType<Bank>();
        
        if (bank == null) { return false; }
        
        if (bank.CurrentBalance >= towerCost)
        {
            Instantiate(towerPrefab, position, Quaternion.identity);
            bank.Withdraw(towerCost);
            return true;
        }
        
        return false;
    }

    IEnumerator Build()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
            foreach (Transform grandChild in child)
            {
                grandChild.gameObject.SetActive(false);
            }
        }
        
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
            yield return new WaitForSeconds(buildDelay);
            foreach (Transform grandChild in child)
            {
                grandChild.gameObject.SetActive(true);
            }
        }
    }
    
}
