using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacialExpression
{
    private string name;
    private float ratio;
    private float time;
    private float duration;
    private BlendShape[] shapeList;
    private float[] leftEyeRotation;
    private float[] rightEyeRotation;
    private bool hasTeeth = false;
    private float[] weights;
    private float teethRate = 1f;

    public FacialExpression(string name, BlendShape[] shapeList, float[] weights, float[] left, float[] right, bool hasTeeth)
    {
        this.name = name;
        this.shapeList = shapeList;
        this.weights = weights;
        this.hasTeeth = hasTeeth;
        ratio = 0f;
        time = 0f;
        duration = 600f;
        leftEyeRotation = left;
        rightEyeRotation = right;
        this.hasTeeth = hasTeeth;
        CalcTeethRate();
    }

    public string Name() { return name; }

    public float GetRatio() { return ratio; }

    public float GetTime() { return time; }

    public float GetDuration() { return duration; }

    public void PassTime(float speed) { 
        time += speed;
    }

    public bool isOver() { return time >= duration; }

    public bool isActive() { return ratio > 0f; }

    public bool isPeaked() { return ratio >= 100f; }

    public bool hasBlendShape(BlendShape shape) {
        BlendShape b;

        for (int i = 0; i < shapeList.Length; i++) {
            b = shapeList[i];
            if (b.Name() == shape.Name()) return true;
        }
        return false;
    }

    public void changeExpression(SkinnedMeshRenderer smr, SkinnedMeshRenderer upperTeeth, SkinnedMeshRenderer lowerTeeth, GameObject leftEye, GameObject rightEye, float speed, bool isIncrease) {
        if (isIncrease) {
            ratio += speed;
            if (leftEye != null && rightEye != null) {
                leftEye.transform.Rotate(leftEyeRotation[0], leftEyeRotation[1], leftEyeRotation[2], Space.Self);
                rightEye.transform.Rotate(rightEyeRotation[0], rightEyeRotation[1], rightEyeRotation[2], Space.Self);
            }
        }
        else {
            ratio -= speed;
            if (leftEye != null && rightEye != null) {
                leftEye.transform.Rotate(-1 * leftEyeRotation[0], -1 * leftEyeRotation[1], -1 * leftEyeRotation[2], Space.Self);
                rightEye.transform.Rotate(-1 * rightEyeRotation[0], -1 * rightEyeRotation[1], -1 * rightEyeRotation[2], Space.Self);
            } 
        }

        BlendShape b;
        float w;

        for (int i = 0; i < shapeList.Length; i++)
        {
            b = shapeList[i];
            w = weights[i];
            smr.SetBlendShapeWeight(b.Id(), ratio * w);
        }
        
        if (hasTeeth) {
            upperTeeth.SetBlendShapeWeight(0, ratio*teethRate);
            lowerTeeth.SetBlendShapeWeight(0, ratio*teethRate);
        }
    }

    public void resetTime(SkinnedMeshRenderer upperTeeth, SkinnedMeshRenderer lowerTeeth) { 
        ratio = 0f;
        time = 0f;

        if (hasTeeth) {
            upperTeeth.SetBlendShapeWeight(0, 0f);
            lowerTeeth.SetBlendShapeWeight(0, 0f);
        }
    }

    public void CalcTeethRate() {
        if (!hasTeeth) {
            return;
        }

        if (name.EndsWith("X"))
        {
            teethRate = 2f;
        }
        else if (name.EndsWith("Y"))
        {
            teethRate = 2.5f;
        }
        else if (name.EndsWith("Z"))
        {
            teethRate = 1.5f;
        }
    }
}
