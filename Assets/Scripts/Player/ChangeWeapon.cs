using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ChangeWeapon : MonoBehaviour
{
    [SerializeField] private GameObject[] weapons;
    [SerializeField] private Image weaponUiImage;
    [SerializeField] private Sprite[] weaponIcons;
    private int weaponNumber;
    private Controls controls;

    private void Awake()
    {
        controls = new Controls();
        controls.Player.ChangeWeapon.performed += OnChangeWeapon;
    }
    private void Start()
    {
        DeactivateWeapon();
        weaponNumber = 0;
        weaponUiImage.sprite = weaponIcons[weaponNumber];
        weapons[weaponNumber].SetActive(true);
    }

    /// <summary>
    /// Смена оружия
    /// </summary>
    /// <param name="context"></param>
    private void OnChangeWeapon(InputAction.CallbackContext context)
    {
        DeactivateWeapon();
        weaponNumber++;
        if (weaponNumber > weapons.Length-1) 
        {
            weaponNumber = 0;
        }
        weaponUiImage.sprite = weaponIcons[weaponNumber];
        weapons[weaponNumber].SetActive(true);
    }

    /// <summary>
    /// Деактивация всех оружий
    /// </summary>
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

