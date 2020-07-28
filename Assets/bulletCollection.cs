using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;

public class bulletCollection : MonoBehaviour
{
    
}

[XmlRoot("BulletLibrary")]
public class BulletContainer
{
    [XmlArray("Bullets")]
    [XmlArrayItem("Bullet")]
    public List<Bullet> Bullets = new List<Bullet>();
}
