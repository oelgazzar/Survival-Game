using System;
using System.Collections.Generic;

[Serializable]
public class SerializationWrapper<T>
{
    public List<T> Data;
    public SerializationWrapper(List<T> data)
    {
        Data = data;
    }
}