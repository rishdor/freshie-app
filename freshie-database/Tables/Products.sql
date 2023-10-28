CREATE TABLE Products (
product_id INT IDENTITY(1,1) PRIMARY KEY,
product_name VARCHAR(100) NOT NULL,
icon VARBINARY(1000),
category_id INT,
shelf_id INT,
FOREIGN KEY (category_id) REFERENCES Categories(category_id),
FOREIGN KEY (shelf_id) REFERENCES Shelves(shelf_id)
);
