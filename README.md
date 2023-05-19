# Docs

## Running the project locally

- Open a terminal window and navigate to the directory where the `docker-compose.yml` file is located.

```bash
cd /path/to/the/docker-compose.yml
```

- Run the following command to start the database container:

```bash
docker compose up -d
```

- Run the following command to start the application:

```bash
dotnet run --project src/Api
```

## Cleaning up

- To stop the database container:

```bash
docker compose down
```
