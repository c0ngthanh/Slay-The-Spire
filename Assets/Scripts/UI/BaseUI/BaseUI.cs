using System;
using UnityEngine;

public abstract class BaseUI : MonoBehaviour
{
    void Awake()
    {
        OnInialize();
    }

    public void Show(){
        OnShow();
        gameObject.SetActive(true);
    }
    public void Hide(){
        OnHide();
        gameObject.SetActive(false);
    } 
    public void Close(){
        OnClose();
        Destroy(gameObject);
    } 
    
    protected virtual void OnInialize()
    {
        
    }

    protected virtual void OnShow(){
        
    }
    protected virtual void OnHide(){
        
    }
    protected virtual void OnClose(){
        
    }
}
