# BookStore API – DevSecOps Project

  A containerized ASP.NET Core Web API with MongoDB, featuring an automated
  CI/CD pipeline with enterprise-grade security scanning.

  ## Table of Contents
  - [Quickstart](#quickstart)
  - [Usage](#usage)
  - [Environment Variables](#environment-variables)
  - [Volumes](#volumes)
  - [Security](#security)
  - [GitHub Actions Pipeline](#github-actions-pipeline)
  - [Troubleshooting](#troubleshooting)

  ## Quickstart

  ### Prerequisites
  - Docker
  - Docker Compose
  - GitHub Actions enabled (for automated build & deployment)

  ### Steps
  1. Clone this repository:
  ```bash
  git clone git@github.com:A-Marbach/BookStoreApi.git
  cd BookStoreApi

  2. Start the containers:
  docker compose up -d

  3. Test the API:
  curl http://localhost:8080/api/books

  Usage

  The API provides full CRUD operations for a book store:

  ┌────────┬─────────────────┬─────────────────┐
  │ Method │    Endpoint     │   Description   │
  ├────────┼─────────────────┼─────────────────┤
  │ GET    │ /api/books      │ Get all books   │
  ├────────┼─────────────────┼─────────────────┤
  │ GET    │ /api/books/{id} │ Get book by ID  │
  ├────────┼─────────────────┼─────────────────┤
  │ POST   │ /api/books      │ Create new book │
  ├────────┼─────────────────┼─────────────────┤
  │ PUT    │ /api/books/{id} │ Update book     │
  ├────────┼─────────────────┼─────────────────┤
  │ DELETE │ /api/books/{id} │ Delete book     │
  └────────┴─────────────────┴─────────────────┘

  Environment Variables

  ┌────────────────────────────────────────┬───────────────────────────┬─────────────────────────┐
  │                Variable                │        Description        │         Default         │
  ├────────────────────────────────────────┼───────────────────────────┼─────────────────────────┤
  │ BookStoreDatabase__ConnectionString    │ MongoDB connection string │ mongodb://mongodb:27017 │
  ├────────────────────────────────────────┼───────────────────────────┼─────────────────────────┤
  │ BookStoreDatabase__DatabaseName        │ Database name             │ BookStore               │
  ├────────────────────────────────────────┼───────────────────────────┼─────────────────────────┤
  │ BookStoreDatabase__BooksCollectionName │ Collection name           │ Books                   │
  └────────────────────────────────────────┴───────────────────────────┴─────────────────────────┘

  Volumes

  - mongodb_data: Persistent MongoDB storage – data survives container restarts

  # Restart containers
  docker compose restart

  # Stop and remove containers
  docker compose down

  # Remove including volumes (deletes data!)
  docker compose down -v

  Security

  Best Practices

  - ❌ Do not commit .env files or credentials to the repository
  - ❌ Do not hardcode connection strings or passwords
  - ✅ Use GitHub Secrets for all sensitive values
  - ✅ Use Multi-Stage Builds to keep images small and secure

  Security Pipeline Flow

  Push to GitHub
         ↓
  Stage 1: Dockerfile Linting (Hadolint) 🔴 CRITICAL
         ↓
  Stage 2: Build & Push Docker Image (GHCR)
         ↓
  Stage 3: Image Vulnerability Scan (Trivy) 🔴 CRITICAL
         ↓
  Deploy to VM

  Hadolint – Dockerfile Linting

  - Checks Dockerfile for best practices and common mistakes
  - Fails pipeline on any error-level finding
  - Status: 🔴 CRITICAL

  Trivy – Image Scanning

  - Scans the built Docker image for known CVEs
  - Checks OS packages and application dependencies
  - Severity threshold: HIGH and CRITICAL
  - Status: 🔴 CRITICAL

  GitHub Actions Pipeline

  Workflow Overview

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

  Pipeline Flow Diagram

 
  Required Secrets

  ┌────────────┬───────────────────────────────────────┐
  │   Secret   │              Description              │
  ├────────────┼───────────────────────────────────────┤
  │ ghcr_token │ GitHub Personal Access Token for GHCR │
  ├────────────┼───────────────────────────────────────┤
  │ SSH_HOST   │ VM IP address                         │
  ├────────────┼───────────────────────────────────────┤
  │ SSH_USER   │ SSH username                          │
  ├────────────┼───────────────────────────────────────┤
  │ SSH_KEY    │ SSH private key                       │
  ├────────────┼───────────────────────────────────────┤
  │ SSH_PORT   │ SSH port (default: 22)                │
  └────────────┴───────────────────────────────────────┘

  Workflow File

  - Location: .github/workflows/pipeline.yaml

  Troubleshooting

  Port already in use

  docker ps
  docker stop <container-id>
  docker compose up -d

  MongoDB connection error

  docker logs bookstore-api-api-1
  docker logs bookstore-api-mongodb-1

  Pipeline failed: Hadolint error


  ---
