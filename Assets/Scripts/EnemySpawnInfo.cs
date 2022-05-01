using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawnInfo : MonoBehaviour
{
    [SerializeField] private TMP_Text _name;

    public void SetName(string s)
    {
        _name.text = s;
    }
}
