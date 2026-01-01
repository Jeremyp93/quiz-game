# Quiz Game - Production Deployment Guide

## Overview

This guide covers deploying the Quiz Game application to production using Docker and Traefik reverse proxy.

## Architecture

- **Single Container**: Both backend (ASP.NET Core) and frontend (React) served from one container
- **Authentication**: Cookie-based auth for Game Master, viewer code verification for display screen
- **Database**: SQLite with EF Core migrations
- **Reverse Proxy**: Traefik with automatic HTTPS via Let's Encrypt
- **Security**: Rate limiting, secure cookies, ForwardedHeaders, DataProtection persistence

## Prerequisites

1. VPS with Docker and Docker Compose installed
2. Traefik already running with:
   - External network named `traefik`
   - Let's Encrypt configured (certresolver=lets-encrypt)
   - WebSocket support enabled
3. Domain name pointing to your VPS

## Deployment Steps

### 1. Configure Environment Variables

Copy `.env.example` to `.env` and configure:

```bash
cp .env.example .env
```

Edit `.env` with your values:

```env
# Your domain name
DOMAIN=quiz.yourdomain.com

# Strong GM password (16+ characters recommended)
GM_USERNAME=admin
GM_PASSWORD=YourSecurePassword123!

# CORS allowed origins (use your actual domain)
AllowedOrigins=https://quiz.yourdomain.com
```

### 2. Create Required Directories

```bash
mkdir -p data dataprotection-keys
chmod 755 data dataprotection-keys
```

### 3. Deploy Using the Script

Make the deployment script executable and run it:

```bash
chmod +x deploy.sh
./deploy.sh
```

Or manually:

```bash
docker-compose build
docker-compose up -d
```

### 4. Verify Deployment

Check container status:
```bash
docker-compose ps
```

View logs:
```bash
docker-compose logs -f
```

Test health endpoint:
```bash
curl https://your-domain.com/health
```

### 5. Access the Application

- **GM Login**: `https://your-domain.com/login`
  - Username: Value from `GM_USERNAME` in `.env`
  - Password: Value from `GM_PASSWORD` in `.env`

- **Control Panel**: `https://your-domain.com/control` (requires GM login)

- **Display Screen**: `https://your-domain.com/display` (requires viewer code)

## Application Workflow

1. **GM logs in** at `/login`
2. **GM navigates to Control Panel** at `/control`
3. **GM clicks "Start Game"** - A 6-digit viewer code is generated and displayed
4. **Viewers access** `/display` and enter the 6-digit code
5. **GM controls the game** from Control Panel - viewers see updates in real-time

## Security Features

### Cookie-Based Authentication

- **GM cookies**: 8-hour expiration with sliding renewal, HttpOnly, Secure (in production), SameSite=Strict
- **Viewer cookies**: 24-hour expiration, automatically invalidated when new game starts
- **Session versioning**: Each "Start Game" increments session version, invalidating old viewer codes

### Rate Limiting

- **Login attempts**: 5 attempts per 5 minutes
- **Viewer code verification**: 10 attempts per 5 minutes

### Data Protection

- **Persistent keys**: Stored in `/app/dataprotection-keys` volume for cookie encryption across container restarts
- **ForwardedHeaders**: Properly handles X-Forwarded-For and X-Forwarded-Proto from Traefik

## Database Management

### Location

Database is stored in the `./data` directory (bind mount):
- File: `./data/quizgame.db`
- Directly accessible on host filesystem

### Backup

```bash
# Create backup
cp ./data/quizgame.db ./backups/quizgame-$(date +%Y%m%d-%H%M%S).db

# Restore backup
docker-compose down
cp ./backups/quizgame-TIMESTAMP.db ./data/quizgame.db
docker-compose up -d
```

### Migrations

Migrations are automatically applied on container startup in production mode.

## Maintenance

### View Logs

```bash
docker-compose logs -f quiz-game
```

### Restart Container

```bash
docker-compose restart quiz-game
```

### Update Application

```bash
git pull
docker-compose build
docker-compose up -d
```

### Stop Application

```bash
docker-compose down
```

## Troubleshooting

### Container Won't Start

Check logs:
```bash
docker-compose logs quiz-game
```

Common issues:
- Ensure `.env` file exists and is configured
- Verify Traefik network exists: `docker network ls | grep traefik`
- Check port 8080 is not in use

### Can't Access via Domain

1. Verify DNS points to your VPS: `nslookup your-domain.com`
2. Check Traefik is routing correctly: `docker-compose logs quiz-game | grep traefik`
3. Ensure DOMAIN in `.env` matches your actual domain

### SignalR WebSocket Connection Fails

1. Verify Traefik WebSocket support is enabled
2. Check `X-Forwarded-Proto` header middleware is applied
3. Ensure cookies are being sent with WebSocket upgrade request

### Viewer Code Not Working

1. Verify game has been started (code is only generated on "Start Game")
2. Check if session was invalidated (new game started)
3. Rate limiting may be blocking attempts - wait 5 minutes

## Files Structure

```
quiz-game/
├── .env                        # Environment configuration (not in git)
├── .env.example                # Environment template
├── docker-compose.yml          # Docker orchestration
├── Dockerfile.backend          # Multi-stage Docker build
├── deploy.sh                   # Deployment script
├── data/                       # Database storage (bind mount)
│   └── quizgame.db
├── dataprotection-keys/        # ASP.NET DataProtection keys (bind mount)
└── src/                        # Source code
```

## Production Checklist

Before going live:

- [ ] Set strong `GM_PASSWORD` in `.env` (16+ chars, mixed case, numbers, symbols)
- [ ] Configure `DOMAIN` in `.env` to match your actual domain
- [ ] Update `AllowedOrigins` in `.env` to your production domain
- [ ] Ensure Traefik is running with Let's Encrypt configured
- [ ] Create `data` and `dataprotection-keys` directories
- [ ] Verify health endpoint returns 200: `curl https://domain/health`
- [ ] Test GM login flow
- [ ] Test viewer code generation and verification
- [ ] Verify WebSocket connection works (real-time updates)
- [ ] Set up automated database backups

## Support

For issues or questions, check:
- Application logs: `docker-compose logs -f`
- Health endpoint: `https://your-domain.com/health`
- Backend build: `cd src/QuizGame.API && dotnet build`
