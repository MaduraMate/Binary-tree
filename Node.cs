using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node<T>
{
    T key;
    int balance, height;
    Node<T> left, right, parent;

    public Node(T key)
    {
        this.key = key;
        left = null;
        right = null;
        parent = null;
        balance = 0;
        height = 1;
    }

    public Node<T> Parent
    {
        get
        {
            return parent;
        }

        set
        {
            parent = value;
        }
    }

    public int Height
    {
        get
        {
            return height;
        }

        set
        {
            height = value;
        }
    }

    public T Key
    {
        get
        {
            return key;
        }
    }

    public ref Node<T> Left
    {
        get
        {
            return ref left;
        }

    }

    public ref Node<T> Right
    {
        get
        {
            return ref right;
        }
    }
    public int Balance
    {
        get
        {
            return balance;
        }

        set
        {
            balance = value;
        }
    }

}
