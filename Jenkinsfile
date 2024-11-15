pipeline {

    agent any
    stages {
        stage('Build') {
            steps {
                echo 'Building'
                sh 'make build'
            }
        }
        stage('Test') {
            steps {
                echo 'Testing'
            }
        }
    }
}
