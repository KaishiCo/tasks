using Application.Interfaces;

namespace Api.Endpoints;

public static class TaskItemsEndpoints
{
    public static void MapTaskItemsEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/taskItems", async (ITaskItemRepository taskItemRepository) =>
        {
            var taskItems = await taskItemRepository.GetAllAsync();
            return Results.Ok(taskItems);
        });
    }
}
