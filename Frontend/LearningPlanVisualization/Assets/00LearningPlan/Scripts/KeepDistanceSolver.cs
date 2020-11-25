using Microsoft.MixedReality.Toolkit.Utilities.Solvers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepDistanceSolver : Solver
{
    public float targetDistance = 2.0f;
    public override void SolverUpdate()
    {
        if (SolverHandler != null && SolverHandler.TransformTarget != null)
        {

            var target = SolverHandler.TransformTarget;
            var directionVector = transform.position - target.position;
            
            if (directionVector.sqrMagnitude != 0)
            {
                GoalPosition = target.position + directionVector * targetDistance / directionVector.sqrMagnitude;
            }
            
        }
    }
}
