using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CreateAssetMenu]
public class CommandData : ScriptableObject {

    public List<string> Technique;
    public List<string> TechniqueName;
    public string Skill;
    public string HiddenSkill;
}
