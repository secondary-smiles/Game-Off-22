using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tags : MonoBehaviour {
    [SerializeField]
    List<string> tags;

    public bool hasTag(string tag) {
        return tags.Contains(tag);
    }

    public void addTag(string tag) {
        tags.Add(tag);
    }
}
