namespace KonicaMinolta.Shared.Domain.Contacts;

public record ContactBaseDto(string Name, string Surname, int Age, int Phone, bool IsActive);