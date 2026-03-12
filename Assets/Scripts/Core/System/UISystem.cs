using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UISystem : SystemBase
{
    private UIConfig uiConfig;
    private Transform uiRoot; 


    // Dictionary để quản lý các UI đã được Instantiate ra màn hình
    private Dictionary<Type, BaseUI> _spawnedUIs = new Dictionary<Type, BaseUI>();

    public override void Initialize()
    {
        uiConfig = Resources.Load<UIConfig>("UIConfig"); // Load UIConfig từ Resources
        uiRoot = GameObject.Instantiate(Resources.Load<GameObject>("UIRoot")).transform; // Instantiate UIRoot prefab
        GameObject.DontDestroyOnLoad(uiRoot.gameObject);
    }

    public T ShowUI<T>() where T : BaseUI
    {
        Type uiType = typeof(T);
        BaseUI ui = GetOrSpawnUI(uiType);
        if (ui != null)
        {
            ui.Show(); // Hàm của BaseUI
            return ui as T;
        }
        return null;
    }

    /// <summary>
    /// Ẩn 1 UI
    /// </summary>
    public void HideUI<T>() where T : BaseUI
    {
        Type uiType = typeof(T);
        if (_spawnedUIs.TryGetValue(uiType, out BaseUI ui))
        {
            ui.Hide();
        }
    }

    /// <summary>
    /// Đóng và huỷ một UI
    /// </summary>
    public void CloseUI<T>() where T : BaseUI
    {
        Type uiType = typeof(T);
        if (_spawnedUIs.TryGetValue(uiType, out BaseUI ui))
        {
            ui.Close();
            _spawnedUIs.Remove(uiType);
        }
    }

    /// <summary>
    /// Lấy (nếu có sẵn) hoặc Tạo mới (nếu chưa có) từ Prefab
    /// </summary>
    private BaseUI GetOrSpawnUI(Type uiType)
    {
        // 1. Nếu UI này đã từng được sinh ra rồi thì lấy ra dùng luôn
        if (_spawnedUIs.TryGetValue(uiType, out BaseUI existingUI))
        {
            return existingUI;
        }

        BaseUI prefab = uiConfig.UIPrefabs.Find(p => p.GetType() == uiType);
        if (prefab == null)
        {
            Debug.LogError($"UI Prefab of type {uiType} not found in UIConfig!");
            return null;
        }

        BaseUI newUI = GameObject.Instantiate(prefab, uiRoot);
        _spawnedUIs.Add(uiType, newUI);
        
        newUI.gameObject.SetActive(false); 
        
        return newUI;
    }

    public T GetUI<T>() where T : BaseUI
    {
        Type uiType = typeof(T);
        if (_spawnedUIs.TryGetValue(uiType, out BaseUI ui))
        {
            return ui as T;
        }
        return null;
    }
}
