using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Thesis
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
            bf.Binder = new PreMergeToMergedDeserializationBinder();
            using(MemoryStream ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                return ms.ToArray();
            }      
        }
        // Serialize an Quiz to a byte array
        public static byte[] QuiztoByteArray(QuizData obj)
        {
            if(obj == null)
                return null;

            BinaryFormatter bf = new BinaryFormatter();
            bf.Binder = new PreMergeToMergedDeserializationBinder();
            using(MemoryStream ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                return ms.ToArray();
            }
        }

        // Dezerialize a byte array to an Object
        public static QuizData ByteArrayToQuiz(byte[] arrBytes)
        {
            BinaryFormatter binForm = new BinaryFormatter();
            binForm.Binder = new PreMergeToMergedDeserializationBinder();
            QuizData obj;
            using(MemoryStream memStream = new MemoryStream())
            {
                memStream.Write(arrBytes, 0, arrBytes.Length);
                memStream.Seek(0, SeekOrigin.Begin);

                obj = (QuizData)binForm.Deserialize(memStream);
            }
            return obj;
        }

        // Dezerialize a byte array to an Object
        public static object ByteArrayToObject(byte[] arrBytes)
        {
            BinaryFormatter binForm = new BinaryFormatter();
            binForm.Binder = new PreMergeToMergedDeserializationBinder();
            object obj;
            using(MemoryStream memStream = new MemoryStream())
            {
                memStream.Write(arrBytes, 0, arrBytes.Length);
                memStream.Seek(0, SeekOrigin.Begin);

                obj = binForm.Deserialize(memStream);
            }
            return obj;
        }

        public static void QuizObjToDataFile(Quiz quiz)
        {
            if(quiz == null)
                return;

            string folderlocation;
            if(Android.OS.Environment.ExternalStorageState.Equals(Android.OS.Environment.MediaMounted))
                folderlocation = Android.OS.Environment.ExternalStorageDirectory.Path;
            else
                folderlocation = Android.OS.Environment.DirectoryDocuments;

            folderlocation += @"/Quizzes";
            if(!Directory.Exists(folderlocation))
                Directory.CreateDirectory(folderlocation);

            string file = @"/" + quiz.Title + ".dat";
            using(Stream stream = File.Create(folderlocation + file))
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Binder = new PreMergeToMergedDeserializationBinder();
                bf.Serialize(stream, quiz);
            }  
        }
        public static Quiz DataFileToQuizObj(string filename)
        {
            BinaryFormatter bf = new BinaryFormatter();
            bf.Binder = new PreMergeToMergedDeserializationBinder();
            string folderlocation;
            if(Android.OS.Environment.ExternalStorageState.Equals(Android.OS.Environment.MediaMounted))
                folderlocation = Android.OS.Environment.ExternalStorageDirectory.Path;
            else
                folderlocation = Android.OS.Environment.DirectoryDocuments;

            folderlocation += @"/Quizzes";
            if(!Directory.Exists(folderlocation))
                Directory.CreateDirectory(folderlocation);

            string file = @"/" + filename + ".dat";
            using(Stream stream = File.OpenRead(folderlocation + file))
            {
                Quiz quiz = (Quiz)bf.Deserialize(stream);
                return quiz;
            }
        }
    }
}
