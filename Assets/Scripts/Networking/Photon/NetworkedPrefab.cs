using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NetworkedPrefab : MonoBehaviour
{
    public GameObject Prefab;
    public string Path;

    public NetworkedPrefab(GameObject obj, string path) {
        Prefab = obj;
        Path = ReturnNewPrefabPath(path);
    }

    private string ReturnNewPrefabPath(string path) {
        int extensionLength = System.IO.Path.GetExtension(path).Length;
        int startIndex = path.IndexOf("Resources");

        if (startIndex == -1)
            return string.Empty;
        else
            return path.Substring(startIndex + 10, path.Length - (10 + startIndex + extensionLength));
    }
}
