using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UIConfig", menuName = "Config/UIConfig")]
public class UIConfig : ScriptableObject
{
    public List<BaseUI> UIPrefabs; // Danh sách prefab UI, có thể mở rộng thêm sau này
}
