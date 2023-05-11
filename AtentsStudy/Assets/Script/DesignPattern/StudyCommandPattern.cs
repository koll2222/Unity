using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public interface ICommand
{
    void Execute(Transform tr);
    void Undo(Transform tr);
}

public class MoveLeft : ICommand
{
    public void Execute(Transform tr)
    {
        tr.Translate(Vector3.left);
    }
    public void Undo(Transform tr)
    {
        tr.Translate(Vector3.right);
    }
}
public class MoveRight : ICommand
{
    public void Execute(Transform tr)
    {
        tr.Translate(Vector3.right);
    }
    public void Undo(Transform tr)
    {
        tr.Translate(Vector3.left);
    }
}
public class MoveForward : ICommand
{
    public void Execute(Transform tr)
    {
        tr.Translate(Vector3.forward);
    }
    public void Undo(Transform tr)
    {
        tr.Translate(Vector3.back);
    }
}
public class MoveBack : ICommand
{
    public void Execute(Transform tr)
    {
        tr.Translate(Vector3.back);
    }
    public void Undo(Transform tr)
    {
        tr.Translate(Vector3.forward);
    }
}
public class StudyCommandPattern : MonoBehaviour
{
    Stack<ICommand> CommandList = new Stack<ICommand>();

    ICommand Wkey;
    ICommand Akey;
    ICommand Skey;
    ICommand Dkey;

    void Start()
    {
        Wkey = new MoveForward();
        Akey = new MoveLeft();
        Skey = new MoveBack();
        Dkey = new MoveRight();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            Wkey.Execute(transform);
            CommandList.Push(Wkey);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            Akey.Execute(transform);
            CommandList.Push(Akey);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            Skey.Execute(transform);
            CommandList.Push(Skey);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            Dkey.Execute(transform);
            CommandList.Push(Dkey);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (CommandList.Count > 0) CommandList.Pop().Undo(transform);
        }
    }
}
