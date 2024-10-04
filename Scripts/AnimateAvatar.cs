using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateAvatar : MonoBehaviour
{
    Dictionary<string, int> bShapes = new Dictionary<string, int>();    //list of blendshapes
    Dictionary<string, float> exprActive = new Dictionary<string, float>();  //active time
    Dictionary<string, float> exprState = new Dictionary<string, float>();  //inbetweening state

    string currentExpr = "None";
    string nextExpr = "None";
    
    int blendShapeCount;
    SkinnedMeshRenderer skinnedMeshRenderer;
    float blendSpeed = 0.5f;
    float blendFreeze = 600f;

    public GameObject upTeeth;
    public GameObject downTeeth;
    SkinnedMeshRenderer upTeethRenderer;
    SkinnedMeshRenderer downTeethRenderer;

    void Awake()
    {
        skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
        upTeethRenderer = upTeeth.GetComponent<SkinnedMeshRenderer>();
        downTeethRenderer = downTeeth.GetComponent<SkinnedMeshRenderer>();

        bShapes.Add("LipIn", 0);
        bShapes.Add("LipOut", 1);
        bShapes.Add("CheekPuff", 10);
        bShapes.Add("Pucker", 12);
        bShapes.Add("BrowUp", 13);
        bShapes.Add("BrowDown", 14);
        bShapes.Add("BrowIn", 15);
        bShapes.Add("Smile", 30);
        bShapes.Add("LowerLidUp", 31);
        bShapes.Add("OuterBrowUp", 32);
        bShapes.Add("InnerBrowUp", 33);
        bShapes.Add("UpperLidDown", 34);
        bShapes.Add("Frown", 35);
        bShapes.Add("MouthOpenV", 36);
        bShapes.Add("NoseWrinkle", 37);
        bShapes.Add("MouthOpenH", 38);

        exprActive.Add("Happy", 0f);
        exprActive.Add("Sad", 0f);
        exprActive.Add("Angry", 0f);
        exprActive.Add("Disgusted", 0f);
        exprActive.Add("Scared", 0f);
        exprActive.Add("Surprised", 0f);

        exprState.Add("Happy", 0f);
        exprState.Add("Sad", 0f);
        exprState.Add("Angry", 0f);
        exprState.Add("Disgusted", 0f);
        exprState.Add("Scared", 0f);
        exprState.Add("Surprised", 0f);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        InputRead();
               
        if (currentExpr == "Happy")
        {
            Happy();
        }
        else if (currentExpr == "Sad") {
            Sad();
        }
        else if (currentExpr == "Angry")
        {
            Angry();
        }
        else if (currentExpr == "Disgusted")
        {
            Disgusted();
        }
        else if (currentExpr == "Scared")
        {
            Scared();
        }
        else if (currentExpr == "Surprised")
        {
            Surprised();
        }
    }

    void Happy() {
        if (exprState["Happy"] < 100f && exprActive["Happy"] == 0f)  //if happy is not active yet it will increase
        {
            skinnedMeshRenderer.SetBlendShapeWeight(bShapes["Smile"], exprState["Happy"]);
            skinnedMeshRenderer.SetBlendShapeWeight(bShapes["LowerLidUp"], exprState["Happy"]);
            exprState["Happy"] += blendSpeed;
        }
        else if (exprState["Happy"] > 0f && exprActive["Happy"] == blendFreeze)    //if happy has reached its full duration it will decrease
        {
            skinnedMeshRenderer.SetBlendShapeWeight(bShapes["Smile"], exprState["Happy"]);
            skinnedMeshRenderer.SetBlendShapeWeight(bShapes["LowerLidUp"], exprState["Happy"]);
            exprState["Happy"] -= blendSpeed;
        }
        else if (exprState["Happy"] == 0f && exprActive["Happy"] == blendFreeze)   //if happy reached 0 it stops the animation
        {
            exprActive["Happy"] = 0f;
            //if there is another expression in queue, it will be given the signal to start
            currentExpr = nextExpr;
            nextExpr = "None";
        }
        else
        {   //if happy reached its apex, it will be considered as active
            exprActive["Happy"] += blendSpeed;
        }
    }

    void Sad()
    {
        if (exprState["Sad"] < 100f && exprActive["Sad"] == 0f)  //if Sad is not active yet it will increase
        {
            skinnedMeshRenderer.SetBlendShapeWeight(bShapes["Frown"], exprState["Sad"]);
            skinnedMeshRenderer.SetBlendShapeWeight(bShapes["UpperLidDown"], exprState["Sad"]/2);
            skinnedMeshRenderer.SetBlendShapeWeight(bShapes["InnerBrowUp"], exprState["Sad"]);
            skinnedMeshRenderer.SetBlendShapeWeight(bShapes["BrowDown"], exprState["Sad"]/3);
            exprState["Sad"] += blendSpeed;
        }
        else if (exprState["Sad"] > 0f && exprActive["Sad"] == blendFreeze)    //if Sad has reached its peak it will decrease
        {
            skinnedMeshRenderer.SetBlendShapeWeight(bShapes["Frown"], exprState["Sad"]);
            skinnedMeshRenderer.SetBlendShapeWeight(bShapes["UpperLidDown"], exprState["Sad"]/2);
            skinnedMeshRenderer.SetBlendShapeWeight(bShapes["InnerBrowUp"], exprState["Sad"]);
            skinnedMeshRenderer.SetBlendShapeWeight(bShapes["BrowDown"], exprState["Sad"]/3);
            exprState["Sad"] -= blendSpeed;
        }
        else if (exprState["Sad"] == 0f && exprActive["Sad"] == blendFreeze)   //if Sad reached 0 it stops the animation
        {
            exprActive["Sad"] = 0f;
            //if there is another expression in queue, it will be given the signal to start
            currentExpr = nextExpr;
            nextExpr = "None";
        }
        else
        {   //if Sad reached its apex, it will be considered as active
            exprActive["Sad"] += blendSpeed;
        }
    }

    void Angry()
    {
        if (exprState["Angry"] < 100f && exprActive["Angry"] == 0f)  //if Angry is not active yet it will increase
        {
            skinnedMeshRenderer.SetBlendShapeWeight(bShapes["Frown"], exprState["Angry"]);
            skinnedMeshRenderer.SetBlendShapeWeight(bShapes["OuterBrowUp"], exprState["Angry"]);
            skinnedMeshRenderer.SetBlendShapeWeight(bShapes["BrowDown"], exprState["Angry"]/2);
            skinnedMeshRenderer.SetBlendShapeWeight(bShapes["BrowIn"], exprState["Angry"]/2);
            skinnedMeshRenderer.SetBlendShapeWeight(bShapes["MouthOpenH"], exprState["Angry"]/2);
            exprState["Angry"] += blendSpeed;
        }
        else if (exprState["Angry"] > 0f && exprActive["Angry"] == blendFreeze)    //if Angry has reached its full duration it will decrease
        {
            skinnedMeshRenderer.SetBlendShapeWeight(bShapes["Frown"], exprState["Angry"]);
            skinnedMeshRenderer.SetBlendShapeWeight(bShapes["OuterBrowUp"], exprState["Angry"]);
            skinnedMeshRenderer.SetBlendShapeWeight(bShapes["BrowDown"], exprState["Angry"]/2);
            skinnedMeshRenderer.SetBlendShapeWeight(bShapes["BrowIn"], exprState["Angry"]/2);
            skinnedMeshRenderer.SetBlendShapeWeight(bShapes["MouthOpenH"], exprState["Angry"]/2);
            exprState["Angry"] -= blendSpeed;
        }
        else if (exprState["Angry"] == 0f && exprActive["Angry"] == blendFreeze)   //if Angry reached 0 it stops the animation
        {
            exprActive["Angry"] = 0f;
            //if there is another expression in queue, it will be given the signal to start
            currentExpr = nextExpr;
            nextExpr = "None";
        }
        else
        {   //if Angry reached its apex, it will be considered as active
            exprActive["Angry"] += blendSpeed;
        }
    }

    void Disgusted()
    {
        if (exprState["Disgusted"] < 100f && exprActive["Disgusted"] == 0f)  //if Disgusted is not active yet it will increase
        {
            skinnedMeshRenderer.SetBlendShapeWeight(bShapes["NoseWrinkle"], exprState["Disgusted"]);
            skinnedMeshRenderer.SetBlendShapeWeight(bShapes["UpperLidDown"], exprState["Disgusted"] / 2);
            skinnedMeshRenderer.SetBlendShapeWeight(bShapes["LowerLidUp"], exprState["Disgusted"]);
            skinnedMeshRenderer.SetBlendShapeWeight(bShapes["MouthOpenH"], exprState["Disgusted"] / 2);
            skinnedMeshRenderer.SetBlendShapeWeight(bShapes["BrowDown"], exprState["Disgusted"] / 2);
            exprState["Disgusted"] += blendSpeed;
        }
        else if (exprState["Disgusted"] > 0f && exprActive["Disgusted"] == blendFreeze)    //if Disgusted has reached its full duration it will decrease
        {
            skinnedMeshRenderer.SetBlendShapeWeight(bShapes["NoseWrinkle"], exprState["Disgusted"]);
            skinnedMeshRenderer.SetBlendShapeWeight(bShapes["UpperLidDown"], exprState["Disgusted"] / 2);
            skinnedMeshRenderer.SetBlendShapeWeight(bShapes["LowerLidUp"], exprState["Disgusted"]);
            skinnedMeshRenderer.SetBlendShapeWeight(bShapes["MouthOpenH"], exprState["Disgusted"] / 2);
            skinnedMeshRenderer.SetBlendShapeWeight(bShapes["BrowDown"], exprState["Disgusted"] / 2);
            exprState["Disgusted"] -= blendSpeed;
        }
        else if (exprState["Disgusted"] == 0f && exprActive["Disgusted"] == blendFreeze)   //if Disgusted reached 0 it stops the animation
        {
            exprActive["Disgusted"] = 0f;
            //if there is another expression in queue, it will be given the signal to start
            currentExpr = nextExpr;
            nextExpr = "None";
        }
        else
        {   //if Disgusted reached its apex, it will be considered as active
            exprActive["Disgusted"] += blendSpeed;
        }
    }

    void Scared()
    {
        if (exprState["Scared"] < 100f && exprActive["Scared"] == 0f)  //if Scared is not active yet it will increase
        {           
            skinnedMeshRenderer.SetBlendShapeWeight(bShapes["LowerLidUp"], exprState["Scared"]);
            skinnedMeshRenderer.SetBlendShapeWeight(bShapes["Frown"], exprState["Scared"] / 2);
            skinnedMeshRenderer.SetBlendShapeWeight(bShapes["MouthOpenH"], exprState["Scared"] / 2);
            skinnedMeshRenderer.SetBlendShapeWeight(bShapes["MouthOpenV"], exprState["Scared"] / 2);
            skinnedMeshRenderer.SetBlendShapeWeight(bShapes["InnerBrowUp"], exprState["Scared"] / 2);
            skinnedMeshRenderer.SetBlendShapeWeight(bShapes["BrowIn"], exprState["Scared"] / 2);
            skinnedMeshRenderer.SetBlendShapeWeight(bShapes["BrowUp"], exprState["Scared"] / 2);
            skinnedMeshRenderer.SetBlendShapeWeight(bShapes["BrowDown"], exprState["Scared"] / 5);
            upTeethRenderer.SetBlendShapeWeight(0, exprState["Scared"]);
            downTeethRenderer.SetBlendShapeWeight(0, exprState["Scared"]);
            exprState["Scared"] += blendSpeed;
        }
        else if (exprState["Scared"] > 0f && exprActive["Scared"] == blendFreeze)    //if Scared has reached its full duration it will decrease
        {
            skinnedMeshRenderer.SetBlendShapeWeight(bShapes["BrowIn"], exprState["Scared"] / 2);
            skinnedMeshRenderer.SetBlendShapeWeight(bShapes["LowerLidUp"], exprState["Scared"]);
            skinnedMeshRenderer.SetBlendShapeWeight(bShapes["MouthOpenH"], exprState["Scared"] / 2);
            skinnedMeshRenderer.SetBlendShapeWeight(bShapes["MouthOpenV"], exprState["Scared"] / 2);
            skinnedMeshRenderer.SetBlendShapeWeight(bShapes["InnerBrowUp"], exprState["Scared"] / 2);
            skinnedMeshRenderer.SetBlendShapeWeight(bShapes["BrowUp"], exprState["Scared"] / 2);
            skinnedMeshRenderer.SetBlendShapeWeight(bShapes["BrowDown"], exprState["Scared"] / 5);
            upTeethRenderer.SetBlendShapeWeight(0, exprState["Scared"]);
            downTeethRenderer.SetBlendShapeWeight(0, exprState["Scared"]);
            exprState["Scared"] -= blendSpeed;
        }
        else if (exprState["Scared"] == 0f && exprActive["Scared"] == blendFreeze)   //if Scared reached 0 it stops the animation
        {
            exprActive["Scared"] = 0f;
            //if there is another expression in queue, it will be given the signal to start
            currentExpr = nextExpr;
            nextExpr = "None";
        }
        else
        {   //if Scared reached its apex, it will be considered as active
            exprActive["Scared"] += blendSpeed;
        }
    }

    void Surprised()
    {
        if (exprState["Surprised"] < 100f && exprActive["Surprised"] == 0f)  //if Surprised is not active yet it will increase
        {
            skinnedMeshRenderer.SetBlendShapeWeight(bShapes["MouthOpenV"], exprState["Surprised"] / 2);
            skinnedMeshRenderer.SetBlendShapeWeight(bShapes["BrowUp"], exprState["Surprised"] / 2);
            skinnedMeshRenderer.SetBlendShapeWeight(bShapes["LipOut"], exprState["Surprised"] / 2);
            upTeethRenderer.SetBlendShapeWeight(0, exprState["Surprised"]);
            downTeethRenderer.SetBlendShapeWeight(0, exprState["Surprised"]);
            exprState["Surprised"] += blendSpeed;
        }
        else if (exprState["Surprised"] > 0f && exprActive["Surprised"] == blendFreeze)    //if Surprised has reached its full duration it will decrease
        {
            skinnedMeshRenderer.SetBlendShapeWeight(bShapes["MouthOpenV"], exprState["Surprised"] / 2);
            skinnedMeshRenderer.SetBlendShapeWeight(bShapes["BrowUp"], exprState["Surprised"] / 2);
            skinnedMeshRenderer.SetBlendShapeWeight(bShapes["LipOut"], exprState["Surprised"] / 2);
            upTeethRenderer.SetBlendShapeWeight(0, exprState["Surprised"]);
            downTeethRenderer.SetBlendShapeWeight(0, exprState["Surprised"]);
            exprState["Surprised"] -= blendSpeed;
        }
        else if (exprState["Surprised"] == 0f && exprActive["Surprised"] == blendFreeze)   //if Surprised reached 0 it stops the animation
        {
            exprActive["Surprised"] = 0f;
            //if there is another expression in queue, it will be given the signal to start
            currentExpr = nextExpr;
            nextExpr = "None";
        }
        else
        {   //if Surprised reached its apex, it will be considered as active
            exprActive["Surprised"] += blendSpeed;
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
    }
}
