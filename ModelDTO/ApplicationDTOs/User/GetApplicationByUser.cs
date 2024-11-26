namespace ModelDTO.ApplicationDTOs.User;

public record struct GetApplicationByUser(int ApplicationId, Guid userId);
