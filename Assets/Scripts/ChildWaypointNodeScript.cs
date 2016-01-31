using UnityEngine;
using System.Collections;
using UnityEditor;

public class ChildWaypointNodeScript : MonoBehaviour
{

    public System.Collections.Generic.List<GameObject> connectedNodes;

	// Use this for initialization
	void Start ()
	{
	    transform.GetChild(0).gameObject.SetActive(false);

	    foreach (var node in connectedNodes)
	    {
	        node.GetComponent<ChildWaypointNodeScript>().AddToConnectedNodes(gameObject);
	    }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public GameObject GetRandomConnectedNode()
    {
        int index = Random.Range(0, connectedNodes.Count);
        return connectedNodes[index];
    }

    public void AddToConnectedNodes(GameObject nodeToAdd)
    {
        connectedNodes.Add(nodeToAdd);
    }

#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        foreach (var node in connectedNodes)
        {
            Gizmos.DrawLine(transform.position, node.transform.position);
        }
    } 

#endif  

}
