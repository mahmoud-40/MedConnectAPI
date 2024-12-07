using AutoMapper;
using Medical.DTOs.Account;
using Medical.DTOs.Appointments;
using Medical.DTOs.Notifications;
using Medical.DTOs.Doctors;
using Medical.DTOs.Providers;
using Medical.DTOs.Records;
using Medical.Models;
using Medical.Utils;
using Medical.DTOs.Patients;

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
        CreateMap<Patient, ProfilePatientDTO>()
            .ForMember(dest => dest.UpcomingAppointments, opt => opt.MapFrom(src => src.Appointments.Where(a => a.Date >= DateOnly.FromDateTime(DateTime.Today) && a.Date <= DateOnly.FromDateTime(DateTime.Today.AddDays(7)))))
            .ForMember(dest => dest.UnreadNotifications, opt => opt.MapFrom(src => src.Notifications.Where(n => !n.IsSeen)));
        CreateMap<UpdatePatientDTO, Patient>()
            .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

        CreateMap<Provider, ViewProviderDTO>()
            .ForMember(dest => dest.MemberSince, opt => opt.MapFrom(src => Converter.CalcDuration(src.CreatedAt, null)))
            .ForMember(dest => dest.Doctors, opt => opt.MapFrom(src => src.Doctors.Select(d => new AddDoctorDTO
            {
                FullName = d.FullName,
                Title = d.Title,
                HireDate = d.HireDate,
                YearExperience = d.YearExperience
            }).ToList()));
        CreateMap<UpdateProviderDTO, Provider>()
            .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

        CreateMap<Doctor, ViewDoctorDTO>()
            .ForMember(dest => dest.ProviderName, opt => opt.MapFrom(src => src.Provider!.Name));
        CreateMap<AddDoctorDTO, Doctor>()
            .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
        CreateMap<UpdateDoctorDTO, Doctor>()
            .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

        CreateMap<Notification, ViewNotificationDTO>()
            .ForMember(dest => dest.Since, opt => opt.MapFrom(src => Converter.CalcDuration(src.ReleaseDate, null)));
        
        CreateMap<Record, ViewRecordDTO>()
            .ForMember(dest => dest.Since, opt => opt.MapFrom(src => Converter.CalcDuration(src.CreatedAt, null)));
        CreateMap<Record, ViewRecordByPatientDTO>()
            .ForMember(dest => dest.Provider, opt => opt.MapFrom(src => src.Provider));
        CreateMap<Record, ViewRecordByProviderDTO>()
            .ForMember(dest => dest.Patient, opt => opt.MapFrom(src => src.Patient));
        CreateMap<AddRecordDTO, Record>();
        CreateMap<AddRecordByProviderDTO, Record>();
        CreateMap<UpdateRecordDTO, Record>()
            .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
        CreateMap<UpdateRecordByProviderDTO, Record>()
            .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
        
        CreateMap<Appointment, ViewAppointmentDTO>()
            .ForMember(dest => dest.PatientName, opt => opt.MapFrom(src => src.Patient!.Name ?? src.Patient.UserName))
            .ForMember(dest => dest.DoctorName, opt => opt.MapFrom(src => src.Doctor!.FullName))
            .ForMember(dest => dest.DisplayRecord, opt => opt.MapFrom(src => src.Patient!.Records.SingleOrDefault(r => r.ProviderId == src.Doctor!.ProviderId)));
    }
}
