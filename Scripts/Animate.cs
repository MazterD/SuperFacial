using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class Animate : MonoBehaviour
{
    List<BlendShape> bShapes = new List<BlendShape>();    //list of blendshapes
    List<FacialExpression> fExpr = new List<FacialExpression>();  //list of expressions

    string currentExpr = "None";
    string nextExpr = "None";

    SkinnedMeshRenderer skinnedMeshRenderer;
    float blendSpeed = 1f;

    public GameObject upTeeth;
    public GameObject downTeeth;
    public GameObject leftEye;
    public GameObject rightEye;
    SkinnedMeshRenderer upTeethRenderer;
    SkinnedMeshRenderer downTeethRenderer;

    float startTime;
    private string filePath;
    private int user;

    void Awake()
    {
        skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
        upTeethRenderer = upTeeth.GetComponent<SkinnedMeshRenderer>();
        downTeethRenderer = downTeeth.GetComponent<SkinnedMeshRenderer>();

        ImportData();
    }

    // Start is called before the first frame update
    void Start()
    {
        FisherYatesShuffle(fExpr);
        filePath = Path.Combine(Application.persistentDataPath, "PilotResults.csv");
        InitializeCSVFile(filePath);
        DefineUser(filePath);
    }

    // Update is called once per frame
    void Update()
    {
        if (nextExpr == "None" && fExpr.Count > 1) {
            QueueExpression();
        }

        if (fExpr.Count > 0) {
            ExecuteExpression(currentExpr);
        }
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
            fExpr.Remove(expr);
        }
        else if (startTime == 0)
        {
            StartTimer(); //if expr reached its apex, it will stay until it reaches the max defined time
        }
    }

    void QueueExpression() {
        if (currentExpr == "None") {
            currentExpr = fExpr[0].Name();
        }

        if (nextExpr == "None")
        {
            nextExpr = fExpr[1].Name();
        }
    }

    static void FisherYatesShuffle<T>(List<T> list)
    {
        System.Random random = new System.Random();
        int n = list.Count;

        // Start from the end and swap elements with a random one
        for (int i = n - 1; i > 0; i--)
        {
            int j = random.Next(0, i + 1);
            T temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }
    }

    void ImportData()
    {
        //BlendShape creation
        BlendShape lipIn = new BlendShape("LipIn", 0);
        BlendShape lipOut = new BlendShape("LipOut", 1);
        BlendShape cheekPuff = new BlendShape("CheekPuff", 10);
        BlendShape pucker = new BlendShape("Pucker", 12);
        BlendShape browUp = new BlendShape("BrowUp", 13);
        BlendShape browDown = new BlendShape("BrowDown", 14);
        BlendShape browIn = new BlendShape("BrowIn", 15);
        BlendShape smile = new BlendShape("Smile", 30);
        BlendShape lowerLidUp = new BlendShape("LowerLidUp", 31);
        BlendShape outerBrowUp = new BlendShape("OuterBrowUp", 32);
        BlendShape innerBrowUp = new BlendShape("InnerBrowUp", 33);
        BlendShape upperLidDown = new BlendShape("UpperLidDown", 34);
        BlendShape frown = new BlendShape("Frown", 35);
        BlendShape mouthOpenV = new BlendShape("MouthOpenV", 36);
        BlendShape noseWrinkle = new BlendShape("NoseWrinkle", 37);
        BlendShape mouthOpenH = new BlendShape("MouthOpenH", 38);
        BlendShape lowerLidDown = new BlendShape("LowerLidDown", 39);
        BlendShape upperLidUp = new BlendShape("UpperLidUp", 40);

        //Adding BlendShapes to database
        bShapes.Add(lipIn);
        bShapes.Add(lipOut);
        bShapes.Add(cheekPuff);
        bShapes.Add(pucker);
        bShapes.Add(browUp);
        bShapes.Add(browDown);
        bShapes.Add(browIn);
        bShapes.Add(smile);
        bShapes.Add(lowerLidUp);
        bShapes.Add(outerBrowUp);
        bShapes.Add(innerBrowUp);
        bShapes.Add(upperLidDown);
        bShapes.Add(frown);
        bShapes.Add(mouthOpenV);
        bShapes.Add(noseWrinkle);
        bShapes.Add(mouthOpenH);
        bShapes.Add(lowerLidDown);
        bShapes.Add(upperLidUp);

        //Facial expressions - regular, X(distortion level 2), Y(distortion level 3) and Z(distortion level 1)

        //Happy
        BlendShape[] b = { smile, lowerLidUp };
        float[] w = { 1f, 1f };
        float[] l = { 0.0f, 0.0f, 0.0f };
        float[] r = { 0.0f, 0.0f, 0.0f };
        FacialExpression happy = new FacialExpression("Happy", b, w, l, r, false);

        BlendShape[] bX = { smile, lowerLidUp };
        float[] wX = { 2f, 2f };
        float[] lX = { 0.0f, 0.0f, 0.0f };
        float[] rX = { 0.0f, 0.0f, 0.0f };
        FacialExpression happyX = new FacialExpression("HappyX", bX, wX, lX, rX, false);

        BlendShape[] bY = { smile, lowerLidUp, upperLidDown };  //needs eyelid tearing correction
        float[] wY = { 2f, 3f, 1f };
        float[] lY = { 0.0f, 0.0f, 0.0f };
        float[] rY = { 0.0f, 0.0f, 0.0f };
        FacialExpression happyY = new FacialExpression("HappyY", bY, wY, lY, rY, false);

        BlendShape[] bZ = { smile, lowerLidUp };
        float[] wZ = { 1.5f, 1.5f };
        float[] lZ = { 0.0f, 0.0f, 0.0f };
        float[] rZ = { 0.0f, 0.0f, 0.0f };
        FacialExpression happyZ = new FacialExpression("HappyZ", bZ, wZ, lZ, rZ, false);

        fExpr.Add(happy);
        fExpr.Add(happyX);
        fExpr.Add(happyY);
        fExpr.Add(happyZ);


        //Sad
        BlendShape[] b2 = { frown, upperLidDown, innerBrowUp, browDown };
        float[] w2 = { 1f, 0.5f, 1f, 0.33f };
        float[] l2 = { 0.04f, 0.0f, 0.0f };
        float[] r2 = { 0.04f, 0.0f, 0.0f };
        FacialExpression sad = new FacialExpression("Sad", b2, w2, l2, r2, false);

        BlendShape[] b2X = { frown, upperLidDown, innerBrowUp, browDown };
        float[] w2X = { 1.5f, 1f, 2f, 0.66f };
        float[] l2X = { 0.04f, 0.0f, 0.0f };
        float[] r2X = { 0.04f, 0.0f, 0.0f };
        FacialExpression sadX = new FacialExpression("SadX", b2X, w2X, l2X, r2X, false);

        BlendShape[] b2Y = { frown, upperLidDown, innerBrowUp, browDown, lowerLidUp };
        float[] w2Y = { 2f, 1.5f, 2.5f, 1f, 1f };
        float[] l2Y = { 0.04f, 0.0f, 0.0f };
        float[] r2Y = { 0.04f, 0.0f, 0.0f };
        FacialExpression sadY = new FacialExpression("SadY", b2Y, w2Y, l2Y, r2Y, false);

        BlendShape[] b2Z = { frown, upperLidDown, innerBrowUp, browDown };
        float[] w2Z = { 1.25f, 0.75f, 1.5f, 0.48f };
        float[] l2Z = { 0.04f, 0.0f, 0.0f };
        float[] r2Z = { 0.04f, 0.0f, 0.0f };
        FacialExpression sadZ = new FacialExpression("SadZ", b2Z, w2Z, l2Z, r2Z, false);

        fExpr.Add(sad);
        fExpr.Add(sadX);
        fExpr.Add(sadY);
        fExpr.Add(sadZ);


        //Angry
        BlendShape[] b3 = { frown, outerBrowUp, browDown, browIn, mouthOpenH };
        float[] w3 = { 1f, 1f, 0.5f, 0.5f, 0.5f };
        float[] l3 = { 0.0f, 0.0f, 0.0f };
        float[] r3 = { 0.0f, 0.0f, 0.0f };
        FacialExpression angry = new FacialExpression("Angry", b3, w3, l3, r3, false);

        BlendShape[] b3X = { frown, outerBrowUp, browDown, browIn, mouthOpenH };
        float[] w3X = { 1.5f, 2f, 1f, 1f, 1f };
        float[] l3X = { 0.0f, 0.0f, 0.0f };
        float[] r3X = { 0.0f, 0.0f, 0.0f };
        FacialExpression angryX = new FacialExpression("AngryX", b3X, w3X, l3X, r3X, false);

        BlendShape[] b3Y = { frown, outerBrowUp, browDown, browIn, mouthOpenH };
        float[] w3Y = { 2f, 2.5f, 1.5f, 1.5f, 1.5f };
        float[] l3Y = { 0.0f, 0.0f, 0.0f };
        float[] r3Y = { 0.0f, 0.0f, 0.0f };
        FacialExpression angryY = new FacialExpression("AngryY", b3Y, w3Y, l3Y, r3Y, false);

        BlendShape[] b3Z = { frown, outerBrowUp, browDown, browIn, mouthOpenH };
        float[] w3Z = { 1.25f, 1.5f, 0.75f, 0.75f, 0.75f };
        float[] l3Z = { 0.0f, 0.0f, 0.0f };
        float[] r3Z = { 0.0f, 0.0f, 0.0f };
        FacialExpression angryZ = new FacialExpression("AngryZ", b3Z, w3Z, l3Z, r3Z, false);

        fExpr.Add(angry);
        fExpr.Add(angryX);
        fExpr.Add(angryY);
        fExpr.Add(angryZ);


        //Disgusted
        BlendShape[] b4 = { noseWrinkle, upperLidDown, lowerLidUp, mouthOpenH, browDown, frown, browIn };
        float[] w4 = { 1f, 0.5f, 1f, 0.5f, 0.5f, 0.5f, 0.5f };
        float[] l4 = { 0.0f, 0.0f, 0.0f };
        float[] r4 = { 0.0f, 0.0f, 0.0f };
        FacialExpression disgusted = new FacialExpression("Disgusted", b4, w4, l4, r4, false);

        BlendShape[] b4X = { noseWrinkle, upperLidDown, lowerLidUp, mouthOpenH, browDown, frown, browIn };
        float[] w4X = { 2f, 1f, 2f, 1f, 1f, 1f, 1f };
        float[] l4X = { 0.0f, 0.0f, 0.0f };
        float[] r4X = { 0.0f, 0.0f, 0.0f };
        FacialExpression disgustedX = new FacialExpression("DisgustedX", b4X, w4X, l4X, r4X, false);

        BlendShape[] b4Y = { noseWrinkle, upperLidDown, lowerLidUp, mouthOpenH, browDown, frown, browIn };
        float[] w4Y = { 2.5f, 1.5f, 2.5f, 1.5f, 1.5f, 1.5f, 1.5f };
        float[] l4Y = { 0.0f, 0.0f, 0.0f };
        float[] r4Y = { 0.0f, 0.0f, 0.0f };
        FacialExpression disgustedY = new FacialExpression("DisgustedY", b4Y, w4Y, l4Y, r4Y, false);

        BlendShape[] b4Z = { noseWrinkle, upperLidDown, lowerLidUp, mouthOpenH, browDown, frown, browIn };
        float[] w4Z = { 1.5f, 0.75f, 1.5f, 0.75f, 0.75f, 0.75f, 0.75f };
        float[] l4Z = { 0.0f, 0.0f, 0.0f };
        float[] r4Z = { 0.0f, 0.0f, 0.0f };
        FacialExpression disgustedZ = new FacialExpression("DisgustedZ", b4Z, w4Z, l4Z, r4Z, false);

        fExpr.Add(disgusted);
        fExpr.Add(disgustedX);
        fExpr.Add(disgustedY);
        fExpr.Add(disgustedZ);


        //Scared
        BlendShape[] b5 = { lowerLidUp, frown, mouthOpenH, mouthOpenV, innerBrowUp, browIn, browUp, browDown };
        float[] w5 = { 1f, 0.5f, 0.5f, 0.5f, 0.5f, 0.5f, 0.5f, 0.2f };
        float[] l5 = { 0.0f, 0.0f, 0.0f };
        float[] r5 = { 0.0f, 0.0f, 0.0f };
        FacialExpression scared = new FacialExpression("Scared", b5, w5, l5, r5, true);

        BlendShape[] b5X = { lowerLidUp, frown, mouthOpenH, mouthOpenV, innerBrowUp, browIn, browUp, browDown };
        float[] w5X = { 2f, 1f, 1f, 1f, 1f, 1f, 1f, 0.4f };
        float[] l5X = { 0.0f, 0.0f, 0.0f };
        float[] r5X = { 0.0f, 0.0f, 0.0f };
        FacialExpression scaredX = new FacialExpression("ScaredX", b5X, w5X, l5X, r5X, true);

        BlendShape[] b5Y = { lowerLidUp, frown, mouthOpenH, mouthOpenV, innerBrowUp, browIn, browUp, browDown };
        float[] w5Y = { 2.5f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 0.6f };
        float[] l5Y = { 0.0f, 0.0f, 0.0f };
        float[] r5Y = { 0.0f, 0.0f, 0.0f };
        FacialExpression scaredY = new FacialExpression("ScaredY", b5Y, w5Y, l5Y, r5Y, true);

        BlendShape[] b5Z = { lowerLidUp, frown, mouthOpenH, mouthOpenV, innerBrowUp, browIn, browUp, browDown };
        float[] w5Z = { 1.5f, 0.75f, 0.75f, 0.75f, 0.75f, 0.75f, 0.75f, 0.3f };
        float[] l5Z = { 0.0f, 0.0f, 0.0f };
        float[] r5Z = { 0.0f, 0.0f, 0.0f };
        FacialExpression scaredZ = new FacialExpression("ScaredZ", b5Z, w5Z, l5Z, r5Z, true);

        fExpr.Add(scared);
        fExpr.Add(scaredX);
        fExpr.Add(scaredY);
        fExpr.Add(scaredZ);


        //Surprised
        BlendShape[] b6 = { mouthOpenV, browUp, lipOut, lowerLidDown, upperLidUp };
        float[] w6 = { 0.5f, 0.5f, 0.5f, 1f, 1f };
        float[] l6 = { 0.0f, 0.0f, 0.0f };
        float[] r6 = { 0.0f, 0.0f, 0.0f };
        FacialExpression surprised = new FacialExpression("Surprised", b6, w6, l6, r6, true);

        BlendShape[] b6X = { mouthOpenV, browUp, lipOut, lowerLidDown, upperLidUp };
        float[] w6X = { 1f, 1f, 1f, 2f, 2f };
        float[] l6X = { 0.0f, 0.0f, 0.0f };
        float[] r6X = { 0.0f, 0.0f, 0.0f };
        FacialExpression surprisedX = new FacialExpression("SurprisedX", b6X, w6X, l6X, r6X, true);

        BlendShape[] b6Y = { mouthOpenV, browUp, lipOut, lowerLidDown, upperLidUp };
        float[] w6Y = { 1.5f, 1.5f, 1.5f, 2.5f, 2.5f };
        float[] l6Y = { 0.0f, 0.0f, 0.0f };
        float[] r6Y = { 0.0f, 0.0f, 0.0f };
        FacialExpression surprisedY = new FacialExpression("SurprisedY", b6Y, w6Y, l6Y, r6Y, true);

        BlendShape[] b6Z = { mouthOpenV, browUp, lipOut, lowerLidDown, upperLidUp };
        float[] w6Z = { 0.75f, 0.75f, 0.75f, 1.5f, 1.5f };
        float[] l6Z = { 0.0f, 0.0f, 0.0f };
        float[] r6Z = { 0.0f, 0.0f, 0.0f };
        FacialExpression surprisedZ = new FacialExpression("SurprisedZ", b6Z, w6Z, l6Z, r6Z, true);

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

    public void IsExpression(string name) {
        if (currentExpr == "None" || startTime == 0) {
            return;
        }
        else if (currentExpr == name || currentExpr.Substring(0, currentExpr.Length - 1) == name)
        {
            Debug.Log("Match");
        }
        else {
            Debug.Log("No match");
        }

        Debug.Log("Expression: " + currentExpr + " | Answer: " + name);
        string time = StopTimer();
        WriteToCSV(filePath, currentExpr, name, time);

        FacialExpression expr = GetExpression(currentExpr);
        expr.PassTime(expr.GetDuration());
    }

    public void StartTimer() {
        startTime = Time.time;
    }

    public string StopTimer() {
        float endTime = Time.time - startTime;
        Debug.Log("Time taken: " + endTime.ToString() + " seconds");
        startTime = 0;
        return endTime.ToString();
    }


    #region Exporter Stuff

    private void InitializeCSVFile(string path)
    {
        if (!File.Exists(path))
        {
            using (StreamWriter sw = new StreamWriter(path))
            {
                sw.WriteLine("User:Avatar:Target:Choice:Time");
            }
        }
    }

    private void WriteToCSV(string path, string target, string choice, string time)
    {
        try
        {
            using (StreamWriter sw = new StreamWriter(path, true))
            {
                sw.WriteLine($"{user}:M:{target}:{choice}:{time}");
            }
        }
        catch (IOException e)
        {
            Debug.LogError($"Failed to write to CSV file: {e.Message}");
        }
    }

    private void DefineUser(string path) {
        try {
            string line = File.ReadLines(path).Last();
            if (!line.Contains("User"))
            {
                string[] newline = line.Split(':');
                user = (int.Parse(newline[0])) + 1;
            }
            else {
                user = 1;
            }
        }
        catch (IOException e)
        {
            Debug.LogError($"Failed to write to CSV file: {e.Message}");
        }
    }

    #endregion
}
