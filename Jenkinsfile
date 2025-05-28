pipeline {
    agent any

    environment {
        DOTNET_CLI_HOME = "/tmp/DOTNET_CLI_HOME"
        DOTNET_VERSION = '7.0'
    }

    stages {
        stage('Checkout') {
            steps {
                checkout scm
            }
        }

        stage('Restore Dependencies') {
            steps {
                dir('src/Presentation') {
                    sh 'dotnet restore'
                }
            }
        }

        stage('Build') {
            steps {
                dir('src/Presentation') {
                    sh 'dotnet build --configuration Release --no-restore'
                }
            }
        }

        stage('Test') {
            steps {
                dir('src/Presentation') {
                    sh 'dotnet test --no-restore --verbosity normal'
                }
            }
        }

        stage('Docker Build') {
            steps {
                sh 'docker-compose build'
            }
        }
        
        stage('Deploy') {
            steps {
                sh 'docker-compose up -d'
            }
        }
    }

    post {
        always {
            cleanWs()
        }
        success {
            echo 'Build and tests completed successfully!'
        }
        failure {
            echo 'Build or tests failed!'
        }
    }
}