/// Intermediate class for serializing effects


using BaseClasses;
using System.Reflection;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.IO;
using System.Linq;
using System.Security;
using System.Security.Permissions;


[assembly : ReflectionPermission(SecurityAction.RequestMinimum, Flags=ReflectionPermissionFlag.AllFlags)]

namespace IntermediateSerializers
{
    //[ReflectionPermission(SecurityAction.RequestMinimum, Flags=ReflectionPermissionFlag.AllFlags)]
    public static class EffectSerializer
    {
        private static Assembly ass;
        private static ReflectionPermission perm = new ReflectionPermission(PermissionState.Unrestricted);

        // The flags used to lookup the fields for serialization of fields
        const BindingFlags flags = BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance;

        private static Assembly assemb = Assembly.GetCallingAssembly();

        // This serializes an array of effects into a list of dictionaries, where each dictionary represents one of the effects
        public static List<Dictionary<string, object>> Serialize(Effect[] effs)
        {
            List<Dictionary<string, object>> dict = new List<Dictionary<string, object>>();

            foreach(Effect eff in effs){
                // Create a new dictionary to store the data in;
                Dictionary<string, object> effectData = new Dictionary<string, object>();

                // Store the name of the class so the deserializer can find the class on deserialization
                effectData["Class"] = eff.GetType().FullName;

                // Now save all of the fields of the dictionary under Fields
                effectData["Fields"] = SaveFields(eff);

                dict.Add(effectData);
            }

            return dict;

        }

        public static Effect[] Deserialize(List<Dictionary<string, object>> data)
        {
            Effect[] result = new Effect[data.Count];
            int i = 0;

            foreach(Dictionary<string, object> effData in data){
                // First find the type of the serialized effect
                var typ = assemb.GetType(effData["Class"] as string);

                // Then create a new instance of the class using the default parameterless constructor
                var instance = typ.GetConstructor(Type.EmptyTypes).Invoke(null);
                Debug.Log("We made an instance: " + instance);

                // Finally populate it with the field data
                foreach(var fieldInfos in (effData["Fields"] as Dictionary<string, object>)){
                    Debug.Log("Trying to set " + fieldInfos.Key + " to " + fieldInfos.Value + " of type: " + typ.FullName);
                    Debug.Log("HELOOOO");
                    Debug.Log(typ.GetField(fieldInfos.Key, flags).IsPrivate == true);
                    typ.GetField(fieldInfos.Key, flags).SetValue(instance, fieldInfos.Value, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance, null, null);
                }

                //Now store it in the result
                result[i] = instance as Effect;
                i++;
            }

            return result;
        }


        // Uses reflection to save all the fields that should be saved to the dict
        //[ReflectionPermission(SecurityAction.RequestRefuse, Flags=ReflectionPermissionFlag.AllFlags)]
        private static Dictionary<string, object> SaveFields(Effect eff)
        {
			Dictionary<string, object> fields = new Dictionary<string, object>();
            Type typ = eff.GetType();
            var f = typ.GetMembers(flags);

			// Store all fields as {fieldname : value} in the dict
            foreach (MemberInfo info in f)
            {
                if (info.MemberType != MemberTypes.Field){
                    continue;
                }
                Debug.Log("Looking for: " + info.Name);
                Debug.Log("Looking on: " + eff);
                Debug.Log(eff.GetType());
                Debug.Log(eff.GetType().GetField(info.Name));
                var a = eff.GetType();
                var b = a.GetField(info.Name);
                Debug.Log(a);
                Debug.Log(b);
                var v = eff.GetType().GetField(info.Name, flags);
                Debug.Log(v.IsPrivate == true);
                fields[info.Name] = eff.GetType().GetField(info.Name, flags).GetValue(eff);
                Debug.Log(fields[info.Name]);
            }

            Debug.Log(fields.Count);
            Debug.Log("Hello, writing");
            File.WriteAllLines("tmp_" + typ.Name + ".txt",
            fields.Select(x => "[" + x.Key + " " + x.Value + "]").ToArray());

			return fields;

        }
    }
}
