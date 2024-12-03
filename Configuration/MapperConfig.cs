using AutoMapper;
using Medical.DTOs.Account;
using Medical.DTOs.Patients;
using Medical.DTOs.ProvidersDTOs;
using Medical.Models;
using Medical.Utils;

namespace Medical.Configuration;

public class MapperConfig : Profile
{
    public MapperConfig(IConverter converter)
    {
        CreateMap<RegisterDTO, Patient>();
        CreateMap<RegisterDTO, Provider>();

        CreateMap<Patient, ViewPatientDTO>()
            .ForMember(dest => dest.Age, opt => opt.MapFrom(src => converter.GetAge(src.BirthDay)))
            .ForMember(dest => dest.MemberSince, opt => opt.MapFrom(src => converter.CalcDuration(src.CreatedAt, null)));
        CreateMap<UpdatePatientDTO, Patient>();

        CreateMap<Provider, DisplayProviderDTO>();
        CreateMap<Doctor, AddDoctorToProviderDTO>();
    }
}
