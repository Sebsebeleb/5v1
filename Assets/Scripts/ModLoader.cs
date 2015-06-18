using UnityEngine;  
using IronPython;  
using IronPython.Modules;  
using System.Text;
using System;
using System.IO;
using Microsoft.Scripting.Hosting;

// derive from EditorWindow for convenience, but this is just a fire-n-forget script  
public class ModLoader : MonoBehaviour {
	
	[SerializeField]
	private string[] RunOnLoad;
	
	private ScriptEngine engine;
	private ScriptScope scope;

	void Awake(){
		CreateEngine();
	}
	
	void Start(){
		foreach(string path in RunOnLoad){
			RunScript(ReadScript(Application.streamingAssetsPath + "/" +path));
		}
	}
	
	private string ReadScript(string path){
		StreamReader sr = new StreamReader(path);
		return sr.ReadToEnd();
	}
	
	public void RunScript(string data){
		var source = engine.CreateScriptSourceFromString(data);
		source.Execute(scope);
	}

	private void CreateEngine(){
		engine = IronPython.Hosting.Python.CreateEngine();
		scope = engine.CreateScope();
	}

	public static void ScriptTest()  
	{  
		// create the engine  
		var ScriptEngine = IronPython.Hosting.Python.CreateEngine();  
		// and the scope (ie, the python namespace)  ac
		var ScriptScope = ScriptEngine.CreateScope();  
		// execute a string in the interpreter and grab the variable  
		string example = "output = 'hello world'";  
		var ScriptSource = ScriptEngine.CreateScriptSourceFromString(example);  
		ScriptSource.Execute(ScriptScope);  
		string came_from_script = ScriptScope.GetVariable<string>("output");  
		// Should be what we put into 'output' in the script.  
		Debug.Log(came_from_script);              
	}  
}  
	
