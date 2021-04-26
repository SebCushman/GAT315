using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class EnumUI : MonoBehaviour
{
    public TMP_Dropdown dropdown;
    public EnumData data;

    private void OnValidate()
    {
        if (data != null)
        {
            name = data.name;

            dropdown.ClearOptions();
            dropdown.AddOptions(new List<string>(data.names));
        }
    }

    private void Start()
    {
        dropdown.value = data.index;
        dropdown.onValueChanged.AddListener(UpdateValue);
    }

    public void UpdateValue(int value)
    {
        data.index = value;
    }
}
