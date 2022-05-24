using UnityEngine;
using MoonSharp.Interpreter;
using TMPro;
using System;
using MoonSharp.Interpreter.Loaders;
using Assets;
using UnityEngine.UI;
using System.IO;

public class LuaEngine : MonoBehaviour
{
    
    [SerializeField]  public string lua_scripts_path = ".\\";
    [SerializeField]  public TMP_Text caption_field;
    [SerializeField]  public TMP_Text text_field;
    [SerializeField]  public Image img_field;

    private Script scripting = new Script();

    private Sprite LoadNewSprite(string FilePath, float PixelsPerUnit = 100.0f)
    {

        // Load a PNG or JPG image from disk to a Texture2D, assign this texture to a new sprite and return its reference
        Texture2D SpriteTexture = LoadTexture(FilePath);
        Sprite NewSprite = Sprite.Create(SpriteTexture, new Rect(0, 0, SpriteTexture.width, SpriteTexture.height), new Vector2(0, 0), PixelsPerUnit);

        return NewSprite;
    }

    private Texture2D LoadTexture(string FilePath)
    {

        // Load a PNG or JPG file from disk to a Texture2D
        // Returns null if load fails

        Texture2D Tex2D;
        byte[] FileData;

        if (File.Exists(FilePath))
        {
            FileData = File.ReadAllBytes(FilePath);
            Tex2D = new Texture2D(2, 2);           // Create new "empty" texture
            if (Tex2D.LoadImage(FileData))           // Load the imagedata into the texture (size is set automatically)
                return Tex2D;                 // If data = readable -> return texture
        }
        return null;                     // Return null if load failed
    }

    

    void redraw()
    {

    }

    void setMainText(string text)
    {
        text_field.text = text;
    }

    void setTitle(string text)
    {
        caption_field.text = text;
        Debug.Log($"Text settle {text}");
    }
    void setImage(string imageName)
    {
        string s = lua_scripts_path + imageName;
        Debug.Log($"setImage {s}");
        //Sprite spr = Resources.Load<Sprite>(lua_scripts_path + imageName);
        Sprite spr = LoadNewSprite(lua_scripts_path + imageName);
        img_field.sprite = spr;
    }

    void setInvText(string text)
    {

    }

    void print_lua(params string[] subjects)
    {
        string outp = "";
        foreach(string s in subjects)
        {
            outp = outp + s + " ";
        }
        Debug.Log(outp);
    }

    public void playAudio(string filename)
    {
        Debug.Log($"Audio: {filename}");
    }
    public void stopAudio()
    {
        Debug.Log("Audio stop");
    }
    void Start()
    {
        scripting.Globals["platform"] = "Unity";
        scripting.Globals["redraw"] = (Action)redraw;
        scripting.Globals["setMainText"] = (Action<string>)setMainText;
        scripting.Globals["setTitle"] = (Action<string>)setTitle;
        scripting.Globals["setImage"] = (Action<string>)setImage;
        scripting.Globals["setInvText"] = (Action<string>)setInvText;
        scripting.Globals["print"] = (Action<string[]>)print_lua;
        scripting.Globals["unity_audio_play"] = (Action<string>)playAudio;
        scripting.Globals["unity_audio_stop"] = (Action)stopAudio;

        //var loader = new FileSystemScriptLoader();
        var loader = new Loader();
        loader.ModulePaths.Add(lua_scripts_path); 
        scripting.Options.ScriptLoader = loader;
        scripting.DoFile("main.lua", scripting.Globals);
        //scripting.DoFile("hello1.lua", scripting.Globals);
    }
}
