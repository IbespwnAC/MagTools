namespace Mag.Shared.Settings
{
  using Mag.Shared;
  using System;
  using System.Collections.Generic;
  using System.Collections.ObjectModel;
  using System.ComponentModel;
  using System.IO;
  using System.Runtime.InteropServices;
  using System.Xml;

  internal static class SettingsFile
  {
    private static string _documentPath;
    private static string _rootNodeName = "Settings";
    private static readonly System.Xml.XmlDocument XmlDocument = new System.Xml.XmlDocument();

    static SettingsFile()
    {
      ReloadXmlDocument();
    }

    private static XmlNode createMissingNode(string xPath)
    {
      string[] strArray = xPath.Split(new char[] { '/' });
      string xpath = "";
      XmlNode node = XmlDocument.SelectSingleNode(_rootNodeName);
      foreach (string str2 in strArray)
      {
        xpath = xpath + str2;
        if ((XmlDocument.SelectSingleNode(xpath) == null) && (node != null))
        {
          string innerXml = node.InnerXml;
          node.InnerXml = innerXml + "<" + str2 + "></" + str2 + ">";
        }
        node = XmlDocument.SelectSingleNode(xpath);
        xpath = xpath + "/";
      }
      return node;
    }

    public static IEnumerable<string> GetCollection(string xPath)
    {
      XmlNode node = XmlDocument.SelectSingleNode(_rootNodeName + "/" + xPath);
      Collection<string> collection = new Collection<string>();
      if (node != null)
      {
        foreach (XmlNode node2 in node.ChildNodes)
        {
          collection.Add(node2.InnerText);
        }
      }
      return collection;
    }

    public static XmlNode GetNode(string xPath)
    {
      return XmlDocument.SelectSingleNode(_rootNodeName + "/" + xPath);
    }

    public static T GetSetting<T>(string xPath, T defaultValue = default(T))
    {
      XmlNode node = XmlDocument.SelectSingleNode(_rootNodeName + "/" + xPath);
      if (node != null)
      {
        TypeConverter converter = TypeDescriptor.GetConverter(typeof(T));
        if (converter.CanConvertFrom(typeof(string)))
        {
          return (T) converter.ConvertFromString(node.InnerText);
        }
      }
      return defaultValue;
    }

    public static void Init(string filePath, string rootNode = "Settings")
    {
      _documentPath = filePath;
      _rootNodeName = rootNode;
      ReloadXmlDocument();
    }

    public static void PutSetting<T>(string xPath, T value)
    {
      ReloadXmlDocument();
      XmlNode node = XmlDocument.SelectSingleNode(_rootNodeName + "/" + xPath);
      if (node == null)
      {
        node = createMissingNode(_rootNodeName + "/" + xPath);
      }
      TypeConverter converter = TypeDescriptor.GetConverter(typeof(T));
      if (converter.CanConvertTo(typeof(string)))
      {
        string str = converter.ConvertToString(value);
        if (str != null)
        {
          node.InnerText = str;
          XmlDocument.Save(_documentPath);
        }
      }
    }

    public static void ReloadXmlDocument()
    {
      try
      {
        if (!string.IsNullOrEmpty(_documentPath) && File.Exists(_documentPath))
        {
          XmlDocument.Load(_documentPath);
        }
        else
        {
          XmlDocument.LoadXml("<" + _rootNodeName + "></" + _rootNodeName + ">");
        }
      }
      catch (Exception exception)
      {
        Debug.LogException(exception, null);
        XmlDocument.LoadXml("<" + _rootNodeName + "></" + _rootNodeName + ">");
      }
    }

    public static void SetNodeChilderen(string xPath, string childNodeName, Collection<Dictionary<string, string>> childNodeAttributes)
    {
      ReloadXmlDocument();
      XmlNode node = XmlDocument.SelectSingleNode(_rootNodeName + "/" + xPath);
      if (node == null)
      {
        node = createMissingNode(_rootNodeName + "/" + xPath);
      }
      if (node.HasChildNodes)
      {
        node.RemoveAll();
      }
      foreach (Dictionary<string, string> dictionary in childNodeAttributes)
      {
        XmlNode node2 = node.AppendChild(XmlDocument.CreateElement(childNodeName));
        foreach (KeyValuePair<string, string> pair in dictionary)
        {
          XmlAttribute attribute = XmlDocument.CreateAttribute(pair.Key);
          attribute.Value = pair.Value;
          if (node2.Attributes != null)
          {
            node2.Attributes.Append(attribute);
          }
        }
      }
      XmlDocument.Save(_documentPath);
    }
  }
}

