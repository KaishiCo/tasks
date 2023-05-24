# Docs

## Note
- The [Desktop Application](https://github.com/KaishiCo/Java-GUI-Application) interfacing this project, created and maintained by Damien ([ArduousKokuhaku52](https://github.com/ArduousKokuhaku52))

## Running the project locally

- Open a terminal window and navigate to the directory where the `docker-compose.yml` file is located.

```bash
cd /path/to/the/docker-compose.yml
```

- Run the following command to start the api and database containers:

```bash
docker compose up -d
```

## Cleaning up

- To stop the containers:

```bash
docker compose down
```

- Or, to stop the containers and remove the volumes:

```bash
docker compose down -v
```
