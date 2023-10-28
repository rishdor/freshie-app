CREATE TABLE Users (
user_id INT PRIMARY KEY IDENTITY(1000000, 1),
password VARCHAR(255) NOT NULL check(password LIKE '%[A-Z]%' AND password LIKE '%[0-9]%' AND LEN(password) >= 8),
name VARCHAR(100) NOT NULL
);