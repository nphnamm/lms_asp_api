pipeline {
    agent any

    stages {
        stage('Pull latest code') {
            steps {
                echo 'Pulling latest changes...'
                sh '''
                    git fetch origin
                    git reset --hard origin/main
                '''
            }
        }

        stage('Docker Compose Build') {
            steps {
                echo 'Building Docker images...'
                sh 'docker-compose build'
            }
        }

        stage('Docker Compose Up') {
            steps {
                echo 'Starting containers...'
                sh 'docker-compose up -d'
            }
        }
    }
}
