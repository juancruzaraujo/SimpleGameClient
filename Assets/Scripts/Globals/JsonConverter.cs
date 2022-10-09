using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Json;
using System.IO;
using System;
using System.Text;

public static class JsonConverter
{
    public static string ClassToJson(Type theClass, object obj)
    {
        var stream1 = new MemoryStream();
        var ser = new DataContractJsonSerializer(theClass);
        ser.WriteObject(stream1, obj);

        stream1.Position = 0;
        var sr = new StreamReader(stream1);

        string json = sr.ReadToEnd();
        return json;
    }

    public static object JsonToClass(string stringJson, Type theClass, object obj)
    {
        var serializer = new DataContractJsonSerializer(theClass);
        var ms = new MemoryStream(Encoding.UTF8.GetBytes(stringJson));

        var result = serializer.ReadObject(ms);
        ms.Close();

        return result;
    }

}

