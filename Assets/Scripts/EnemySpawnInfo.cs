using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawnInfo : MonoBehaviour
{
    [SerializeField] private TMP_Text _name;
    [SerializeField] private Image _progBar;

    public void SetName(string s)
    {
        _name.text = s;
    }

    public void UpdateProgBar(float startTime, float completionTime)
    {
        _progBar.fillAmount = (Time.time - startTime) / (completionTime * 0.98f);
    }
}
