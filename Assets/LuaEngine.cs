using UnityEngine;
using MoonSharp.Interpreter;
using TMPro;
using System;
using MoonSharp.Interpreter.Loaders;

public class LuaEngine : MonoBehaviour
{
    [SerializeField]  public string lua_scripts_path = ".\\";
    [SerializeField]  public TMP_Text caption_field;
    [SerializeField]  public TMP_Text text_field;

    private Script scripting = new Script();

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

    }

    void setInvText(string text)
    {

    }

    void Start()
    {
        scripting.Globals["platform"] = "Unity";
        scripting.Globals["redraw"] = (Action)redraw;
        scripting.Globals["setMainText"] = (Action<string>)setMainText;
        scripting.Globals["setTitle"] = (Action<string>)setTitle;
        scripting.Globals["setImage"] = (Action<string>)setImage;
        scripting.Globals["setInvText"] = (Action<string>)setInvText;
        // scripting.Globals["redraw"] = (Action)redraw;



        var loader = new FileSystemScriptLoader();
        //scripting.RequireModule("E:\\Gamedev\\LuaWorldWrapper\\LuaWorldUnity\\testdir\\test.lua");
        //scripting.Global
        scripting.Options.ScriptLoader = loader;
        //loader.ModulePaths = new string[] { lua_scripts_path + "\\?", lua_scripts_path + "\\?.lua" };
        loader.ModulePaths= new string[] { $"{lua_scripts_path}\\?", $"{lua_scripts_path}\\?.lua" };

        scripting.DoFile("testdir/test.lua", scripting.Globals);
        scripting.DoFile("main.lua", scripting.Globals);
    }
}
