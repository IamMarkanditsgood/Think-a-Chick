using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class AvatarManager : MonoBehaviour
{
    [SerializeField] private RawImage[] _avatar;

    [SerializeField] private Texture2D _basicAvatar;

    private int maxSize = 1000;
    private string _path;
    private Texture2D _texture;

    public void SetSavedPicture()
    {
        if (PlayerPrefs.HasKey("AvatarPath"))
        {
            string path = PlayerPrefs.GetString("AvatarPath");
            foreach (var image in _avatar)
            {
                image.texture = NativeGallery.LoadImageAtPath(path, maxSize);
            }
        }
        else
        {
            foreach (var image in _avatar)
            {
                image.texture = _basicAvatar;
            }
            
        }
    }

    public void PickFromGallery()
    {
        if (NativeGallery.IsMediaPickerBusy())
            return;

        NativeGallery.Permission permission = NativeGallery.GetImageFromGallery((path) =>
        {
            if (path != null)
            {
                _path = path;

                _texture = NativeGallery.LoadImageAtPath(path, maxSize);
                if (_texture == null)
                {
                    Debug.Log("Couldn't load texture from " + path);
                    return;
                }
                foreach (var image in _avatar)
                {
                    image.texture = _texture;
                }
                PlayerPrefs.SetString("AvatarPath", _path);
            }
        }, "Select an image", "image/*");

        Debug.Log("Permission result: " + permission);
    }

}
