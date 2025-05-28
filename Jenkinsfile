    pipeline {
        agent any

        environment {
            DOTNET_CLI_HOME = "/tmp/DOTNET_CLI_HOME"
            DOTNET_VERSION = '8.0'
        }

        stages {
            stage('Install Prerequisites') {
                steps {
                    sh '''
                        apt-get update
                        apt-get install -y wget apt-transport-https
                    '''
                }
            }

            stage('Install .NET SDK') {
                steps {
                    sh '''
                        wget https://packages.microsoft.com/config/ubuntu/20.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
                        dpkg -i packages-microsoft-prod.deb
                        apt-get update
                        apt-get install -y dotnet-sdk-8.0
                    '''
                }
            }

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