# Forgot Password Functionality Setup Guide

## Overview
The forgot password functionality has been successfully implemented using ASP.NET Core Identity and email notifications.

## Features Implemented

### 1. Two New API Endpoints

#### Forgot Password
- **Endpoint**: `POST /api/auth/forgot-password`
- **Request Body**:
```json
{
  "email": "user@example.com"
}
```
- **Response**: Returns a generic message for security (doesn't reveal if email exists)
- **Behavior**: Generates a password reset token and sends an email with a reset link

#### Reset Password
- **Endpoint**: `POST /api/auth/reset-password`
- **Request Body**:
```json
{
  "email": "user@example.com",
  "token": "reset-token-from-email",
  "newPassword": "newPassword123"
}
```
- **Response**: Success or error message
- **Behavior**: Validates the token and updates the user's password

### 2. Email Service
- Created `IEmailService` interface and `EmailService` implementation
- Supports SMTP email sending with configurable settings
- Includes error handling and logging

### 3. DTOs Created
- `ForgotPasswordRequestDTO` - For forgot password requests
- `ResetPasswordRequestDTO` - For password reset with token

## Configuration Required

### 1. Email Settings (appsettings.json)

Update the following settings in your `appsettings.json`:

```json
"EmailSettings": {
  "SmtpServer": "smtp.gmail.com",
  "Port": "587",
  "SenderEmail": "your-email@gmail.com",
  "SenderName": "MR Tracking System",
  "Username": "your-email@gmail.com",
  "Password": "your-app-password",
  "EnableSSL": "true"
}
```

#### For Gmail Users:
1. Enable 2-factor authentication on your Google account
2. Generate an App Password:
   - Go to Google Account Settings → Security → 2-Step Verification → App passwords
   - Create a new app password
   - Use this app password in the configuration (not your regular password)

#### For Other Email Providers:
- **Outlook/Hotmail**: 
  - SmtpServer: `smtp-mail.outlook.com`
  - Port: `587`
  
- **Yahoo**: 
  - SmtpServer: `smtp.mail.yahoo.com`
  - Port: `587`

- **Custom SMTP**: Configure according to your provider's documentation

### 2. Frontend URL Configuration

Update the frontend URL in `appsettings.json`:

```json
"AppSettings": {
  "FrontendUrl": "http://localhost:3000"
}
```

This URL is used to generate the password reset link that will be sent in the email.

## Testing the Functionality

### 1. Test Forgot Password

**Request:**
```bash
POST http://localhost:5025/api/auth/forgot-password
Content-Type: application/json

{
  "email": "testuser@example.com"
}
```

**Expected Response:**
```
"If your email is registered, you will receive a password reset link."
```

### 2. Check Email

The user should receive an email with:
- Subject: "Password Reset Request"
- A link like: `http://localhost:3000/reset-password?email=testuser@example.com&token=<encoded-token>`

### 3. Test Reset Password

**Request:**
```bash
POST http://localhost:5025/api/auth/reset-password
Content-Type: application/json

{
  "email": "testuser@example.com",
  "token": "<token-from-email>",
  "newPassword": "NewPassword123"
}
```

**Expected Response (Success):**
```
"Password has been reset successfully."
```

## Security Features Implemented

1. **Token Expiration**: Reset tokens expire after 24 hours (ASP.NET Identity default)
2. **Generic Responses**: Doesn't reveal whether an email exists in the system
3. **URL Encoding**: Tokens are properly encoded/decoded to handle special characters
4. **Secure Token Generation**: Uses ASP.NET Core Identity's built-in token provider
5. **Password Validation**: Respects the password policy configured in Identity options

## Frontend Integration

Your frontend should:

1. **Forgot Password Page**:
   - Show a form with email input
   - Call `POST /api/auth/forgot-password`
   - Display a message to check email

2. **Reset Password Page**:
   - Parse the email and token from the URL query parameters
   - Show a form with new password input
   - Call `POST /api/auth/reset-password` with email, token, and new password
   - Redirect to login on success

### Example Frontend Flow (React/Angular/Vue)

```javascript
// Forgot Password Page
async function handleForgotPassword(email) {
  const response = await fetch('http://localhost:5025/api/auth/forgot-password', {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify({ email })
  });
  const message = await response.text();
  alert(message);
}

// Reset Password Page
async function handleResetPassword(email, token, newPassword) {
  const response = await fetch('http://localhost:5025/api/auth/reset-password', {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify({ email, token, newPassword })
  });
  
  if (response.ok) {
    const message = await response.text();
    alert(message);
    // Redirect to login
  } else {
    const error = await response.json();
    alert('Error: ' + error.errors.join(', '));
  }
}
```

## Troubleshooting

### Email Not Sending

1. **Check SMTP Configuration**: Ensure all email settings are correct
2. **Check Firewall**: Make sure port 587 is not blocked
3. **Check Logs**: Review application logs for detailed error messages
4. **Gmail Issues**: Ensure you're using an App Password, not your regular password
5. **Test SMTP Credentials**: Use a tool like Telnet to test SMTP server connectivity

### Token Invalid or Expired

1. **Check Token Expiration**: Tokens expire after 24 hours by default
2. **URL Encoding**: Ensure the token is properly URL encoded/decoded
3. **Check User Exists**: Ensure the email corresponds to an existing user
4. **Token Provider**: Verify `AddDefaultTokenProviders()` is called in Program.cs

## Files Modified/Created

**Created:**
- `MRTracking/DTO/ForgotPasswordRequestDTO.cs`
- `MRTracking/DTO/ResetPasswordRequestDTO.cs`
- `MRTracking/Services/IEmailService.cs`
- `MRTracking/Services/EmailService.cs`

**Modified:**
- `MRTracking/Controllers/AuthController.cs` - Added forgot-password and reset-password endpoints
- `MRTracking/Program.cs` - Registered EmailService
- `MRTracking/appsettings.json` - Added email and app settings

## Next Steps

1. Configure your email settings in `appsettings.json`
2. Update the frontend URL to match your frontend application
3. Implement the forgot password and reset password pages in your frontend
4. Test the complete flow end-to-end
5. Consider customizing the email template to match your branding

## Additional Customization Options

### Custom Token Expiration

To change the token expiration time, modify the Identity configuration in `Program.cs`:

```csharp
builder.Services.Configure<DataProtectionTokenProviderOptions>(options =>
{
    options.TokenLifespan = TimeSpan.FromHours(2); // Change from 24 hours to 2 hours
});
```

### Custom Email Template

Modify the `emailBody` variable in the `ForgotPassword` method in `AuthController.cs` to customize the email appearance.

### Add Password Strength Validation

The password validation rules are configured in `Program.cs` under Identity configuration. Current settings require a minimum of 5 characters with no special requirements.

