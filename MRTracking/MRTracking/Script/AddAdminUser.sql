-- =============================================
-- Script to Add Roles and Admin User (PostgreSQL)
-- Username: admin@yopmail.com
-- Password: Adm1n@123
-- =============================================

-- Step 1: Insert all application roles
DO $$
BEGIN
    -- Insert Admin role
    INSERT INTO "AspNetRoles" ("Id", "Name", "NormalizedName", "ConcurrencyStamp")
    VALUES ('120bcbde-ff6e-49d5-913a-b488e2a8c860', 'Admin', 'ADMIN', '120bcbde-ff6e-49d5-913a-b488e2a8c860')
    ON CONFLICT ("Id") DO NOTHING;

    -- Insert MedicalRepresentative role
    INSERT INTO "AspNetRoles" ("Id", "Name", "NormalizedName", "ConcurrencyStamp")
    VALUES ('91e08da7-5a91-4dd8-b809-1fa19007d587', 'MedicalRepresentative', 'MEDICALREPRESENTATIVE', '91e08da7-5a91-4dd8-b809-1fa19007d587')
    ON CONFLICT ("Id") DO NOTHING;

    -- Insert Doctor role
    INSERT INTO "AspNetRoles" ("Id", "Name", "NormalizedName", "ConcurrencyStamp")
    VALUES ('2ab83b9d-b84f-4ec3-818e-e99e066ee000', 'Doctor', 'DOCTOR', '2ab83b9d-b84f-4ec3-818e-e99e066ee000')
    ON CONFLICT ("Id") DO NOTHING;

    -- Insert MedicalStore role
    INSERT INTO "AspNetRoles" ("Id", "Name", "NormalizedName", "ConcurrencyStamp")
    VALUES ('8b7fc89b-645a-44da-81d8-e2a7faf6af6a', 'MedicalStore', 'MEDICALSTORE', '8b7fc89b-645a-44da-81d8-e2a7faf6af6a')
    ON CONFLICT ("Id") DO NOTHING;

    RAISE NOTICE 'All roles have been inserted or already exist.';
END $$;

-- Step 2: Create Admin User
DO $$
DECLARE
    v_UserId UUID := gen_random_uuid();
    v_AdminRoleId VARCHAR(450) := '120bcbde-ff6e-49d5-913a-b488e2a8c860';
    v_UserName VARCHAR(256) := 'admin@yopmail.com';
    v_NormalizedUserName VARCHAR(256) := 'ADMIN@YOPMAIL.COM';
    v_Email VARCHAR(256) := 'admin@yopmail.com';
    v_NormalizedEmail VARCHAR(256) := 'ADMIN@YOPMAIL.COM';
    -- Password hash for: Adm1n@123
    -- Generated using ASP.NET Core Identity Password Hasher V3
    v_PasswordHash TEXT := 'AQAAAAIAAYagAAAAEKvPW3qXNFYvzJvYmJvdEtL5LqKvhYTNl/3RzVxQSj4YMz4wH7K3j5L2qH+QqO5lLg==';
    v_SecurityStamp TEXT := UPPER(REPLACE(gen_random_uuid()::TEXT, '-', ''));
    v_ConcurrencyStamp TEXT := gen_random_uuid()::TEXT;
    v_UserExists INTEGER;
BEGIN
    -- Check if user already exists
    SELECT COUNT(*) INTO v_UserExists 
    FROM "AspNetUsers" 
    WHERE "NormalizedUserName" = v_NormalizedUserName 
       OR "NormalizedEmail" = v_NormalizedEmail;

    IF v_UserExists = 0 THEN
        -- Insert user into AspNetUsers table
        INSERT INTO "AspNetUsers" (
            "Id",
            "UserName",
            "NormalizedUserName",
            "Email",
            "NormalizedEmail",
            "EmailConfirmed",
            "PasswordHash",
            "SecurityStamp",
            "ConcurrencyStamp",
            "PhoneNumber",
            "PhoneNumberConfirmed",
            "TwoFactorEnabled",
            "LockoutEnd",
            "LockoutEnabled",
            "AccessFailedCount"
        )
        VALUES (
            v_UserId,
            v_UserName,
            v_NormalizedUserName,
            v_Email,
            v_NormalizedEmail,
            TRUE,
            v_PasswordHash,
            v_SecurityStamp,
            v_ConcurrencyStamp,
            NULL,
            FALSE,
            FALSE,
            NULL,
            TRUE,
            0
        );

        -- Assign Admin role to the user
        INSERT INTO "AspNetUserRoles" ("UserId", "RoleId")
        VALUES (v_UserId, v_AdminRoleId);

        RAISE NOTICE 'Admin user created successfully!';
        RAISE NOTICE 'Username: %', v_UserName;
        RAISE NOTICE 'User ID: %', v_UserId;
    ELSE
        RAISE NOTICE 'User with username "admin@yopmail.com" already exists. No action taken.';
    END IF;
END $$;

