using UnityEngine;
using System.Collections;
using Newtonsoft.Json;
using System.Text;
using System.IO;

public static class JSONMaker {

	public static string makeJSON(string type, int code, bool hasData, string[] data){

		StringBuilder sb = new StringBuilder ();
		StringWriter sw = new StringWriter (sb);

		JsonWriter writer = new JsonTextWriter (sw);

		writer.Formatting = Formatting.Indented;
		writer.WriteStartObject ();
		writer.WritePropertyName (type);
		writer.WriteStartObject ();
		writer.WritePropertyName ("code");
		writer.WriteValue (code);

		// Add the data property if hasData is true.

		if (hasData) {
			writer.WritePropertyName ("data");
			writer.WriteStartArray ();

			foreach (string d in data) {
				writer.WriteValue (d);
			}

			writer.WriteEndArray ();
		}
			
		writer.WriteEndObject ();
		writer.WriteEndObject ();


		return sb.ToString ();
	}

}
