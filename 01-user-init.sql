-- Create users table
CREATE TABLE IF NOT EXISTS users (
    id SERIAL PRIMARY KEY,

    -- Basic Info
    email VARCHAR(255) UNIQUE NOT NULL,
    name VARCHAR(255) NOT NULL,
    password_hash VARCHAR(255) NOT NULL,

    -- Contact Info
    phone VARCHAR(20),
    phone_verified BOOLEAN DEFAULT FALSE,

    -- Personal Info
    sex VARCHAR(10) CHECK (sex IN ('male', 'female', 'other', NULL)),
    birthday DATE,
    avatar_url VARCHAR(500),

    -- Security & Verification
    otp VARCHAR(10),
    otp_expiration TIMESTAMP WITH TIME ZONE,
    is_email_verified BOOLEAN DEFAULT FALSE,

    -- E-commerce/Marketplace specific
    role VARCHAR(20) DEFAULT 'customer' CHECK (role IN ('customer', 'seller', 'admin')),
    is_active BOOLEAN DEFAULT TRUE,

    -- Preferences
    preferred_language VARCHAR(10) DEFAULT 'fr',
    preferred_currency VARCHAR(3) DEFAULT 'USD',
    newsletter_subscribed BOOLEAN DEFAULT FALSE,

    -- Additional Info
    last_login_at TIMESTAMP WITH TIME ZONE,

    -- Timestamps
    created_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP
);

-- Create indexes for faster lookups
CREATE INDEX IF NOT EXISTS idx_users_email ON users(email);
CREATE INDEX IF NOT EXISTS idx_users_phone ON users(phone);
CREATE INDEX IF NOT EXISTS idx_users_role ON users(role);

-- Insert seed data
INSERT INTO users (
    email,
    name,
    password_hash,
    phone,
    sex,
    birthday,
    role,
    is_email_verified,
    phone_verified
) VALUES
    (
        'admin@wenze.com',
        'Admin User',
        '$2a$10$XQYvZ8qH9L0K5N2M3P4Q5e6R7S8T9U0V1W2X3Y4Z5A6B7C8D9E0F1',
        '+1234567890',
        'male',
        '1985-01-15',
        'admin',
        TRUE,
        TRUE
    ),
    (
        'seller@wenze.com',
        'Jane Seller',
        '$2a$10$A1B2C3D4E5F6G7H8I9J0K1L2M3N4O5P6Q7R8S9T0U1V2W3X4Y5Z6',
        '+1234567891',
        'female',
        '1990-05-20',
        'seller',
        TRUE,
        TRUE
    ),
    (
        'customer@wenze.com',
        'John Customer',
        '$2a$10$Z9Y8X7W6V5U4T3S2R1Q0P9O8N7M6L5K4J3I2H1G0F9E8D7C6B5A4',
        '+1234567892',
        'male',
        '1995-08-10',
        'customer',
        TRUE,
        FALSE
    )
ON CONFLICT (email) DO NOTHING;
