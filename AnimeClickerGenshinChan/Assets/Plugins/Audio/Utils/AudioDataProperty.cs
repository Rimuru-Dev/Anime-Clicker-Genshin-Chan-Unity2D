using System;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace AbyssMoth.Plugins.Audio.Utils
{
    [Serializable]
    [SuppressMessage("ReSharper", "NotAccessedField.Local")]
    public class AudioDataProperty
    {
        [SerializeField] private string _id = "Null";
        [SerializeField] private string _key = "None";
    
        public string Key => _key;
    }
}