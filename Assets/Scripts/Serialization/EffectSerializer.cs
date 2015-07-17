using BaseClasses;
using System.Reflection;
using System.Collections.Generic;
using System;
using UnityEngine;



namespace IntermediateSerializers
{

    /// Intermediate class for serializing effects
    public static class EffectSerializer
    {
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

        // Loads all effects from a dictionary (From a save file)
        public static Effect[] Deserialize(List<Dictionary<string, object>> data)
        {
            Effect[] result = new Effect[data.Count];
            int i = 0;

            foreach(Dictionary<string, object> effData in data){
                // First find the type of the serialized effect
                var typ = assemb.GetType(effData["Class"] as string);

                // Then create a new instance of the class using the default parameterless constructor
                var instance = typ.GetConstructor(Type.EmptyTypes).Invoke(null);

                // Finally populate it with the field data
                foreach(var fieldInfos in (effData["Fields"] as Dictionary<string, object>)){
                    typ.GetField(fieldInfos.Key, flags).SetValue(instance, fieldInfos.Value, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance, null, null);
                }

                //Now store it in the result
                result[i] = instance as Effect;
                i++;
            }

            return result;
        }


        // Uses reflection to save all the fields that should be saved to the dict for the given effect.
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
                var a = eff.GetType();
                var b = a.GetField(info.Name);
                var v = eff.GetType().GetField(info.Name, flags);
                fields[info.Name] = eff.GetType().GetField(info.Name, flags).GetValue(eff);
            }

			return fields;

        }
    }
}
