using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace ThesisClient
{
    // Helps fix Serialization Excpetion of two different assemblies :/
    sealed class PreMergeToMergedDeserializationBinder : SerializationBinder
    {
        public override Type BindToType(string assemblyName, string typeName)
        {
            Type typeToDeserialize = null;

            // For each assemblyName/typeName that you want to deserialize to
            // a different type, set typeToDeserialize to the desired type.
            String exeAssembly = Assembly.GetExecutingAssembly().FullName;

            // The following line of code returns the type.
            typeToDeserialize = Type.GetType(String.Format("{0}, {1}",
                typeName, exeAssembly));

            return typeToDeserialize;
        }
    }

    //Static class for Serialization
    static class BinarySerializer
    {
        // Serialize an Objectbyte to a byte array
        public static byte[] ObjectToByteArray(Object obj)
        {       
            if(obj == null)
                return null;

            BinaryFormatter bf = new BinaryFormatter();
            using(MemoryStream ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                return ms.ToArray();
            }       
        }


        // Dezerialize a byte array to an Object
        public static object ByteArrayToObject(byte[] arrBytes)

        {
            BinaryFormatter binForm = new BinaryFormatter();
            binForm.Binder = new PreMergeToMergedDeserializationBinder();

            using(MemoryStream memStream = new MemoryStream())
            {
                memStream.Write(arrBytes, 0, arrBytes.Length);
                memStream.Seek(0, SeekOrigin.Begin);

                object obj = binForm.Deserialize(memStream);
                return obj;
            }
        }
    }
}
