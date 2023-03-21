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
        //Debug.Log(avltree.heightOfTree());
        //Debug.Log(avltree.rootNode.Key);
        //Debug.Log(avltree.rootNode.Left.Key);
        //Debug.Log(avltree.rootNode.Right.Key);
        //GameObject gameObject = Instantiate(circle, new Vector3(1, 1), transform.rotation);
        //var cir = Instantiate(CirclePrefab, Vector2.zero, Quaternion.identity);
        //cir.ValueText.text = 30.ToString();
        //cir.transform.position = cir.transform.position + Vector3.up * (float) 1;
        //cir.transform.position = cir.transform.position + Vector3.up * (float) 1;
        //cir.transform.position = cir.transform.position + Vector3.up * (float) 1;
        //cir.transform.position = cir.transform.position + Vector3.up * (float) 1;
        //var cir2 = Instantiate(CirclePrefab, Vector2.zero, Quaternion.identity);
        //cir2.ValueText.text = 30.ToString();
        //cir2.transform.position = cir2.transform.position + Vector3.up * (float)1;
        //cir2.transform.position = cir2.transform.position + Vector3.up * (float)1;
        //cir2.transform.position = cir2.transform.position + Vector3.up * (float)1;
        //cir2.transform.position = cir2.transform.position + Vector3.right * (float)1;
        //cir.updatePosition(); 
        //cir.updatePosition();
        //cir.updatePosition();
        //cir.updatePosition();
        geneRateUITree(avltree.rootNode, false, new Vector3());

    }

    void geneRateUITree(Node<int> node, bool right, Vector3 v3)
    {
        Vector3 pos = new Vector3();
        if(node == avltree.rootNode)
        {
            var cir = Instantiate(CirclePrefab, Vector2.zero, Quaternion.identity);
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

                
                var cir = Instantiate(CirclePrefab, Vector2.zero, Quaternion.identity);
                circlesUI.Add(cir);
                cir.ValueText.text = node.Key.ToString();
                cir.transform.position = cir.transform.position + Vector3.up  * (float) (v3.y - 1);
                if((v3.y - 1) == avltree.heightOfTree() - 1)
                {
                    cir.transform.position = cir.transform.position + Vector3.right * ((float)(v3.x - 8));
                }
                else
                {
                    cir.transform.position = cir.transform.position + Vector3.right * ((float)(v3.x - 2));
                }
                
                pos = cir.transform.position;
            }

            if(right == true)
            {
                if (node.Key == 60)
                {
                    //Debug.Log(avltree.getHeight(node));
                }
                var cir = Instantiate(CirclePrefab, Vector2.zero, Quaternion.identity);
                circlesUI.Add(cir);
                cir.ValueText.text = node.Key.ToString();
                cir.transform.position = cir.transform.position + Vector3.up * (float) (v3.y - 1);
                if ((v3.y - 1) == avltree.heightOfTree() - 1)
                {
                    cir.transform.position = cir.transform.position + Vector3.right * ((float)(v3.x + 8));
                }
                else
                {
                    cir.transform.position = cir.transform.position + Vector3.right * ((float)(v3.x + 2));
                }
                pos = cir.transform.position;
            }
        }

        if(node.Left != null)
        {
            //Debug.Log("Left");
            geneRateUITree(node.Left, false, pos);
        }

        if(node.Right != null)
        {
            //Debug.Log("Right");
            geneRateUITree(node.Right, true, pos);
        }
    }

    void destroyCircles()
    {
        foreach(Circle circ in circlesUI)
        {
            Destroy(circ.gameObject);
        }
        circlesUI.Clear();
    }

    public void AddNodeButtonHandler()
    {
        int number = int.Parse(addNodeField.text);
        avltree.addNode(number);
        destroyCircles();
        geneRateUITree(avltree.rootNode, false, new Vector3());
        

    }

    void deleteNodeButtonHandler()
    {
        int number = int.Parse(deleteNodeField.text);
        avltree.removeNode(number);
        destroyCircles();
        geneRateUITree(avltree.rootNode, false, new Vector3());
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
