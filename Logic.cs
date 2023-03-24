using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Logic : MonoBehaviour
{
    public GameObject circle;
    public Button button;
    public Button deleteButton;
    public TMP_InputField addNodeField;
    public TMP_InputField deleteNodeField;
    private List<Circle> circlesUI = new List<Circle>();
    private List<LineRenderer> lines = new List<LineRenderer>();
    [SerializeField] private Circle CirclePrefab;
    Tree<int> avltree = new Tree<int>();
    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener(AddNodeButtonHandler);
        deleteButton.onClick.AddListener(deleteNodeButtonHandler);
        avltree.addNode(10);
        avltree.addNode(20);
        avltree.addNode(30);
        avltree.addNode(40);
        avltree.addNode(50);
        avltree.addNode(60);
        avltree.addNode(70);
        avltree.addNode(80);
        avltree.addNode(80);
        geneRateUITree(avltree.rootNode, false, new Circle());

    }

    void geneRateUITree(Node<int> node, bool right, Circle circle)
    {
        Vector3 pos = new Vector3();
        Circle cir = null;
        if(node == avltree.rootNode)
        {
            cir = Instantiate(CirclePrefab, Vector2.zero, Quaternion.identity);
            circlesUI.Add(cir);
            cir.ValueText.text = avltree.rootNode.Key.ToString();
            cir.transform.position = cir.transform.position + Vector3.up * (float) avltree.heightOfTree();
            pos = cir.transform.position;
        }
        else
        {
            if(right == false)
            {
                if(node.Key == 20)
                {
                    //Debug.Log(avltree.getHeight(node));
                }

                
                cir = Instantiate(CirclePrefab, Vector2.zero, Quaternion.identity);
                circlesUI.Add(cir);
                cir.ValueText.text = node.Key.ToString();
                cir.transform.position = cir.transform.position + Vector3.up  * (float) (circle.transform.position.y - 1);
                if((circle.transform.position.y - 1) == avltree.heightOfTree() - 1)
                {
                    cir.transform.position = cir.transform.position + Vector3.right * ((float)(circle.transform.position.x - 8));
                }
                else
                {
                    cir.transform.position = cir.transform.position + Vector3.right * ((float)(circle.transform.position.x - 2));
                }
                
                pos = cir.transform.position;
                GameObject lineObject = new GameObject("Line");
                LineRenderer lineRenderer = lineObject.AddComponent<LineRenderer>();
                lineRenderer.positionCount = 2;
                lineRenderer.startWidth = 0.05f;
                lineRenderer.endWidth = 0.05f;
                lineRenderer.material.color = Color.black;
                lineRenderer.SetPosition(0, cir.transform.position);
                lineRenderer.SetPosition(1, circle.transform.position);
                lines.Add(lineRenderer);
            }

            if(right == true)
            {
                if (node.Key == 60)
                {
                    //Debug.Log(avltree.getHeight(node));
                }
                cir = Instantiate(CirclePrefab, Vector2.zero, Quaternion.identity);
                circlesUI.Add(cir);
                cir.ValueText.text = node.Key.ToString();
                cir.transform.position = cir.transform.position + Vector3.up * (float) (circle.transform.position.y - 1);
                if ((circle.transform.position.y - 1) == avltree.heightOfTree() - 1)
                {
                    cir.transform.position = cir.transform.position + Vector3.right * ((float)(circle.transform.position.x + 8));
                }
                else
                {
                    cir.transform.position = cir.transform.position + Vector3.right * ((float)(circle.transform.position.x + 2));
                }
                pos = cir.transform.position; GameObject lineObject = new GameObject("Line");
                LineRenderer lineRenderer = lineObject.AddComponent<LineRenderer>();
                lineRenderer.positionCount = 2;
                lineRenderer.startWidth = 0.05f;
                lineRenderer.endWidth = 0.05f;
                lineRenderer.material.color = Color.black;
                lineRenderer.SetPosition(0, cir.transform.position);
                lineRenderer.SetPosition(1, circle.transform.position);
                lines.Add(lineRenderer);
            }
        }

        if(node.Left != null)
        {
            //Debug.Log("Left");
            geneRateUITree(node.Left, false, cir);
        }

        if(node.Right != null)
        {
            //Debug.Log("Right");
            geneRateUITree(node.Right, true, cir);
        }
    }

    void destroyCircles()
    {
        foreach(Circle circ in circlesUI)
        {
            Destroy(circ.gameObject);
        }
        circlesUI.Clear();
        foreach (LineRenderer line in lines)
        {
            Destroy(line.gameObject);
        }
        lines.Clear();
    }

    public void AddNodeButtonHandler()
    {
        int number = int.Parse(addNodeField.text);
        avltree.addNode(number);
        destroyCircles();
        geneRateUITree(avltree.rootNode, false, new Circle());
        

    }

    void deleteNodeButtonHandler()
    {
        int number = int.Parse(deleteNodeField.text);
        avltree.removeNode(number);
        destroyCircles();
        geneRateUITree(avltree.rootNode, false, new Circle());
        //Debug.Log(avltree.rootNode.Key);
        foreach (Circle c in circlesUI)
        {
            //Debug.Log(c.ValueText.ToString());
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
