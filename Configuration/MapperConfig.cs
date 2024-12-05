using AutoMapper;
using Medical.DTOs.Account;
using Medical.DTOs.Notification;
using Medical.DTOs.Patients;
using Medical.DTOs.Providers;
using Medical.DTOs.ProvidersDTOs;
using Medical.Models;
using Medical.Utils;

namespace Medical.Configuration;

public class MapperConfig : Profile
{
    public static IConverter Converter { get; } = new Converter();

    public MapperConfig()
    {
        CreateMap<RegisterDTO, Patient>();
        CreateMap<RegisterDTO, Provider>();

        CreateMap<Patient, ViewPatientDTO>()
            .ForMember(dest => dest.Age, opt => opt.MapFrom(src => Converter.GetAge(src.BirthDay)))
            .ForMember(dest => dest.MemberSince, opt => opt.MapFrom(src => Converter.CalcDuration(src.CreatedAt, null)));
        CreateMap<UpdatePatientDTO, Patient>()
            .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

        CreateMap<Provider, DisplayProviderDTO>()
            .ForMember(dest => dest.MemberSince, opt => opt.MapFrom(src => Converter.CalcDuration(src.CreatedAt, null)))
            .ForMember(dest => dest.Doctors, opt => opt.MapFrom(src => src.Doctors.Select(d => new AddDoctorToProviderDTO
            {
                FullName = d.FullName,
                Title = d.Title,
                HireDate = d.HireDate,
                YearExperience = d.YearExperience
            }).ToList()));
        CreateMap<UpdateProviderDTO, Provider>()
            .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

        CreateMap<Doctor, AddDoctorToProviderDTO>();
        CreateMap<AddDoctorToProviderDTO, Doctor>();

        CreateMap<Notification, ViewNotificationDTO>()
            .ForMember(dest => dest.Since, opt => opt.MapFrom(src => Converter.CalcDuration(src.ReleaseDate, null)));
    }
}
