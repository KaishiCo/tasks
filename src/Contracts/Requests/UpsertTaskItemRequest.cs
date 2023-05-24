namespace Contracts.Requests;

public record UpsertTaskItemRequest(
    string Name,
    string Description,
    DateTime Date,
    bool IsCompleted);
