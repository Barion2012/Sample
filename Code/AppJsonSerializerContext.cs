using Sample.Models;
using System.Text.Json.Serialization;
namespace Sample.Code;


[JsonSerializable(typeof(string))]
[JsonSerializable(typeof(Job))]
[JsonSerializable(typeof(JobData))]
internal partial class AppJsonSerializerContext : JsonSerializerContext
{
}
