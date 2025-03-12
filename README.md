# Sales-Date-Prediction

## Descripción
Sales Date Prediction es una aplicación basada en web para predecir la fecha de la próxima orden de un cliente utilizando el historial de pedidos. La aplicación está construida con **Angular 19** para el frontend y **ASP.NET Core 9** para el backend, utilizando **Entity Framework Core** para la gestión de datos.

## Características Principales

### 📚 **Frontend (Angular 19 + TypeScript)**
- UI con **Angular Material** y Bootstrap 5.
- Formas reactivas para la gestión de datos.
- Páginas preconfiguradas con estructura modular.
- Servicios de diálogo y notificaciones.
- Autenticación basada en **OIDC/OAuth2**.
- Internacionalización y tematización con **SASS**.
- CLI de Angular para administración eficiente.
- Animaciones y directivas personalizadas.
![image](https://github.com/user-attachments/assets/1b81772c-0f1d-4271-95c0-03e17484839a)

![image](https://github.com/user-attachments/assets/8292268e-67da-48fb-8a4a-d06a32c7b223)

![image](https://github.com/user-attachments/assets/b839763a-df75-48dc-ac07-796409fa00da)




### 🛠️ **Backend (ASP.NET Core 9 + EF Core)**
- API RESTful siguiendo principios de diseño REST.
- Documentación de API con **Swagger**.
- Autenticación/autorización con **ASP.NET Identity**.
- Manejo de tokens de acceso y refresco con **WebStorage**.
- API de correo electrónico para confirmación, recuperación y restablecimiento de contraseña.
- Base de datos **Code First** con migraciones automáticas.
- Patrón **Repositorio + Unidad de Trabajo** para acceso a datos.
- API CRUD para gestión de clientes y pedidos.

## 🛠️ Instalación y Configuración

### **Requisitos Previos**
- **Node.js** (versión 16+)
- **Angular CLI**
- **.NET SDK 9**
- **SQL Server**
- **Docker (Opcional para despliegue)**

### **1. Clonar el Repositorio**
```sh
git clone https://github.com/Camila-1996/Sales-Date-Prediction.git
cd sales-date-prediction
```

### **2. Configurar el Backend**
```sh
cd backend

# Restaurar dependencias
 dotnet restore

# Aplicar migraciones de la base de datos
 dotnet ef database update

# Ejecutar el backend
 dotnet run
```

### **3. Configurar el Frontend**
```sh
cd frontend

# Instalar dependencias
npm install

# Ejecutar la aplicación
ng serve --open
```

## 📊 Estructura del Proyecto
```
/sales-date-prediction
├── backend/ (ASP.NET Core 9)
│   ├── Controllers/ (Controladores API)│   
│   ├── Startup.cs (Configuración inicial)
│   └── appsettings.json (Configuración de base de datos y autenticación)
│
├── frontend/ (Angular 19)
│   ├── src/app/components/ (Componentes de UI)
│   ├── src/app/services/ (Servicios HTTP y autenticación)
│   ├── src/assets/ (Recursos estáticos)
│   ├── src/environments/ (Configuración de entornos)
│   └── angular.json (Configuración de Angular CLI)
│
└── README.md (Este archivo)
```

## 🚸️ Roles y Permisos
La aplicación cuenta con gestión de usuarios basada en roles:
- **Admin**: Gestión completa del sistema.
- **Usuario Registrado**: Puede ver su historial de compras y predicciones.

## 👁 Autenticación y Seguridad
- Autenticación con **JWT Bearer Token**.
- Protección de rutas con **Guards en Angular**.
- Hashing de contraseñas con **ASP.NET Identity**.
- Seguridad CORS configurada en el backend.

## 📈 Tecnologías Usadas
- **Frontend:** Angular 19, TypeScript, Angular Material, Bootstrap 5
- **Backend:** ASP.NET Core 9, Entity Framework Core, SQL Server
- **Autenticación:** OAuth2, OIDC, ASP.NET Identity



