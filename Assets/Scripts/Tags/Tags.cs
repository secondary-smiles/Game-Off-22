using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tags : MonoBehaviour
{
    public List<string> tags;

    public bool hasTag(string tag) {
        return tags.Contains(tag);
    }
}
