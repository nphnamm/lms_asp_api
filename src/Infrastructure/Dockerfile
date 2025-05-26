FROM mcr.microsoft.com/dotnet/sdk:8.0.0

WORKDIR /src

# Copy project files
COPY ["src/Infrastructure/Infrastructure.csproj", "src/Infrastructure/"]
COPY ["src/Application/Application.csproj", "src/Application/"]
COPY ["src/Domain/Domain.csproj", "src/Domain/"]
COPY ["src/Presentation/Presentation.csproj", "src/Presentation/"]

# Restore packages
RUN dotnet restore "src/Infrastructure/Infrastructure.csproj"

# Copy the rest of the code
COPY . .

# Install EF Core tools
RUN dotnet tool install --global dotnet-ef
ENV PATH="${PATH}:/root/.dotnet/tools"

WORKDIR "/src/src/Infrastructure" 