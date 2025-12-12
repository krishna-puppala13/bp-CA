# Blood Pressure Category Calculator – CI/CD Pipeline
# Project Overview

This project is an ASP.NET Core Razor Pages application that calculates a user’s blood pressure category based on systolic and diastolic values. The application classifies blood pressure as Low, Ideal, Pre-High, or High and provides a short advice message for each category.

The main focus of this project is the implementation of a complete CI/CD pipeline, including testing, security scanning, performance testing, telemetry, and safe deployment strategies.

# Features

Blood pressure category calculation

Input validation (systolic must be greater than diastolic)

Personalised advice message for each category

Azure Application Insights telemetry

Automated CI/CD pipeline using GitHub Actions

# CI/CD Pipeline

The project uses GitHub Actions to automate build, testing, security checks, and deployment.

# Continuous Integration (CI)

Restore dependencies and build application

Unit testing (MSTest)

Behaviour-Driven Development (BDD) testing using Reqnroll

Code quality and coverage analysis using SonarCloud

Dependency vulnerability scanning using OWASP Dependency-Check

# Continuous Deployment (CD)

Deployment to Azure App Service staging slot

End-to-End testing using Playwright

Performance testing using k6

Security testing using OWASP ZAP baseline scan

Manual approval gate before production release

Blue-Green deployment using Azure slot swap

# Testing Overview

Unit Tests: Validate business logic, boundaries, validation rules, and advice messages

BDD Tests: Validate behaviour using Gherkin scenarios

E2E Tests: Validate full user flow in a deployed environment

Performance Tests: Validate response time and stability under load

Security Tests: Identify common vulnerabilities and dependency risks

Performance Testing

Performance tests are executed using k6 against the staging environment.


Security

Security is integrated throughout the pipeline:

Dependency scanning for known vulnerabilities

Automated penetration testing using OWASP ZAP

Secure deployment using approval gates

Runtime monitoring with Azure Application Insights

# Deployment Strategy

The application uses a blue-green deployment strategy with Azure App Service deployment slots. New versions are deployed to a staging slot and validated before being promoted to production using a slot swap. This ensures zero downtime and safe releases.

# Telemetry and Monitoring

Azure Application Insights is used to monitor:

Application requests

Performance metrics

Errors and exceptions

This provides visibility into application health after deployment.

# How to Run Locally

Clone the repository

Open the solution in Visual Studio or VS Code

Run the application using:

dotnet run


Navigate to http://localhost:5000
