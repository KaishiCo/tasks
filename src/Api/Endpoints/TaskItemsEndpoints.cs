using Api.Extensions;
using Application.Interfaces;
using Application.Models;
using Contracts.Requests;

namespace Api.Endpoints;

public static class TaskItemsEndpoints
{
    public static void MapTaskItemsEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/taskItems", async (ITaskItemRepository taskItemRepository) =>
        {
            var taskItems = await taskItemRepository.GetAllAsync();
            return Results.Ok(taskItems);
        })
        .RequireAuthorization();

        app.MapPost("/api/taskItems", async (UpsertTaskItemRequest request, ITaskItemRepository taskItemRepository, HttpContext httpCtx) =>
        {
            var taskItem = new TaskItem
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Description = request.Description,
                Date = request.Date,
                IsCompleted = request.IsCompleted,
                UserId = Guid.Parse(httpCtx.GetUserId())
            };

            var created = await taskItemRepository.CreateAsync(taskItem);
            if (!created)
                return Results.BadRequest();
            return Results.Created($"/api/taskItems/{taskItem.Id}", taskItem);
        })
        .RequireAuthorization();

        app.MapDelete("/api/taskItems/{id:guid}", async (Guid id, ITaskItemRepository taskItemRepository, HttpContext httpCtx) =>
        {
            var taskItem = await taskItemRepository.GetByIdAsync(id);
            if (taskItem is null)
                return Results.NotFound();

            var userId = httpCtx.GetUserId();
            if (taskItem.UserId != Guid.Parse(userId))
                return Results.Forbid();

            await taskItemRepository.DeleteAsync(id);
            return Results.Ok();
        })
        .RequireAuthorization();

        app.MapPut("/api/taskItems/{id:guid}", async (Guid id, UpsertTaskItemRequest request, ITaskItemRepository taskItemRepository, HttpContext httpCtx) =>
        {
            var taskItem = await taskItemRepository.GetByIdAsync(id);
            if (taskItem is null)
                return Results.NotFound();

            if (taskItem.UserId != Guid.Parse(httpCtx.GetUserId()))
                return Results.Forbid();

            taskItem.Name = request.Name;
            taskItem.Description = request.Description;
            taskItem.Date = request.Date;
            taskItem.IsCompleted = request.IsCompleted;

            var updated = await taskItemRepository.UpdateAsync(taskItem);
            if (!updated)
                return Results.BadRequest();
            return Results.Ok(taskItem);
        })
        .RequireAuthorization();
    }
}
