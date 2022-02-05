using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;


using PDollarGestureRecognizer;

public class fieldMagicInput : MonoBehaviour
{
    #region skills

    public GameObject Skill;

    #endregion




    public GameObject normalMode;
    public GameObject magicMode;


    private bool isDraw = false;


    private float magicCount = 2f;


    Collider m_Collider;

    public Transform gestureOnScreenPrefab;

    private List<Gesture> trainingSet = new List<Gesture>();

    private List<Point> points = new List<Point>();
    private int strokeId = -1;

    private Vector3 virtualKeyPosition = Vector2.zero;
    private Rect drawArea;

    private RuntimePlatform platform;
    private int vertexCount = 0;

    private List<LineRenderer> gestureLinesRenderer = new List<LineRenderer>();
    private LineRenderer currentGestureLineRenderer;
    private Rect rect;

    private CanvasScaler canvasScaler;

    public Text message;
    private string messageString;

    

    private bool recognized;

    // Start is called before the first frame update
    void Start()
    {
        
        TextAsset[] gesturesXml = Resources.LoadAll<TextAsset>("GestureSet/10-stylus-MEDIUM/");
        foreach (TextAsset gestureXml in gesturesXml)
            trainingSet.Add(GestureIO.ReadGestureFromXML(gestureXml.text));

        rect = GetComponent<RectTransform>().rect;
        canvasScaler = GameObject.Find("Canvas").gameObject.GetComponent<CanvasScaler>();

        

        /*Load user custom gestures
        string[] filePaths = Directory.GetFiles(Application.persistentDataPath, "*.xml");
        foreach (string filePath in filePaths)
            trainingSet.Add(GestureIO.ReadGestureFromFile(filePath));
        
        string[] filePaths = Directory.GetFiles(Application.dataPath + "/MagicRune/XML_Files/", " *.xml");
        Debug.Log(Application.dataPath + "/MagicRune/XML_Files");
        if (filePaths.Length > 0)
        {
            Debug.Log(filePaths[0]);
        }
        
        foreach (string filePath in filePaths)
        {
            trainingSet.Add(GestureIO.ReadGestureFromFile(filePath));
            
        }
        */


    }
    
