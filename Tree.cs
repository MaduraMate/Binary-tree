using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree<T>
{

    public Node<T> rootNode;
    public Tree()
    {
        rootNode = null;   
    }

    public int heightOfTree()
    {
        return getHeight(rootNode);
    }

    public int getHeight(Node<T> node)
    {
        if(node == null)
        {
            return 0;
        }

        int heightL = 1 + getHeight(node.Left);
        int heightR = 1 + getHeight(node.Right);

        if(heightL > heightR)
        {
            return heightL;
        }
        else if(heightL < heightR)
        {
            return heightR;
        }

        return heightR;
    }

    T max(T a, T b)
    {
        if (Comparer<T>.Default.Compare(a,b) > 0)
        {
            return a;
        }
        else if(Comparer<T>.Default.Compare(a, b) < 0)
        {
            return b;
        }

        return default(T);
        
    }

    int max(int a, int b)
    {
        return (a > b) ? a : b;
    }

    void balancePPp(ref Node<T> t, Node<T> r)
    {
        t.Right = r.Left;
        t.Right = r.Left;
        r.Left = t;
        r.Balance = 0;
        t.Balance = 0;
        t = r;
    }

    void balanceMMm(ref Node<T> t, Node<T> l)
    {
        t.Left = l.Right;
        l.Right = t;
        l.Balance = 0;
        t.Balance = 0;
        t = l;
    }

    void balancePPm(ref Node<T> t, Node<T> r)
    {
        Node<T> l = r.Left;
        t.Right = l.Left;
        r.Left = l.Right;
        l.Left = t;
        l.Right = r;
        t.Balance = -1 * ((l.Balance + 1) / 2);
        r.Balance = (1 - l.Balance) / 2;
        l.Balance = 0;
        t = l;
    }

    void balanceMMp(ref Node<T> t, Node<T> l)
    {
        Node<T> r = l.Right;
        l.Right = r.Left;
        t.Left = r.Right;
        r.Left = l;
        r.Right = t;
        l.Balance = -1 * ((r.Balance + 1) / 2);
        t.Balance = (1 - r.Balance) / 2;
        r.Balance = 0;
        t = r;
    }

    Node<T> rightSubTreeGrown(ref Node<T> t, ref bool d)
    {

        if (t.Balance == 1)
        {
            Node<T> r = t.Right;
            if (r.Balance == 1)
            {
                balancePPp(ref t, r);
            }
            else
            {
                balancePPm(ref t, r);
            }

            d = false;
        }
        else
        {
            t.Balance += 1;
            d = t.Balance > 0;
        }

        return t;
    }

    Node<T> leftSubTreeGrown(ref Node<T> t, ref bool d)
    {
        if (t.Balance == -1)
        {
            Node<T> l = t.Left;
            if (l.Balance == -1)
            {
                balanceMMm(ref t, l);
            }
            else
            {
                balanceMMp(ref t, l);
            }

            d = false;
        }
        else
        {
            t.Balance -= 1;
            d = t.Balance < 0;
        }

        return t;
    }

    int getBalance(Node<T> node)
    {
        if(node == null)
        {
            return 0;
        }

        return getHeight(node.Left) - getHeight(node.Right);
    }



    public Node<T> insert(ref Node<T> node,ref T key,ref bool d)
    {
        if(node == null)
        {
            node = new Node<T>(key);
            d = true;
            return node;
        }

        //Debug.Log(key);
        //Debug.Log(node.Key);

        if(Comparer<T>.Default.Compare(key, node.Key) < 0)
        {
            //Debug.Log("kisebb");
            insert(ref node.Left,ref key, ref d);

            if(d == true)
            {
                leftSubTreeGrown(ref node, ref d);
            }
        }else if(Comparer<T>.Default.Compare(key, node.Key) > 0)
        {
            //Debug.Log("nagyobb");
            insert(ref node.Right, ref key, ref d);
            Node<T> tjkad = node;
            if (d == true)
            {
                rightSubTreeGrown(ref node, ref d);
            }
        }
        else
        {
            //Debug.Log("egyenlo");
            d = false;
            return node;
        }

        return default(Node<T>);
    }

    public void addNode(T key)
    {

        if(rootNode == null)
        {
            rootNode = new Node<T>(key);
        }
        else
        {
            Node<T> node = new Node<T>(key);
            bool b = true;
            insert(ref rootNode, ref key, ref b);
        }
    }

    public void removeNode(T key)
    {

        if (rootNode != null)
        {
            bool b = true;
            avlDel(ref rootNode, key, ref b);
        }
    }

    void remMin(ref Node<T> t, ref Node<T> minp)
    {
        if(t.Left == null)
        {
            minp = t;
            t = minp.Right;
            minp.Right = null;
        }
        else
        {
            remMin(ref t.Left, ref minp);
        }
    }

    Node<T> AVLremMin(ref Node<T> t, ref Node<T> minp, ref bool d)
    {
        if(t.Left == null)
        {
            minp = t;
            t = minp.Right;
            minp.Right = null;
            d = true;
        }
        else
        {
            AVLremMin(ref t.Left, ref minp, ref d);
            if (d == true)
            {
                leftSubTreeShrunk(ref t, ref d);
            }
        }
        return default(Node<T>);
    }

    void rightSubTreeMinToRoot(ref Node<T> t, ref bool d)
    {
        Node<T> p = null;
        AVLremMin(ref t.Right, ref p, ref d);
        p.Left = t.Left;
        p.Right = t.Right;
        p.Balance = t.Balance;
        t.Left = null;
        t.Right = null;
        t = null;
        t = p;
    }

    void leftSubTreeShrunk(ref Node<T> t, ref bool d)
    {
        if(t.Balance == 1)
        {
            balance_PP(ref t, ref d);
        }
        else
        {
            t.Balance = t.Balance + 1;
            d = (t.Balance == 0);
        }
    }

    void rightSubTreeShrunk(ref Node<T> t, ref bool d)
    {
        if (t.Balance == -1)
        {
            balance_MM(ref t, ref d);
        }
        else
        {
            t.Balance = t.Balance - 1;
            d = (t.Balance == 0);
        }
    }

    void balance_PP(ref Node<T> t, ref bool d)
    {
        Node<T> r = t.Right;

        if(r.Balance == -1)
        {
            balancePPm(ref t, r);
        }else if(r.Balance == 0)
        {
            balancePP0(ref t, r);
            d = false;
        }else if(r.Balance == 1)
        {
            balancePPp(ref t, r);
        }
    }

    void balance_MM(ref Node<T> t, ref bool d)
    {
        Node<T> l = t.Left;

        if (l.Balance == -1)
        {
            balanceMMm(ref t, l);
        }
        else if (l.Balance == 0)
        {
            balanceMM0(ref t, l);
            d = false;
        }
        else if (l.Balance == 1)
        {
            balanceMMp(ref t, l);
        }
    }

    void balancePP0(ref Node<T> t, Node<T> r)
    {
        t.Right = r.Left;
        r.Left = t;
        t.Balance = 1;
        r.Balance = -1;
        t = r;
    }

    void balanceMM0(ref Node<T> t, Node<T> l)
    {
        t.Left = l.Right;
        l.Right = t;
        t.Balance = 1;
        l.Balance = -1;
        t = l;
    }

    public void avlDel(ref Node<T> t, T key, ref bool d)
    {
        if(t != null)
        {
            if(Comparer<T>.Default.Compare(key, t.Key) < 0)
            {
                avlDel(ref t.Left, key, ref d);
                //Debug.Log(d);
                if(d == true)
                {
                    leftSubTreeShrunk(ref t, ref d);
                }
            }else if(Comparer<T>.Default.Compare(key, t.Key) > 0)
            {
                avlDel(ref t.Right, key, ref d);
                //Debug.Log(d);
                if (d == true)
                {
                    rightSubTreeShrunk(ref t, ref d);
                }
            }else if(Comparer<T>.Default.Compare(key, t.Key) == 0)
            {
                AVLdelRoot(ref t, ref d);
                // for debug
                Node<T> dbadba = t;
            }
        }
        else
        {
            d = false;
        }
    }

    void AVLdelRoot(ref Node<T> t, ref bool d)
    {
        if (t.Left == null)
        {
            Node<T> p = t;
            t = p.Right;
            p.Left = null;
            p.Right = null;
            p = null;
            d = true;
        }
        else if(t.Right == null)
        {
            Node<T> p = t;
            t = p.Left;
            p.Left = null;
            p.Right = null;
            p = null;
            d = true;
        }else if (t.Left != null && t.Right != null)
        {
            rightSubTreeMinToRoot(ref t, ref d);
            if (d == true)
            {
                rightSubTreeShrunk(ref t, ref d);
            }
        }
    }

    /*public void delete(ref Node<T> t, T key, ref bool d)
    {
        if(t == null)
        {
            d = false;
            return;
        }
        else
        {
            if(Comparer<T>.Default.Compare(key, t.Key) < 0)
            {
                delete(ref t.Left, key, ref d);

                if (d == true)
                {

                }
            }
            else if(Comparer<T>.Default.Compare(key, t.Key) < 0)
            {
                delete(ref t.Right, key, ref d);

                if (d == true)
                {

                }
            }
            else
            {

            }


        }
    }*/
}
