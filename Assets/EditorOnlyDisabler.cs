using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class EditorOnlyDisabler : MonoBehaviour
{
    List<GameObject> hideObjects;

    // Start is called before the first frame update
    void Start()
    {
        hideObjects = new List<GameObject> ( GameObject.FindGameObjectsWithTag(GameTags.EditorOnly) );
    }

    // Update is called once per frame
    void Update()
    {
        foreach(GameObject obj in hideObjects)
        {
            if (Application.isPlaying) obj.SetActive(false);
            else obj.SetActive(true);
        }
    }
}