    // Update is called once per frame
    void Update()
    {
        if (platform == RuntimePlatform.Android || platform == RuntimePlatform.IPhonePlayer)
        {
            if (Input.touchCount > 0)
            {
                virtualKeyPosition = new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y);
                isDraw = true;
            }
        }
        else
        {
            if (Input.GetMouseButton(0))
            {
                virtualKeyPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y);
                isDraw = true;
            }
        }

        
        

        if (IsPointInRT(new Vector2(virtualKeyPosition.x, virtualKeyPosition.y), GetComponent<RectTransform>()))
        {

        

            if (Input.GetMouseButtonDown(0))
            {

            

                ++strokeId;

                Transform tmpGesture = Instantiate(gestureOnScreenPrefab, transform.position, transform.rotation) as Transform;
                currentGestureLineRenderer = tmpGesture.GetComponent<LineRenderer>();

                gestureLinesRenderer.Add(currentGestureLineRenderer);

                vertexCount = 0;
            }

            if (Input.GetMouseButton(0))
            {
                points.Add(new Point(virtualKeyPosition.x, -virtualKeyPosition.y, strokeId));

                currentGestureLineRenderer.SetVertexCount(++vertexCount);
                currentGestureLineRenderer.SetPosition(vertexCount - 1, Camera.main.ScreenToWorldPoint(new Vector3(virtualKeyPosition.x, virtualKeyPosition.y, 10)));
            }

            else
            {
                if (isDraw && magicCount > 0)
                {

                    isDraw = false;
                    recognaize();
                    clearField();
                    magicCount--;

                    if (magicCount == 0)
                    {
                        isDraw = false;

                        message.text = messageString;
                        magicCount = 2;
                        makeMagic();
                        messageString = "";
                        turnOnNormalMode();
                        
                    }

                }

                
            }

            
        }

        

    }

    private void makeMagic()
    {
        GameObject skil;
        switch (messageString)
        {

            

            case "from above from above ":
                skil = Instantiate(Skill, new Vector3(1, 2, 1), new Quaternion(0, 0, 0, 0));

                if (skil != null)
                    Destroy(skil, Skill.GetComponent<SkillsControlls>().timer);
                break;
            case "From Botton From Botton ":
                skil = Instantiate(Skill, new Vector3(1, 5, 1), new Quaternion(0, 0, 180, 0));
                
                if (skil != null)
                    Destroy(skil, Skill.GetComponent<SkillsControlls>().timer);
                break;

        }

        if (messageString.Equals("from above from above "))
        {
            
        }

        
    }

    public void clearField()
    {
        recognized = true;

        if (recognized)
        {

            recognized = false;
            strokeId = -1;

            points.Clear();

            foreach (LineRenderer lineRenderer in gestureLinesRenderer)
            {

                lineRenderer.SetVertexCount(0);
                Destroy(lineRenderer.gameObject);
            }

            gestureLinesRenderer.Clear();
        }

    }

    public void recognaize()
    {
        Gesture candidate = new Gesture(points.ToArray());
        Result gestureResult = PointCloudRecognizer.Classify(candidate, trainingSet.ToArray());

        messageString += gestureResult.GestureClass + " ";
        //message.text = gestureResult.GestureClass + " " + gestureResult.Score;
    }

    bool IsPointInRT(Vector2 point, RectTransform rt)
    {
        // Get the rectangular bounding box of your UI element
        Rect rect = rt.rect;

        // Get the left, right, top, and bottom boundaries of the rect
        /*
        float leftSide = rt.anchoredPosition.x - rect.width / 2;
        float rightSide = rt.anchoredPosition.x + rect.width / 2;
        float topSide = rt.anchoredPosition.y + rect.height / 2;
        float bottomSide = rt.anchoredPosition.y - rect.height / 2;
        */
        
        float rectCenterX = (canvasScaler.referenceResolution.x / 2) + rt.anchoredPosition.x;
        float rectCenterY = (canvasScaler.referenceResolution.y / 2) + rt.anchoredPosition.y;

        float leftSide = rectCenterX - rect.width / 2;
        float rightSide = rectCenterX + rect.width / 2;
        float topSide = rectCenterY + rect.height / 2;
        float bottomSide = rectCenterY - rect.height / 2;

        //Debug.Log(leftSide + ", " + rightSide + ", " + topSide + ", " + bottomSide);

        /*

        Debug.Log("Delta - " + rt.sizeDelta.x + "------------" + rt.sizeDelta.y);
        Debug.Log("Anchored - " + rt.anchoredPosition.x + "-------------------" + rt.anchoredPosition.y);


        Debug.Log("rect width - " + rect.width + "  ||||  rect height - " + rect.height);

        Debug.Log("d x - " + canvasScaler.referenceResolution.x / 2 + "  ||||  d y - " + canvasScaler.referenceResolution.y / 2);

        float rectCenterX = (canvasScaler.referenceResolution.x / 2) + rt.anchoredPosition.x;
        float rectCenterY = (canvasScaler.referenceResolution.y / 2) + rt.anchoredPosition.y;

        Debug.Log("Anchored - " + rt.anchoredPosition.x + "-------------------" + rt.anchoredPosition.y);

        Debug.Log("Rect center - " + rectCenterX + "-------------------" + rectCenterY);*/

        //Debug.Log("Screen size x - " + Screen.width / 2 + " -------- y - " + Screen.height / 2);

        //float width = rt.sizeDelta.x * rt.localScale.x;
        //float height = rt.sizeDelta.y * rt.localScale.y;

        //Debug.Log("width - " + width + "----------------" + "height - " + height);


        // Transform screen cordinate to canvas

        float pointXProc = point.x * 100f / Screen.width;
        float pointX = pointXProc / 100f * canvasScaler.referenceResolution.x;

        float pointYProc = point.y * 100f / Screen.height;
        float pointY = pointYProc / 100f * canvasScaler.referenceResolution.y;

        //Debug.Log("x - " + pointX + " ------- " + "y - " + pointY);

        // Check to see if the point is in the calculated bounds
        if (pointX >= leftSide &&
            pointX <= rightSide &&
            pointY >= bottomSide &&
            pointY <= topSide)
        {
            return true;
        }
        return false;
    }



    public void turnOnMagicMode()
    {
        normalMode.SetActive(false);
        magicMode.SetActive(true);
    }

    public void turnOnNormalMode()
    {
        normalMode.SetActive(true);
        magicMode.SetActive(false);
        clearField();
    }


}
