#!/bin/bash
set -e

echo "=== Quiz Game Deployment Script ==="
echo ""

# Check if .env exists
if [ ! -f .env ]; then
    echo "ERROR: .env file not found!"
    echo "Please copy .env.example to .env and configure it."
    exit 1
fi

# Create required directories
echo "Creating required directories..."
mkdir -p data dataprotection-keys

# Set proper permissions
echo "Setting directory permissions..."
chmod 755 data dataprotection-keys

# Build and start containers
echo "Building Docker image..."
docker-compose build

echo "Starting containers..."
docker-compose up -d

echo ""
echo "=== Deployment Complete ==="
echo ""
echo "Checking container status..."
docker-compose ps

echo ""
echo "Viewing logs (press Ctrl+C to exit)..."
docker-compose logs -f
