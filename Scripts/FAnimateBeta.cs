using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FAnimateBeta : MonoBehaviour
{
    List<BlendShape> bShapes = new List<BlendShape>();    //list of blendshapes
    List<FacialExpression> fExpr = new List<FacialExpression>();  //list of expressions

    string currentExpr = "None";
    string nextExpr = "None";

    SkinnedMeshRenderer skinnedMeshRenderer;
    float blendSpeed = 0.5f;

    GameObject leftEye = null;
    GameObject rightEye = null;
    SkinnedMeshRenderer upTeethRenderer = null;
    SkinnedMeshRenderer downTeethRenderer = null;

    void Awake()
    {
        skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
        ImportData();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        InputRead();
        ExecuteExpression(currentExpr);
    }

    void ExecuteExpression(string name)
    {
        FacialExpression expr = GetExpression(name);
        if (expr == null) {
            return;
        }

        if (!expr.isPeaked() && expr.GetTime() == 0)  //if expr is not active yet it will increase
        {
            expr.changeExpression(skinnedMeshRenderer, upTeethRenderer, downTeethRenderer, leftEye, rightEye, blendSpeed, true);
        }
        else if (expr.isActive() && expr.isOver())    //if expr has reached its full duration it will decrease
        {
            expr.changeExpression(skinnedMeshRenderer, upTeethRenderer, downTeethRenderer, leftEye, rightEye, blendSpeed, false);
        }
        else if (!expr.isActive() && expr.isOver())   //if expr reached 0 it stops the animation
        {
            expr.resetTime(upTeethRenderer, downTeethRenderer);
            currentExpr = nextExpr; //if there is another expression in queue, it will be given the signal to start
            nextExpr = "None";
        }
        else
        {
            expr.PassTime(blendSpeed); //if expr reached its apex, it will stay until it reaches the max defined time
        }
    }

    void InputRead()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (currentExpr == "None")
            {
                currentExpr = "Happy";
            }
            else if (nextExpr != "Happy")
            {
                nextExpr = "Happy";
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (currentExpr == "None")
            {
                currentExpr = "Sad";
            }
            else if (nextExpr != "Sad")
            {
                nextExpr = "Sad";
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (currentExpr == "None")
            {
                currentExpr = "Angry";
            }
            else if (nextExpr != "Angry")
            {
                nextExpr = "Angry";
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            if (currentExpr == "None")
            {
                currentExpr = "Disgusted";
            }
            else if (nextExpr != "Disgusted")
            {
                nextExpr = "Disgusted";
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            if (currentExpr == "None")
            {
                currentExpr = "Scared";
            }
            else if (nextExpr != "Scared")
            {
                nextExpr = "Scared";
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            if (currentExpr == "None")
            {
                currentExpr = "Surprised";
            }
            else if (nextExpr != "Surprised")
            {
                nextExpr = "Surprised";
            }
        }
        
        else if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            currentExpr = "None";
            nextExpr = "None";
        }

        else if (Input.GetKeyDown(KeyCode.Q))
        {
            if (currentExpr == "None")
            {
                currentExpr = "HappyX";
            }
            else if (nextExpr != "HappyX")
            {
                nextExpr = "HappyX";
            }
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            if (currentExpr == "None")
            {
                currentExpr = "SadX";
            }
            else if (nextExpr != "SadX")
            {
                nextExpr = "SadX";
            }
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            if (currentExpr == "None")
            {
                currentExpr = "AngryX";
            }
            else if (nextExpr != "AngryX")
            {
                nextExpr = "AngryX";
            }
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            if (currentExpr == "None")
            {
                currentExpr = "DisgustedX";
            }
            else if (nextExpr != "DisgustedX")
            {
                nextExpr = "DisgustedX";
            }
        }
        else if (Input.GetKeyDown(KeyCode.T))
        {
            if (currentExpr == "None")
            {
                currentExpr = "ScaredX";
            }
            else if (nextExpr != "ScaredX")
            {
                nextExpr = "ScaredX";
            }
        }
        else if (Input.GetKeyDown(KeyCode.Y))
        {
            if (currentExpr == "None")
            {
                currentExpr = "SurprisedX";
            }
            else if (nextExpr != "SurprisedX")
            {
                nextExpr = "SurprisedX";
            }
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            if (currentExpr == "None")
            {
                currentExpr = "HappyY";
            }
            else if (nextExpr != "HappyY")
            {
                nextExpr = "HappyY";
            }
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            if (currentExpr == "None")
            {
                currentExpr = "SadY";
            }
            else if (nextExpr != "SadY")
            {
                nextExpr = "SadY";
            }
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            if (currentExpr == "None")
            {
                currentExpr = "AngryY";
            }
            else if (nextExpr != "AngryY")
            {
                nextExpr = "AngryY";
            }
        }
        else if (Input.GetKeyDown(KeyCode.F))
        {
            if (currentExpr == "None")
            {
                currentExpr = "DisgustedY";
            }
            else if (nextExpr != "DisgustedY")
            {
                nextExpr = "DisgustedY";
            }
        }
        else if (Input.GetKeyDown(KeyCode.G))
        {
            if (currentExpr == "None")
            {
                currentExpr = "ScaredY";
            }
            else if (nextExpr != "ScaredY")
            {
                nextExpr = "ScaredY";
            }
        }
        else if (Input.GetKeyDown(KeyCode.H))
        {
            if (currentExpr == "None")
            {
                currentExpr = "SurprisedY";
            }
            else if (nextExpr != "SurprisedY")
            {
                nextExpr = "SurprisedY";
            }
        }
        else if (Input.GetKeyDown(KeyCode.Z))
        {
            if (currentExpr == "None")
            {
                currentExpr = "HappyZ";
            }
            else if (nextExpr != "HappyZ")
            {
                nextExpr = "HappyZ";
            }
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            if (currentExpr == "None")
            {
                currentExpr = "SadZ";
            }
            else if (nextExpr != "SadZ")
            {
                nextExpr = "SadZ";
            }
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            if (currentExpr == "None")
            {
                currentExpr = "AngryZ";
            }
            else if (nextExpr != "AngryZ")
            {
                nextExpr = "AngryZ";
            }
        }
        else if (Input.GetKeyDown(KeyCode.V))
        {
            if (currentExpr == "None")
            {
                currentExpr = "DisgustedZ";
            }
            else if (nextExpr != "DisgustedZ")
            {
                nextExpr = "DisgustedZ";
            }
        }
        else if (Input.GetKeyDown(KeyCode.B))
        {
            if (currentExpr == "None")
            {
                currentExpr = "ScaredZ";
            }
            else if (nextExpr != "ScaredZ")
            {
                nextExpr = "ScaredZ";
            }
        }
        else if (Input.GetKeyDown(KeyCode.N))
        {
            if (currentExpr == "None")
            {
                currentExpr = "SurprisedZ";
            }
            else if (nextExpr != "SurprisedZ")
            {
                nextExpr = "SurprisedZ";
            }
        }
    }

    void ImportData() {
        //BlendShape creation
        BlendShape jawDown = new BlendShape("JawDown", 0);
        BlendShape jawForward = new BlendShape("JawForward", 1);
        BlendShape jawLeft = new BlendShape("JawLeft", 2);
        BlendShape jawRight = new BlendShape("JawRight", 3);
        BlendShape smile = new BlendShape("Smile", 4);
        BlendShape frown = new BlendShape("Frown", 5);
        BlendShape mouthOut = new BlendShape("MouthOut", 6);
        BlendShape pucker = new BlendShape("Pucker", 7);
        BlendShape mouthUp = new BlendShape("MouthUp", 8);

        BlendShape cheekPuff = new BlendShape("CheekPuff", 14);
        BlendShape squint = new BlendShape("Squint", 15);
        BlendShape browUp = new BlendShape("BrowUp", 16);
        BlendShape innerBrowUp = new BlendShape("InnerBrowUp", 17);
        BlendShape outerBrowUp = new BlendShape("OuterBrowUp", 18);
        BlendShape innerBrowDown = new BlendShape("InnerBrowDown", 19);
        BlendShape outerBrowDown = new BlendShape("OuterBrowDown", 20);
        BlendShape browMad = new BlendShape("BrowMad", 21);
        BlendShape browSad = new BlendShape("BrowSad", 22);

        BlendShape upperLipUp = new BlendShape("UpperLipUp", 23);
        BlendShape lowerLipDown = new BlendShape("LowerLipDown", 24);

        BlendShape noseWrinkle = new BlendShape("NoseWrinkle", 33);
        BlendShape upperLidDown = new BlendShape("UpperLidDown", 34);
        BlendShape lowerLidUp = new BlendShape("LowerLidUp", 35);
        BlendShape closeEyes = new BlendShape("CloseEyes", 36);

        //Adding BlendShapes to database
        bShapes.Add(jawDown);
        bShapes.Add(jawForward);
        bShapes.Add(jawLeft);
        bShapes.Add(jawRight);
        bShapes.Add(smile);
        bShapes.Add(frown);
        bShapes.Add(mouthOut);
        bShapes.Add(pucker);
        bShapes.Add(mouthUp);

        bShapes.Add(cheekPuff);
        bShapes.Add(squint);
        bShapes.Add(browUp);
        bShapes.Add(innerBrowUp);
        bShapes.Add(outerBrowUp);
        bShapes.Add(innerBrowDown);
        bShapes.Add(outerBrowDown);
        bShapes.Add(browMad);
        bShapes.Add(browSad);

        bShapes.Add(upperLipUp);
        bShapes.Add(lowerLipDown);

        bShapes.Add(noseWrinkle);
        bShapes.Add(lowerLidUp);
        bShapes.Add(upperLidDown);
        bShapes.Add(closeEyes);

        //Facial expressions - regular, X(distortion level 2), Y(distortion level 3) and Z(distortion level 1)

        //Happy
        BlendShape[] b = { smile, lowerLidUp };
        float[] w = { 0.5f, 0.5f };
        float[] l = { 0.0f, 0.0f, 0.0f };
        float[] r = { 0.0f, 0.0f, 0.0f };
        FacialExpression happy = new FacialExpression("Happy", b, w, l, r, false);

        BlendShape[] bX = { smile, lowerLidUp, upperLidDown };
        float[] wX = { 1f, 1f, 0.25f };
        float[] lX = { 0.0f, 0.0f, 0.0f };
        float[] rX = { 0.0f, 0.0f, 0.0f };
        FacialExpression happyX = new FacialExpression("HappyX", bX, wX, lX, rX, false);

        BlendShape[] bY = { smile, lowerLidUp, upperLidDown };
        float[] wY = { 1.5f, 1.25f, 0.5f };
        float[] lY = { 0.0f, 0.0f, 0.0f };
        float[] rY = { 0.0f, 0.0f, 0.0f };
        FacialExpression happyY = new FacialExpression("HappyY", bY, wY, lY, rY, false);

        BlendShape[] bZ = { smile, lowerLidUp };
        float[] wZ = { 0.75f, 0.75f };
        float[] lZ = { 0.0f, 0.0f, 0.0f };
        float[] rZ = { 0.0f, 0.0f, 0.0f };
        FacialExpression happyZ = new FacialExpression("HappyZ", bZ, wZ, lZ, rZ, false);

        fExpr.Add(happy);
        fExpr.Add(happyX);
        fExpr.Add(happyY);
        fExpr.Add(happyZ);


        //Sad
        BlendShape[]  b2 = { frown, upperLidDown, browSad };
        float[] w2 = { 0.5f, 0.5f, 0.33f };
        float[] l2 = { 0.0f, 0.0f, 0.0f };
        float[] r2 = { 0.0f, 0.0f, 0.0f };
        FacialExpression sad = new FacialExpression("Sad", b2, w2, l2, r2, false);

        BlendShape[] b2X = { frown, upperLidDown, browSad };
        float[] w2X = { 1f, 1f, 0.66f };
        float[] l2X = { 0.0f, 0.0f, 0.0f };
        float[] r2X = { 0.0f, 0.0f, 0.0f };
        FacialExpression sadX = new FacialExpression("SadX", b2X, w2X, l2X, r2X, false);

        BlendShape[] b2Y = { frown, upperLidDown, lowerLidUp, browSad };
        float[] w2Y = { 1.5f, 1.5f, 0.5f, 1f };
        float[] l2Y = { 0.0f, 0.0f, 0.0f };
        float[] r2Y = { 0.0f, 0.0f, 0.0f };
        FacialExpression sadY = new FacialExpression("SadY", b2Y, w2Y, l2Y, r2Y, false);

        BlendShape[] b2Z = { frown, upperLidDown, browSad };
        float[] w2Z = { 0.75f, 0.75f, 0.48f };
        float[] l2Z = { 0.0f, 0.0f, 0.0f };
        float[] r2Z = { 0.0f, 0.0f, 0.0f };
        FacialExpression sadZ = new FacialExpression("SadZ", b2Z, w2Z, l2Z, r2Z, false);

        fExpr.Add(sad);
        fExpr.Add(sadX);
        fExpr.Add(sadY);
        fExpr.Add(sadZ);


        //Angry
        BlendShape[]  b3 = { frown, outerBrowUp, browMad };
        float[] w3 = { 1f, 0.5f, 0.5f };
        float[] l3 = { 0.0f, 0.0f, 0.0f };
        float[] r3 = { 0.0f, 0.0f, 0.0f };
        FacialExpression angry = new FacialExpression("Angry", b3, w3, l3, r3, false);

        BlendShape[] b3X = { frown, outerBrowUp, browMad, lowerLipDown, upperLipUp };
        float[] w3X = { 1.5f, 1f, 1f, 0.25f, 0.5f };
        float[] l3X = { 0.0f, 0.0f, 0.0f };
        float[] r3X = { 0.0f, 0.0f, 0.0f };
        FacialExpression angryX = new FacialExpression("AngryX", b3X, w3X, l3X, r3X, false);

        BlendShape[] b3Y = { frown, outerBrowUp, browMad, lowerLipDown, upperLipUp };
        float[] w3Y = { 2f, 1.25f, 1.5f, 0.5f, 0.75f };
        float[] l3Y = { 0.0f, 0.0f, 0.0f };
        float[] r3Y = { 0.0f, 0.0f, 0.0f };
        FacialExpression angryY = new FacialExpression("AngryY", b3Y, w3Y, l3Y, r3Y, false);

        BlendShape[] b3Z = { frown, outerBrowUp, browMad, lowerLipDown, upperLipUp };
        float[] w3Z = { 1.25f, 0.75f, 0.75f, 0.12f, 0.25f };
        float[] l3Z = { 0.0f, 0.0f, 0.0f };
        float[] r3Z = { 0.0f, 0.0f, 0.0f };
        FacialExpression angryZ = new FacialExpression("AngryZ", b3Z, w3Z, l3Z, r3Z, false);

        fExpr.Add(angry);
        fExpr.Add(angryX);
        fExpr.Add(angryY);
        fExpr.Add(angryZ);


        //Disgusted
        BlendShape[] b4 = { noseWrinkle, closeEyes, mouthUp, pucker, mouthOut, squint, frown, upperLipUp, browMad };
        float[] w4 = { 1f, 0.5f, 0.25f, 0.2f, 0.5f, 0.5f, 0.2f, 0.2f, 0.5f };
        float[] l4 = { 0.0f, 0.0f, 0.0f };
        float[] r4 = { 0.0f, 0.0f, 0.0f };
        FacialExpression disgusted = new FacialExpression("Disgusted", b4, w4, l4, r4, false);

        BlendShape[] b4X = { noseWrinkle, closeEyes, mouthUp, pucker, mouthOut, squint, frown, upperLipUp, browMad };
        float[] w4X = { 2f, 0.75f, 0.75f, 0.4f, 1f, 1f, 0.4f, 0.4f, 1f };
        float[] l4X = { 0.0f, 0.0f, 0.0f };
        float[] r4X = { 0.0f, 0.0f, 0.0f };
        FacialExpression disgustedX = new FacialExpression("DisgustedX", b4X, w4X, l4X, r4X, false);

        BlendShape[] b4Y = { noseWrinkle, closeEyes, mouthUp, pucker, mouthOut, squint, frown, upperLipUp, browMad };
        float[] w4Y = { 2.5f, 0.85f, 1f, 0.6f, 1.5f, 1.5f, 0.6f, 0.6f, 1.25f };
        float[] l4Y = { 0.0f, 0.0f, 0.0f };
        float[] r4Y = { 0.0f, 0.0f, 0.0f };
        FacialExpression disgustedY = new FacialExpression("DisgustedY", b4Y, w4Y, l4Y, r4Y, false);

        BlendShape[] b4Z = { noseWrinkle, closeEyes, mouthUp, pucker, mouthOut, squint, frown, upperLipUp, browMad };
        float[] w4Z = { 1.5f, 0.62f, 0.5f, 0.3f, 0.75f, 0.75f, 0.3f, 0.3f, 0.75f };
        float[] l4Z = { 0.0f, 0.0f, 0.0f };
        float[] r4Z = { 0.0f, 0.0f, 0.0f };
        FacialExpression disgustedZ = new FacialExpression("DisgustedZ", b4Z, w4Z, l4Z, r4Z, false);

        fExpr.Add(disgusted);
        fExpr.Add(disgustedX);
        fExpr.Add(disgustedY);
        fExpr.Add(disgustedZ);


        //Scared
        BlendShape[] b5 = { lowerLidUp, frown, jawDown, innerBrowUp, browSad, browUp };
        float[] w5 = { 0.25f, 0.5f, 0.25f, 0.2f, 0.5f, 0.25f };
        float[] l5 = { 0.0f, 0.0f, 0.0f };
        float[] r5 = { 0.0f, 0.0f, 0.0f };
        FacialExpression scared = new FacialExpression("Scared", b5, w5, l5, r5, false);

        BlendShape[] b5X = { lowerLidUp, frown, jawDown, innerBrowUp, browSad, browUp };
        float[] w5X = { 0.75f, 1f, 0.75f, 0.4f, 1f, 0.75f };
        float[] l5X = { 0.0f, 0.0f, 0.0f };
        float[] r5X = { 0.0f, 0.0f, 0.0f };
        FacialExpression scaredX = new FacialExpression("ScaredX", b5X, w5X, l5X, r5X, false);

        BlendShape[] b5Y = { lowerLidUp, frown, jawDown, innerBrowUp, browSad, browUp };
        float[] w5Y = { 1f, 1.5f, 1f, 0.5f, 1.5f, 1f };
        float[] l5Y = { 0.0f, 0.0f, 0.0f };
        float[] r5Y = { 0.0f, 0.0f, 0.0f };
        FacialExpression scaredY = new FacialExpression("ScaredY", b5Y, w5Y, l5Y, r5Y, false);

        BlendShape[] b5Z = { lowerLidUp, frown, jawDown, innerBrowUp, browSad, browUp };
        float[] w5Z = { 0.5f, 0.75f, 0.5f, 0.3f, 0.75f, 0.5f };
        float[] l5Z = { 0.0f, 0.0f, 0.0f };
        float[] r5Z = { 0.0f, 0.0f, 0.0f };
        FacialExpression scaredZ = new FacialExpression("ScaredZ", b5Z, w5Z, l5Z, r5Z, false);

        fExpr.Add(scared);
        fExpr.Add(scaredX);
        fExpr.Add(scaredY);
        fExpr.Add(scaredZ);


        //Surprised
        BlendShape[] b6 = { jawDown, browUp, mouthOut, pucker };
        float[] w6 = { 0.25f, 0.5f, 0.5f, 0.5f };
        float[] l6 = { 0.0f, 0.0f, 0.0f };
        float[] r6 = { 0.0f, 0.0f, 0.0f };
        FacialExpression surprised = new FacialExpression("Surprised", b6, w6, l6, r6, false);

        BlendShape[] b6X = { jawDown, browUp, mouthOut, pucker };
        float[] w6X = { 0.75f, 1f, 1f, 1f };
        float[] l6X = { 0.0f, 0.0f, 0.0f };
        float[] r6X = { 0.0f, 0.0f, 0.0f };
        FacialExpression surprisedX = new FacialExpression("SurprisedX", b6X, w6X, l6X, r6X, false);

        BlendShape[] b6Y = { jawDown, browUp, mouthOut, pucker };
        float[] w6Y = { 1f, 1.5f, 1.5f, 1.25f };
        float[] l6Y = { 0.0f, 0.0f, 0.0f };
        float[] r6Y = { 0.0f, 0.0f, 0.0f };
        FacialExpression surprisedY = new FacialExpression("SurprisedY", b6Y, w6Y, l6Y, r6Y, false);

        BlendShape[] b6Z = { jawDown, browUp, mouthOut, pucker };
        float[] w6Z = { 0.5f, 0.75f, 0.75f, 0.75f };
        float[] l6Z = { 0.0f, 0.0f, 0.0f };
        float[] r6Z = { 0.0f, 0.0f, 0.0f };
        FacialExpression surprisedZ = new FacialExpression("SurprisedZ", b6Z, w6Z, l6Z, r6Z, false);

        fExpr.Add(surprised);
        fExpr.Add(surprisedX);
        fExpr.Add(surprisedY);
        fExpr.Add(surprisedZ);
    }

    FacialExpression GetExpression(string name) {
        foreach (FacialExpression f in fExpr) {
            if (name == f.Name()) {
                return f;
            }
        }
        return null;
    }
}
