/**
 * Copyright 2012 Calvin Rien
 * (http://the.darktable.com)
 * 
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 * 
 * http://www.apache.org/licenses/LICENSE-2.0
 * 
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

/**
 * https://gist.github.com/yourpalmark/6231952
 */

#define PLIST_CS

using System.IO;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;
using System.Collections.Generic;

#if PLIST_CS
using PlistCS;
#endif

public class IncrementBuildVersion : ScriptableObject
{
	/*
	[MenuItem("Tools/Increment Build Number")]
	static void Init()
	{
		IncrementBuild("");
	}
	*/
	
	[PostProcessBuild]
	public static void OnPostProcessBuild(BuildTarget target, string buildPath)
	{
		if (target != BuildTarget.iPhone)
		{
			return;
		}
		
		IncrementBuild(buildPath);
	}
	
	public static void IncrementBuild(string buildPath)
	{
		var settingsPath = Path.GetDirectoryName(Application.dataPath);
		settingsPath = Path.Combine(settingsPath, "ProjectSettings");
		settingsPath = Path.Combine(settingsPath, "ProjectSettings.asset");
		
		if (!File.Exists(settingsPath))
		{
			Debug.LogError("Couldn't find project settings file.");
			return;
		}
		
		var lines = File.ReadAllLines(settingsPath);
		
		if (!lines[0].StartsWith("%YAML"))
		{
			Debug.LogError("Project settings file needs to be serialized as a text asset. (Check 'Project Settings->Editor')");
			return;
		}
		
		string pattern = @"^(\s*iPhoneBundleVersion:\s*)([\d\.]+)$";
		bool success = false;
		
		System.Version version = null;
		
		for (int i=0; i<lines.Length; i++)
		{
			var line = lines[i];
			
			if (!Regex.IsMatch(line, pattern))
			{
				continue;
			}
			
			var match = Regex.Match(line, pattern);
			
			version = new System.Version(match.Groups[2].Value);
			
			var major = version.Major < 0 ? 0 : version.Major;
			var minor = version.Minor < 0 ? 0 : version.Minor;
			var build = version.Build < 0 ? 0 : version.Build;
			var revision = version.Revision < 0 ? 0 : version.Revision;
			
			version = new System.Version(major, minor, build, revision + 1);
			
			line = match.Groups[1].Value + version;
			
			lines[i] = line;
			success = true;
			
			break;
		}
		
		if (!success)
		{
			Debug.LogError("Couldn't find bundle version in ProjectSettings.asset");
			return;
		}
		
		File.WriteAllLines(settingsPath, lines);
		
		Debug.Log("Build version: " + version);
		
		#if PLIST_CS
		var plistPath = Path.Combine(buildPath, "Info.plist");
		
		if (!File.Exists(plistPath))
		{
			Debug.LogWarning("Couldn't find Info.plist in build output.");
			return;
		}
		
		// modify plist
		var plist = (Dictionary<string,object>) Plist.readPlist(plistPath);
		
		plist["CFBundleShortVersionString"] = version.Major.ToString() + "." + version.Minor.ToString() + "." + version.Build.ToString();
		plist["CFBundleVersion"] = version.Revision.ToString();
		
		Plist.writeXml(plist, plistPath);
		#endif
	}
}