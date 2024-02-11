using UnityEngine;
using UnityEngine.InputSystem;

public class ChangeWeapon : MonoBehaviour
{
    [SerializeField] private GameObject[] weapons;
    private int weaponNumber;
    private Controls controls;

    private void Awake()
    {
        controls = new Controls();
        controls.Player.ChangeWeapon.performed += OnChangeWeapon;
    }
    void Start()
    {
        DeactivateWeapon();
        weaponNumber = 0;
        weapons[weaponNumber].SetActive(true);
    }

    private void OnChangeWeapon(InputAction.CallbackContext context)
    {
        weaponNumber++;
        if (weaponNumber == weapons.Length+1) 
        {
            weaponNumber = 1;
        }
        DeactivateWeapon();
        weapons[weaponNumber-1].SetActive(true);
    }


    private void DeactivateWeapon()
    {
        foreach (GameObject weapon in weapons)
        {
            weapon.SetActive(false);
        }
    }
    
    private void OnEnable()
    {
        controls.Player.Enable();
    }

    private void OnDisable()
    {
        controls.Player.Disable();
    }
    
}

