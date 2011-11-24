using UnityEngine;
using UnityEditor;

// /////////////////////////////////////////////////////////////////////////////////////////////////////////
//
// Batch copies and re-applies transforms.
// Useful to capture modifications while running the game in the IDE.
//
// Copy Transforms: 
// to copy the rotation, position. scale of all selected GameObjects 
// Apply Tansforms:
// Applies all transform from the capture command to the selected GameObjects
// the new functionality in Custom -> Copy Transforms. Enjoy! :-)
// 
// Inspired by Martin Schultz's TextureImportSettings script: e-mail: ms@decane.net
//
// Developed by Jerome Maurey-Delaunay
// e-mail: jerome@wemakedotcoms.com
//
// /////////////////////////////////////////////////////////////////////////////////////////////////////////


public class CaptureTransform : ScriptableObject {
	

	private static Object[] gameObjects;
	private static Vector3[] positions;
	private static Quaternion[] rotations;
	private static Vector3[] scales;

	[MenuItem ("Custom/Copy Transforms")]
	static void CaptureGameObject() {
        gameObjects = GetSelectedGameObjects();
        
		int lenght = gameObjects.Length;
		string str;
		GameObject go;
		
		positions 	= new Vector3[ lenght ];
		rotations	= new Quaternion[ lenght ];
		scales 		= new Vector3[ lenght ];
		
		for (int i = 0; i < lenght; i++) 
		{	
			go = (GameObject) gameObjects[i];
			
			positions[i] 	= CloneVector3D( go.transform.position );
			rotations[i] 	= CloneQuaternion( go.transform.rotation );
			scales[i] 		= CloneVector3D( go.transform.localScale );
			
			str  = "---------------------- \n";
			str += go.name + " >> ";
			str += "position: " + go.transform.position.ToString() + " | ";
			str += "rotation: " +go.transform.rotation.ToString() + " | ";
			str += "scale: " +go.transform.localScale.ToString();
			
			Debug.Log(str);
			
		}
    }
	
	[MenuItem ("Custom/Apply Transforms")]
	static void ApplyTransformToGameObject() {
		
		Selection.objects = gameObjects;
		
		int lenght = gameObjects.Length;
		GameObject go;
		
		for (int i = 0; i < lenght; i++) 
		{	
			go = (GameObject) gameObjects[i];
			go.transform.position = positions[i];
			go.transform.rotation = rotations[i];
			go.transform.localScale = scales[i];
			
		}
    }
	
	
	static Object[] GetSelectedGameObjects()
    {
        return Selection.GetFiltered(typeof(GameObject), SelectionMode.DeepAssets);
    }
	
	static Vector3 CloneVector3D ( Vector3 v )
	{
		return new Vector3(v.x, v.y, v.z);
	}
	
	static Quaternion CloneQuaternion ( Quaternion q )
	{
		return new Quaternion(q.x, q.y, q.z, q.w);
	}
}
