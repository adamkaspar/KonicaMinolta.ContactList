namespace KonicaMinolta.Shared.Domain.Contacts;

public record ContactResultDto(int Id, string Name, string Surname, int Age, int Phone, bool IsActive) : ContactBaseDto(Name, Surname, Age, Phone, IsActive);