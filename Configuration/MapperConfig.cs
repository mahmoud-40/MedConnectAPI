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
    public MapperConfig()
    {
        var scope = ServiceProviderHolder.ServiceProvider?.CreateScope();
        IConverter converter = scope?.ServiceProvider.GetRequiredService<IConverter>() ?? throw new ArgumentNullException($"{nameof(IConverter)} is not registered");

        CreateMap<RegisterDTO, Patient>();
        CreateMap<RegisterDTO, Provider>();

        CreateMap<Patient, ViewPatientDTO>()
            .ForMember(dest => dest.Age, opt => opt.MapFrom(src => converter.GetAge(src.BirthDay)))
            .ForMember(dest => dest.MemberSince, opt => opt.MapFrom(src => converter.CalcDuration(src.CreatedAt, null)));
        CreateMap<Patient, ProfilePatientDTO>()
            .ForMember(dest => dest.Age, opt => opt.MapFrom(src => converter.GetAge(src.BirthDay)))
            .ForMember(dest => dest.MemberSince, opt => opt.MapFrom(src => converter.CalcDuration(src.CreatedAt, null)))
            .ForMember(dest => dest.UpcomingAppointments, opt => opt.MapFrom(src => src.Appointments.Where(a => a.Date >= DateOnly.FromDateTime(DateTime.Today) && a.Date <= DateOnly.FromDateTime(DateTime.Today.AddDays(7)))))
            .ForMember(dest => dest.UnreadNotifications, opt => opt.MapFrom(src => src.Notifications.Where(n => !n.IsSeen)));
        CreateMap<UpdatePatientDTO, Patient>()
            .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

        CreateMap<Provider, ViewProviderDTO>()
            .ForMember(dest => dest.MemberSince, opt => opt.MapFrom(src => converter.CalcDuration(src.CreatedAt, null)))
            .ForMember(dest => dest.Doctors, opt => opt.MapFrom(src => src.Doctors));
        CreateMap<UpdateProviderDTO, Provider>()
            .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

        CreateMap<Doctor, ViewDoctorDTO>()
            .ForMember(dest => dest.ProviderName, opt => opt.MapFrom(src => src.Provider!.Name));
        CreateMap<AddDoctorDTO, Doctor>()
            .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
        CreateMap<UpdateDoctorDTO, Doctor>()
            .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

        CreateMap<Notification, ViewNotificationDTO>()
            .ForMember(dest => dest.Since, opt => opt.MapFrom(src => converter.CalcDuration(src.ReleaseDate, null)));
        
        CreateMap<Record, ViewRecordDTO>()
            .ForMember(dest => dest.Since, opt => opt.MapFrom(src => converter.CalcDuration(src.CreatedAt, null)))
            .ForMember(dest => dest.LastUpdate, opt => opt.MapFrom(src => converter.CalcDuration(src.UpdatedAt, null)));
        CreateMap<Record, ViewRecordByPatientDTO>()
            .ForMember(dest => dest.Provider, opt => opt.MapFrom(src => src.Provider))
            .ForMember(dest => dest.Since, opt => opt.MapFrom(src => converter.CalcDuration(src.CreatedAt, null)))
            .ForMember(dest => dest.LastUpdate, opt => opt.MapFrom(src => converter.CalcDuration(src.UpdatedAt, null)));
        CreateMap<Record, ViewRecordByProviderDTO>()
            .ForMember(dest => dest.Patient, opt => opt.MapFrom(src => src.Patient))
            .ForMember(dest => dest.Since, opt => opt.MapFrom(src => converter.CalcDuration(src.CreatedAt, null)))
            .ForMember(dest => dest.LastUpdate, opt => opt.MapFrom(src => converter.CalcDuration(src.UpdatedAt, null)));
        CreateMap<AddRecordDTO, Record>();
        CreateMap<AddRecordByProviderDTO, Record>();
        CreateMap<UpdateRecordDTO, Record>()
            .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
        CreateMap<UpdateRecordByProviderDTO, Record>()
            .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

        CreateMap<Appointment, ViewAppointmentDTO>()
            .ForMember(dest => dest.PatientName, opt => opt.MapFrom(src => src.Patient!.Name ?? src.Patient.UserName))
            .ForMember(dest => dest.DoctorName, opt => opt.MapFrom(src => src.Doctor!.FullName))
            .ForMember(dest => dest.ProviderName, opt => opt.MapFrom(src => src.Doctor!.Provider!.Name))
            .ForMember(dest => dest.Time, opt => opt.MapFrom(src => converter.CalcTime(src.Time, src.Doctor!.Provider!.Shift)))
            .ForMember(dest => dest.DisplayRecord, opt => opt.MapFrom(src => src.Patient!.Records.SingleOrDefault(r => r.ProviderId == src.Doctor!.ProviderId)));
        CreateMap<AddAppointmentDTO, Appointment>();
        
    }
}
