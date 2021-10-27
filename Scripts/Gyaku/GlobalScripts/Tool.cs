using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Tool
{
    public static List<Component> GetGenerics(GameObject This){
        GenericInput Keys = This.GetComponent<GenericInput>();
   	 	GenericStats Stats = This.GetComponent<GenericStats>();
   	    GenericAnimator Anim = This.GetComponent<GenericAnimator>();
    	GenericMovement Movement = This.GetComponent<GenericMovement>();

        List<Component> components = new List<Component>();
        components.Add(Keys);
        components.Add(Stats);
        components.Add(Anim);
        components.Add(Movement);

        return components;
    }

    public static float GetColliderLenght(GameObject bounds){
        return Vector3.Distance(bounds.GetComponent<Collider>().bounds.max,bounds.GetComponent<Collider>().bounds.center);
    }

    public static void ChangeMeshColorAll(string Hex, GameObject gameObject){
        foreach (MeshRenderer g in gameObject.GetComponentsInChildren<MeshRenderer>())
			{
                Color color;
				ColorUtility.TryParseHtmlString(Hex, out color);
				g.GetComponent<MeshRenderer>().materials[0].SetColor("_EM",color);
			}
    }

}