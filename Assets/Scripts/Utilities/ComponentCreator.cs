using UnityEngine;

public static class ComponentCreator {

    public static T Create<T>() where T : Component {
        GameObject go = new GameObject();
        go.AddComponent<T>();
        return go.GetComponent<T>();
    }

    public static T Create<T>(GameObject go) where T : Component {
        go.AddComponent<T>();
        return go.GetComponent<T>();
    }
}
