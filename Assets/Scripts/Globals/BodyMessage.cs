using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

[DataContract]
public class BodyMessage
{
    [DataMember]
    public string messageTag { get; set; }

    [DataMember]
    public string messageBody { get; set; }
}
