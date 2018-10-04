using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utility {

	public static Dictionary<string,string> sceneToFile = new Dictionary<string,string>
	{
		{"Introduction 1", "introduction.txt"},
		{"Bedroom 1", "bedroom1.txt"},
		{"House Hall 1", "hallway.txt"},
		{"House LivingRoom", "downstairsArea.txt"},
		{"House Kitchen", "cereal.txt"},
		{"School Entrance", "School Entrance.txt"},
		{"School DownStairs", "School Downstairs.txt"},
		{"School UpStairs", "School Upstairs.txt"}
	};
    public static readonly string filepath = Application.dataPath;
   
    public static readonly string SCORES_FILE = System.IO.Path.Combine(Application.streamingAssetsPath, "scores.txt");
	public static readonly string FILE_PREFIX = Application.streamingAssetsPath;
	public static readonly string TXT_SUFFIX = ".txt";
  

	public static string PrefixFile(string name){
		if (!name.EndsWith(TXT_SUFFIX)){
			name += TXT_SUFFIX;
		}

		return System.IO.Path.Combine(FILE_PREFIX, name);

	}

}
