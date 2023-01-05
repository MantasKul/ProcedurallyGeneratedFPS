using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoCountController : MonoBehaviour
{
    public int currentAmmo;

    public Text ammoText;

    private WeaponManager weaponManager;

    // Start is called before the first frame update
    void Start()
    {
        currentAmmo = 0;

        ammoText = GetComponent<Text>();

        weaponManager = GameObject.FindWithTag("Player").GetComponent<WeaponManager>();
    }

    private void Update()
    {
        ammoText.text = "" + weaponManager.getCurrentAmmo().ToString();

    }
}
