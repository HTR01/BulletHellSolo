using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;

public class xmlPattern : MonoBehaviour
{
    
}

public class Bullet
{
    [XmlAttribute("bullet")]
    public string Name;

}