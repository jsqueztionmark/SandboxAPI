using Sandbox.Business.Models;
using Sandbox.DAL.Entities;

namespace Sandbox.Business.Mapping;
using AutoMapper;

public class MappingProfile : Profile
{
	public MappingProfile()
	{
		CreateMap<TestEntity, TestModel>().ReverseMap();
	}
}