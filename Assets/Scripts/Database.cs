using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Database : ScriptableObject {
    public List<int> exampleint;
    public ExampleClass exampleclass;
    public List<string> savenames;
    [System.Serializable]
    public class ExampleClass
    {
        public List<string> str;
    }
}
