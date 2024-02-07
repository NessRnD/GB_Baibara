using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeWeapon : MonoBehaviour
{
    [SerializeField] private GameObject[] weapons;
    private int weaponNumber;

    void Start()
    {
        weaponNumber = 0;
        DeactivateWeapon();
    }

    void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            weaponNumber++;
            if (weaponNumber == weapons.Length+1) 
            {
                weaponNumber = 1;
            }
            DeactivateWeapon();
            weapons[weaponNumber-1].SetActive(true);
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            weaponNumber--;
            if (weaponNumber <= 0)
            {
                weaponNumber = weapons.Length;
            }
            DeactivateWeapon();
            weapons[weaponNumber - 1].SetActive(true);
        }
    }

    private void DeactivateWeapon()
    {
        foreach (GameObject weapon in weapons)
        {
            weapon.SetActive(false);
        }
    }
}
