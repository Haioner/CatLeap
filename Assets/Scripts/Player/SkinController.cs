using UnityEngine.U2D.Animation;
using UnityEngine;

public class SkinController : MonoBehaviour
{
    [SerializeField] private SpriteLibrary library;

    public void SetSkin(SpriteLibraryAsset m_spriteLibraryAsset)
    {
        library.spriteLibraryAsset = m_spriteLibraryAsset;
    }
}


