using System.Runtime.Serialization;

namespace Sandbox.Business.Models;

[DataContract]
public class TestModel
{
	[DataMember(Name = "id")]
	public required string Id { get; set; }
	
	[DataMember(Name = "name")]
	public string? Name { get; set; }	
}