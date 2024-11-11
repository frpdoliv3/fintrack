namespace FinTrack.Application.Security.EntitiesBase;

public interface IHasOwnerId
{
    string? OwnerId { get; set; }
}