﻿using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace Menu
{
    public class VectorOption : Option
    {
        public TMP_InputField inputFieldX;
        public TMP_InputField inputFieldY;
        public TMP_InputField inputFieldZ;
        // Start is called before the first frame update
        public override void Awake()
        {
            base.Awake();
            
            inputFieldX.onEndEdit.AddListener(OnValueChangedX);
            inputFieldY.onEndEdit.AddListener(OnValueChangedY);
            inputFieldZ.onEndEdit.AddListener(OnValueChangedZ);

            var currentValue = (Vector3) CurrentOptions.GetType().GetField(targetOption).GetValue(CurrentOptions);
            inputFieldX.text = currentValue.x.ToString("0");
            inputFieldY.text = currentValue.y.ToString("0");
            inputFieldZ.text = currentValue.z.ToString("0");
        }

        public void OnValueChangedX(string newValue)
        {
            var currentValue = (Vector3) CurrentOptions.GetType().GetField(targetOption).GetValue(CurrentOptions);
            currentValue.x = float.Parse(newValue);
            var fieldInfo = CurrentOptions.GetType().GetField(targetOption);
            fieldInfo.SetValue(CurrentOptions, currentValue);
        }  
        
        public void OnValueChangedY(string newValue)
        {
            var currentValue = (Vector3) CurrentOptions.GetType().GetField(targetOption).GetValue(CurrentOptions);
            currentValue.y = float.Parse(newValue);
            var fieldInfo = CurrentOptions.GetType().GetField(targetOption);
            fieldInfo.SetValue(CurrentOptions, currentValue);
        }  
        
        public void OnValueChangedZ(string newValue)
        {
            var currentValue = (Vector3) CurrentOptions.GetType().GetField(targetOption).GetValue(CurrentOptions);
            currentValue.z = float.Parse(newValue);
            var fieldInfo = CurrentOptions.GetType().GetField(targetOption);
            fieldInfo.SetValue(CurrentOptions, currentValue);
        }
        
        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
