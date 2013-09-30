using NUnit.Framework;
using System;
using UnityEngine;

namespace AssemblyCSharp
{
	[TestFixture()]
	public class AngleTester
	{
		
		// The angle between dirA and dirB around axis
	public static float AngleAroundAxis (Vector3 dirA, Vector3 dirB, Vector3 axis) {
	    // Project A and B onto the plane orthogonal target axis
		
		Vector3 dist = dirA - dirB;
		
		Vector3 compa = Vector3.Project (dist, axis);
		Vector3 compb = dist - compa;
	 //   dirA = Vector3.Project (dirA, axis);
	 //   dirB = dist - Vector3.Project (dirB, axis);
	   
	    // Find (positive) angle between A and B
	    float angle = Vector3.Angle (compb, compa);
	   
	    // Return angle multiplied with 1 or -1
		Debug.Log(angle * (Vector3.Dot (axis, Vector3.Cross (dirA, dirB)) < 0 ? -1 : 1));
	    return angle * (Vector3.Dot (axis, Vector3.Cross (dirA, dirB)) < 0 ? -1 : 1);
	}
		
		
		[Test()]
		public void TestCase ()
		{
			Assert.AreEqual(AngleAroundAxis(Vector3.forward,Vector3.forward,Vector3.forward),0);
		}
	}
}

