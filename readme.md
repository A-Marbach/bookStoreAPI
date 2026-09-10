# BookStore API 

Containerized ASP.NET Core Web API with MongoDB, Docker Compose, CI/CD, security scanning and monitoring.

This project demonstrates container operation, application deployment, health checks, monitoring and troubleshooting in a Linux-based environment.

## Table of Contents

- [Quickstart](#quickstart)
- [Usage](#usage)
- [Environment Variables](#environment-variables)
- [Volumes](#volumes)
- [Security](#security)
- [GitHub Actions Pipeline](#github-actions-pipeline)
- [Monitoring](#monitoring)
- [Troubleshooting](#troubleshooting)
- [Skills Demonstrated](#skills-demonstrated)

---

## Quickstart

### Prerequisites

- Docker
- Docker Compose

### Steps

1. Clone this repository:

```bash
git clone git@github.com:A-Marbach/BookStoreApi.git
cd BookStoreApi
```

2. Start the containers:

```bash
docker compose up -d
```

3. Test the API:

```bash
curl http://localhost:8080/api/books
```

---

## Usage

The API provides full CRUD operations for a book store:

| Method | Endpoint        | Description     |
|--------|-----------------|-----------------|
| GET    | `/api/books`      | Get all books   |
| GET    | `/api/books/{id}` | Get book by ID  |
| POST   | `/api/books`      | Create new book |
| PUT    | `/api/books/{id}` | Update book     |
| DELETE | `/api/books/{id}` | Delete book     |

---

## Environment Variables

| Variable                                 | Description               | Default                   |
|------------------------------------------|---------------------------|---------------------------|
| `BookStoreDatabase__ConnectionString`    | MongoDB connection string | `mongodb://mongodb:27017` |
| `BookStoreDatabase__DatabaseName`        | Database name             | `BookStore`               |
| `BookStoreDatabase__BooksCollectionName` | Collection name           | `Books`                   |

---

## Volumes

- `mongodb_data` – Persistent MongoDB storage that survives container restarts and container recreation

```bash
# Restart containers
docker compose restart

# Stop and remove containers
docker compose down

# Remove containers and volumes (deletes database data!)
docker compose down -v
```

---

## Security

### Security Practices

- Do not commit `.env` files or credentials to the repository
- Do not hardcode connection strings or passwords
- Use GitHub Secrets for all sensitive values
- Use multi-stage Docker builds to reduce image size and attack surface

### Security Pipeline Flow

```text
Push to GitHub
       ↓
Stage 1: Dockerfile Linting (Hadolint)
       ↓
Stage 2: Build & Push Docker Image (GHCR)
       ↓
Stage 3: Image Vulnerability Scan (Trivy)
       ↓
Stage 4: Deploy to VM
```

### Hadolint – Dockerfile Linting

- Checks Dockerfile for best practices and common mistakes
- Fails pipeline on any error-level finding

### Trivy – Image Scanning

- Scans the built Docker image for known CVEs
- Checks OS packages and application dependencies
- Severity threshold: HIGH and CRITICAL

---

## GitHub Actions Pipeline

The GitHub Actions workflow builds, validates, scans and deploys the application automatically.

### Workflow Overview

```
1. Code Push to GitHub
   ↓
2. Build .NET Application
   ↓
3. Lint Dockerfile (Hadolint)
   ↓
4. Build & Push Docker Image to GHCR
   ↓
5. Scan Image (Trivy)
   ↓
6. Deploy to VM via SSH
```

### Required Secrets

| Secret | Description |
|--------|-------------|
| `ghcr_token` | GitHub Personal Access Token for GHCR |
| `SSH_HOST` | VM IP address |
| `SSH_USER` | SSH username |
| `SSH_KEY` | SSH private key |
| `SSH_PORT` | SSH port (default: 22) |

### Workflow File

`.github/workflows/deployment.yaml`

---

## Monitoring

The application exposes metrics and health checks for basic operational monitoring and troubleshooting.

- Prometheus scrapes `/metrics` every 15 seconds
- Grafana dashboard shows Memory, CPU and Request Rate
- Health check endpoint: `GET /health` – returns `Healthy` when API is up
- MongoDB health check via `mongosh ping` – checks database connectivity
- Container health status visible via `docker ps`

---

## Troubleshooting

### Port already in use

Check running containers:

```bash
docker ps
```
Stop the conflicting container if necessary:

```bash
docker stop <container-id>
docker compose up -d
```

### MongoDB connection error

Check application and database logs:

```bash
docker compose logs api
docker compose logs mongodb
```

### Check container health

```bash
docker compose ps
```

For detailed health information:

```bash
docker inspect <container-name>
```

---

## Skills Demonstrated

- Linux-based Application Operations
- Docker and Docker Compose
- MongoDB
- CI/CD with GitHub Actions
- Container Image Security Scanning
- Prometheus Monitoring
- Grafana Dashboards
- Health Checks
- Log Analysis
- Container Troubleshooting
- SSH-based Deployment